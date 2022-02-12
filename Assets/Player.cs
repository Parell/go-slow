using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public new Rigidbody rigidbody;
    [HideInInspector] public HealthController healthController;
    [SerializeField] private Thruster[] thrusters;

    [Header("States")]
    public bool isDead;

    [Space]
    public float yaw;

    [Header("Mass")]
    public float wetMass;
    [SerializeField] private float dryMass;
    [SerializeField] private float currentMass;

    [Header("Force")]
    [SerializeField] private float torqueForce;
    [SerializeField] private float mainForce;
    [SerializeField] private float force;

    [Space]

    public float Isp;
    public float mainIsp;

    [Space]
    public float Ve;
    public float massFlowRate;
    [Space]
    public float mainVe;
    public float mainMassFlowRate;
    [Space]
    public float totalDeltaV;
    public float currentDeltaV;

    private Vector3 forceDirection;
    private float gravity = 0.35f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        healthController = GetComponent<HealthController>();

        rigidbody.mass = wetMass;

        rigidbody.velocity = new Vector3(0, 0, 10);
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);

        Calculate();

        yaw = Input.GetAxis("Mouse X");

        StopThrusters();

        if (GameManager.Instance.isPaused || isDead) { return; }

        if (rigidbody.mass > dryMass)
        {
            HandleYaw();
            HandleForward();
            HandleBackward();
            HandleLeft();
            HandleRight();
            HandleMainThruster();
        }
        else
        {
            rigidbody.mass = dryMass;
            isDead = true;

            healthController.FuelDeath();
        }
    }

    private void Calculate()
    {
        torqueForce = 4 * force * Mathf.Sin(90);

        Isp = Ve / gravity;

        mainIsp = mainVe / gravity;

        force = massFlowRate * Ve;

        mainForce = mainMassFlowRate * mainVe;

        totalDeltaV = mainVe * Mathf.Log(wetMass / dryMass);
        currentDeltaV = mainVe * Mathf.Log(wetMass / rigidbody.mass);
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.isPaused || isDead) { return; }

        if (MainThruster()) { rigidbody.AddRelativeForce(Vector3.forward * mainForce); }

        rigidbody.AddRelativeTorque(Vector3.up * torqueForce * yaw);

        rigidbody.AddRelativeForce(forceDirection * force);

        forceDirection = Vector3.zero;
    }

    #region Inputs
    bool Forward()
    {
        if (Input.GetKey(KeyCode.W)) return true; return false;
    }

    bool Backward()
    {
        if (Input.GetKey(KeyCode.S)) return true; return false;
    }

    bool Left()
    {
        if (Input.GetKey(KeyCode.A)) return true; return false;
    }

    bool Right()
    {
        if (Input.GetKey(KeyCode.D)) return true; return false;
    }

    bool YawLeft()
    {
        if (yaw < 0) return true; return false;
    }

    bool YawRight()
    {
        if (yaw > 0) return true; return false;
    }

    bool MainThruster()
    {
        if (Input.GetKey(KeyCode.Space)) return true; return false;
    }
    #endregion

    #region Handles
    private void HandleYaw()
    {
        if (YawLeft())
        {
            thrusters[5].StartThruster();
            thrusters[6].StartThruster();
            LoseFuel();
        }
        else if (YawRight())
        {
            thrusters[4].StartThruster();
            thrusters[7].StartThruster();
            LoseFuel();
        }
        else if (!Right() && !Left())
        {
            thrusters[4].StopThruster();
            thrusters[7].StopThruster();
            thrusters[5].StopThruster();
            thrusters[6].StopThruster();
        }
    }

    private void HandleForward()
    {
        if (Forward())
        {
            thrusters[2].StartThruster();
            thrusters[3].StartThruster();
            forceDirection += Vector3.forward;
            LoseFuel();
        }
        else
        {
            thrusters[2].StopThruster();
            thrusters[3].StopThruster();
        }
    }

    private void HandleBackward()
    {
        if (Backward())
        {
            thrusters[0].StartThruster();
            thrusters[1].StartThruster();
            forceDirection += Vector3.back;
            LoseFuel();
        }
        else
        {
            thrusters[0].StopThruster();
            thrusters[1].StopThruster();
        }
    }

    private void HandleLeft()
    {
        if (Left())
        {
            thrusters[6].StartThruster();
            thrusters[7].StartThruster();
            forceDirection += Vector3.left;
            LoseFuel();
        }
        else if (!YawRight() && !YawLeft())
        {
            thrusters[6].StopThruster();
            thrusters[7].StopThruster();
        }
    }

    private void HandleRight()
    {
        if (Right())
        {
            thrusters[4].StartThruster();
            thrusters[5].StartThruster();
            forceDirection += Vector3.right;
            LoseFuel();
        }
        else if (!YawRight() && !YawLeft())
        {
            thrusters[4].StopThruster();
            thrusters[5].StopThruster();
        }
    }

    private void HandleMainThruster()
    {
        if (MainThruster())
        {
            thrusters[8].StartThruster();
            rigidbody.mass = rigidbody.mass - mainMassFlowRate * Time.deltaTime;
        }
        else
        {
            thrusters[8].StopThruster();
        }
    }
    #endregion

    private void LoseFuel()
    {
        rigidbody.mass = rigidbody.mass - massFlowRate * Time.deltaTime;
    }

    private void StopThrusters()
    {
        for (int i = 0; i < thrusters.Length; i++)
        {
            thrusters[i].StopThruster();
        }
    }
}
