using UnityEngine;

public class SpeedBoostPickup : MonoBehaviour
{
    [Header("Boost Settings")]
    public float boostAmount = 10f;
    public float accelerationBoost = 3f;
    public float boostDuration = 3f;

    private void OnTriggerEnter(Collider other)
    {
        // SEARCH PARENT TOO
        car1 car = other.GetComponentInParent<car1>();

        if (car != null)
        {
            Debug.Log("BOOST COLLECTED!");

            car.ApplySpeedBoost(
                boostAmount,
                accelerationBoost,
                boostDuration
            );

            Destroy(gameObject);
        }
    }
}