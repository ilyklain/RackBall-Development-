using UnityEngine;

public class RackMainBallCollisionPhysics : MonoBehaviour
{
    public float friction = 0.985f;
    public float minVelocity = 0.03f;
    public float bounceFactor = 0.95f;
    public float angularDragFactor = 0.98f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        ApplyFriction();
        ApplyAngularDrag();
        CheckStopCondition();
    }

    void ApplyFriction()
    {
        if (rb.velocity.magnitude > 0)
        {
            rb.velocity *= friction;
        }
    }

    void ApplyAngularDrag()
    {
        if (Mathf.Abs(rb.angularVelocity) > 0)
        {
            rb.angularVelocity *= angularDragFactor;
        }
    }

    void CheckStopCondition()
    {
        if (rb.velocity.magnitude < minVelocity)
        {
            rb.velocity = Vector2.zero;
        }
        if (Mathf.Abs(rb.angularVelocity) < minVelocity)
        {
            rb.angularVelocity = 0f;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            HandleBallCollision(collision);
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            HandleWallCollision(collision);
        }
    }

    void HandleBallCollision(Collision2D collision)
    {
        Rigidbody2D otherRb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (otherRb == null)
            return;

        Vector2 normal = collision.contacts[0].normal;
        Vector2 relativeVelocity = rb.velocity - otherRb.velocity;
        float velocityAlongNormal = Vector2.Dot(relativeVelocity, normal);

        if (velocityAlongNormal > 0)
            return;

        float restitution = bounceFactor;
        float impulseScalar = -(1 + restitution) * velocityAlongNormal;
        impulseScalar /= (1 / rb.mass) + (1 / otherRb.mass);

        Vector2 impulse = impulseScalar * normal;
        rb.AddForce(impulse / rb.mass, ForceMode2D.Impulse);
        otherRb.AddForce(-impulse / otherRb.mass, ForceMode2D.Impulse);
    }

    void HandleWallCollision(Collision2D collision)
    {
        Vector2 normal = collision.contacts[0].normal;
        Vector2 reflectedVelocity = Vector2.Reflect(rb.velocity, normal);
        rb.velocity = reflectedVelocity * bounceFactor;
    }
}
