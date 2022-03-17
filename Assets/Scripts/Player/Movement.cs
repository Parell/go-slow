using UnityEngine;

public class Movement : MonoBehaviour
{
    [HideInInspector] public new Rigidbody rigidbody;
    [HideInInspector] public Player player;

    public Thruster[] thrusters;

    [Space]

    public Vector3 moveDirection;
    public float yaw;
    public bool mainThruster;

    [Space]
    public float totalMass;
    public float emptyMass;

    [Space]
    public float maneuverExitVelocity;
    public float maneuverMassFlowRate;
    public float maneuverForce;

    [Space]
    public float mainExitVelocity;
    public float mainMassFlowRate;
    public float mainForce;

    [Space]
    public float totalDeltaV;
    public float currentDeltaV;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        player = GetComponent<Player>();

        rigidbody.mass = totalMass;
    }

    private void Update()
    {
        Calculations();

        // Lock to y axis
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        if (GameManager.Instance.isPaused || player.shipDisabled) { StopAllThrusters(); return; }

        if (rigidbody.mass > emptyMass)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            moveDirection.x = horizontal;
            moveDirection.z = vertical;

            mainThruster = Input.GetKey(KeyCode.Space);

            yaw = Input.GetAxisRaw("Yaw");

            HandleThruster();
        }
        else
        {
            mainThruster = false;
            player.shipDisabled = true;

            StopAllThrusters();

            rigidbody.mass = emptyMass;
        }
    }

    private void FixedUpdate()
    {
        if (mainThruster)
        {
            rigidbody.AddRelativeForce(Vector3.forward * mainForce);
        }

        rigidbody.AddRelativeForce(moveDirection * maneuverForce);
        rigidbody.AddRelativeTorque(Vector3.up * (2 * maneuverForce) * yaw);

        moveDirection = Vector3.zero;
        yaw = 0f;
    }

    private void Calculations()
    {
        maneuverForce = maneuverMassFlowRate * maneuverExitVelocity;
        mainForce = mainMassFlowRate * mainExitVelocity;

        totalDeltaV = maneuverExitVelocity * Mathf.Log(totalMass / emptyMass);
        currentDeltaV = maneuverExitVelocity * Mathf.Log(totalMass / rigidbody.mass);
    }

    private void HandleThruster()
    {
        #region Forward
        if (moveDirection.z == 1)
        {
            BurnFuel(maneuverMassFlowRate);

            thrusters[4].StartThruster();
            thrusters[5].StartThruster();
        }
        else
        {
            thrusters[4].StopThruster();
            thrusters[5].StopThruster();
        }
        #endregion

        #region Back
        if (moveDirection.z == -1)
        {
            BurnFuel(maneuverMassFlowRate);

            thrusters[0].StartThruster();
            thrusters[1].StartThruster();
        }
        else
        {
            thrusters[0].StopThruster();
            thrusters[1].StopThruster();
        }
        #endregion

        #region Left
        if (moveDirection.x == -1)
        {
            BurnFuel(maneuverMassFlowRate);

            thrusters[2].StartThruster();
            thrusters[3].StartThruster();
        }
        else if (yaw == 0)
        {
            thrusters[2].StopThruster();
            thrusters[3].StopThruster();
        }
        #endregion

        #region Right
        if (moveDirection.x == 1)
        {
            BurnFuel(maneuverMassFlowRate);

            thrusters[6].StartThruster();
            thrusters[7].StartThruster();
        }
        else if (yaw == 0)
        {
            thrusters[6].StopThruster();
            thrusters[7].StopThruster();
        }
        #endregion

        #region Yaw
        if (yaw == -1)
        {
            BurnFuel(maneuverMassFlowRate);

            thrusters[2].StartThruster();
            thrusters[6].StartThruster();
        }
        else if (yaw == 1)
        {
            BurnFuel(maneuverMassFlowRate);

            thrusters[3].StartThruster();
            thrusters[7].StartThruster();
        }
        else if (moveDirection.x != -1 && moveDirection.x != 1)
        {
            thrusters[2].StopThruster();
            thrusters[6].StopThruster();
            thrusters[3].StopThruster();
            thrusters[7].StopThruster();
        }
        #endregion

        #region Main
        if (mainThruster)
        {
            BurnFuel(mainMassFlowRate);

            thrusters[8].StartThruster();
        }
        else
        {
            thrusters[8].StopThruster();
        }
        #endregion
    }

    private void BurnFuel(float massFlowRate)
    {
        rigidbody.mass -= massFlowRate * Time.deltaTime;
    }

    private void StopAllThrusters()
    {
        for (int i = 0; i < thrusters.Length; i++)
        {
            thrusters[i].StopThruster();
        }
    }
}
