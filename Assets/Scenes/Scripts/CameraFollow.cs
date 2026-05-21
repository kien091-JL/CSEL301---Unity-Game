using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Rigidbody targetRb;

    public Vector3 offset = new Vector3(0, 2, -4);
    public float smooth = 5f;

    void LateUpdate()
    {
        // Auto find player
        if (target == null)
        {
            GameObject car = GameObject.FindGameObjectWithTag("Player");

            if (car != null)
            {
                target = car.transform;
                targetRb = car.GetComponent<Rigidbody>();
            }
            else return;
        }

        // Get movement direction (IMPORTANT FIX)
        Vector3 moveDir = targetRb != null ? targetRb.linearVelocity : target.forward;

        if (moveDir.magnitude < 0.1f)
            moveDir = target.forward;

        moveDir.Normalize();

        // Camera follows movement, not rotation
        Vector3 desiredPos = target.position - moveDir * Mathf.Abs(offset.z)
                             + Vector3.up * offset.y;

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPos,
            smooth * Time.deltaTime
        );

        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}