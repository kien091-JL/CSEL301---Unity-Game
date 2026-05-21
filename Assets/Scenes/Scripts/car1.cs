using UnityEngine;
using System.Collections;

public class car1 : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 35f;
    public float acceleration = 5f;
    public float turnSpeed = 80f;

    [Header("Physics")]
    public float downForce = 8f;
    public float brakeForce = 1f;

    public LapTime lapSystem;

    private Rigidbody rb;
    private bool finished = false;

    private float currentSpeed = 0f;

    // ORIGINAL VALUES
    private float originalMoveSpeed;
    private float originalAcceleration;

    // BOOST
    private Coroutine boostRoutine;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.centerOfMass = new Vector3(0, -1f, 0);

        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        rb.linearDamping = 0.5f;
        rb.angularDamping = 2f;

        // SAVE ORIGINAL VALUES
        originalMoveSpeed = moveSpeed;
        originalAcceleration = acceleration;

        if (lapSystem == null)
        {
            lapSystem = FindFirstObjectByType<LapTime>();
        }

        if (CompareTag("Untagged"))
        {
            gameObject.tag = "Player";
        }
    }

    void FixedUpdate()
    {
        // DOWNFORCE
        rb.AddForce(-transform.up * downForce);

        float move = Input.GetAxis("Vertical");
        float turn = Input.GetAxis("Horizontal");

        // SMOOTH ACCELERATION
        float targetSpeed = move * moveSpeed;
        // forward movement (smooth physics)
        Vector3 moveVelocity = transform.forward * move * moveSpeed;


        currentSpeed = Mathf.Lerp(
            currentSpeed,
            targetSpeed,
            acceleration * Time.fixedDeltaTime
        );

        Vector3 velocity =
            transform.forward * currentSpeed;

        rb.linearVelocity = new Vector3(
            velocity.x,
            rb.linearVelocity.y,
            velocity.z
        );

        // STEERING
        if (Mathf.Abs(currentSpeed) > 0.5f)
        // turning only when moving
        if (Mathf.Abs(move) > 0.1f)
        if (Mathf.Abs(move) > 0.1f)
        {
            float speedFactor =
                Mathf.Clamp01(
                    Mathf.Abs(currentSpeed) / moveSpeed
                );

            float steerAmount =
                turn *
                turnSpeed *
                (1f - speedFactor * 0.5f) *
                Time.fixedDeltaTime;

            Quaternion turnRotation =
                Quaternion.Euler(
                0,
                    steerAmount,
                0
            );

            rb.MoveRotation(
                rb.rotation * turnRotation
            );
        }

        // BRAKE
        // brake system
        if (Input.GetKey(KeyCode.Space))
        {
            currentSpeed = Mathf.Lerp(
                currentSpeed,
                0,
                brakeForce * Time.fixedDeltaTime
            );
        }
    }

    // SPEED BOOST FUNCTION
    public void ApplySpeedBoost(
        float speedBoost,
        float accelBoost,
        float duration
    )
    {
        if (boostRoutine != null)
        {
            StopCoroutine(boostRoutine);
        }

        boostRoutine = StartCoroutine(
            SpeedBoostCoroutine(
                speedBoost,
                accelBoost,
                duration
            )
        );
    }

    private IEnumerator SpeedBoostCoroutine(
        float speedBoost,
        float accelBoost,
        float duration
    )
    {
        // APPLY BOOST
        moveSpeed += speedBoost;
        acceleration += accelBoost;

        yield return new WaitForSeconds(duration);

        // RESET VALUES
        moveSpeed = originalMoveSpeed;
        acceleration = originalAcceleration;

        boostRoutine = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (finished) return;

        if (lapSystem == null)
            return;

        // CHECKPOINT
        Checkpoint cp = other.GetComponent<Checkpoint>();

        if (cp != null)
        {
            Debug.Log("Checkpoint: " + cp.index);
            lapSystem.HitCheckpoint(cp.index);
        }

        // FINISH
        if (other.CompareTag("Finish"))
        {
            finished = true;
            Debug.Log("FINISH HIT");

            lapSystem.HitFinish(rb);
        }
    }
}