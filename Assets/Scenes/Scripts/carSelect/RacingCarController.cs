using UnityEngine;

public class RacingCarController : MonoBehaviour
{
    public float speed = 20f;
    public float turnSpeed = 100f;
    public float brakePower = 5f;
    public float downForce = 10f; 

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -1f, 0);
    }

    void FixedUpdate()
    {
        rb.AddForce(Vector3.down * downForce, ForceMode.Acceleration);

        if (Input.GetKey(KeyCode.Space))
        {
            rb.linearVelocity = Vector3.Lerp(
                rb.linearVelocity,
                Vector3.zero,
                brakePower * Time.fixedDeltaTime
            );
        }

        float move = 0f;

        if (Input.GetKey(KeyCode.W))
            move = 1f;
        else if (Input.GetKey(KeyCode.S))
            move = -1f;

        rb.AddForce(transform.forward * move * speed);

        float steer = Input.GetAxis("Horizontal");

        if (Mathf.Abs(move) > 0.1f)
        {
            float rotation = steer * turnSpeed * Time.fixedDeltaTime;
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0, rotation, 0));
        }
    }
}