using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Body : MonoBehaviour
{
    protected static readonly List<Body> bodies = new List<Body>();

    [Tooltip("Whether or not this body is included when calculating NBody gravity")]
    public bool includeInOtherBodyCalculations = true;
    public bool isStatic = false;
    public GameObject parent;

    [Tooltip("The velocity this body will start with")]
    public Vector3 initialVelocity;

    public float mass => this.rigidbody.mass;

    private new Rigidbody rigidbody;

    public Vector3 getVelocity() => this.rigidbody.velocity;
    public Vector3 getPosition() => this.rigidbody.position;

    private void Awake()
    {
        this.rigidbody = this.GetComponent<Rigidbody>();

        if (parent != null)
        {
            this.rigidbody.velocity = parent.GetComponentInParent<Rigidbody>().velocity;
        }
        else
        {
            this.rigidbody.velocity = initialVelocity;
        }
        Body.bodies.Add(this);
    }

    private void FixedUpdate()
    {
        this.addVelocity(this._getGravitationalAcceleration() * Time.fixedDeltaTime);
    }

    public void addVelocity(Vector3 velocity)
    {
        this.rigidbody.velocity += velocity;
    }

    protected Vector3 calculateAttractionTo(Body other)
    {
        return this.calculateAttractionTo(other, this.transform.position);
    }

    protected Vector3 calculateAttractionTo(Body other, Vector3 fromPosition)
    {
        Vector3 delta = other.transform.position - fromPosition;

        Vector3 direction = delta.normalized;
        float squareDistance = delta.sqrMagnitude;

        Vector3 deltaV = (GameManager.gravityConstant * other.mass * direction) / squareDistance;

        return deltaV;
    }

    protected Vector3 _getGravitationalAcceleration()
    {
        return this.getGravitationalAcceleration(this.getPosition());
    }

    public Vector3 getGravitationalAcceleration(Vector3 position)
    {
        if (isStatic)
        {
            return Vector3.zero;
        }
        else
        {
            Vector3 acceleration = Vector3.zero;

            foreach (Body body in Body.bodies)
            {
                if (this == body) continue;
                if (!body.includeInOtherBodyCalculations) continue;

                acceleration += this.calculateAttractionTo(body, position);
            }

            return acceleration;
        }
    }
}