using UnityEngine;

public class  check : MonoBehaviour
{
    void Start()
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();

        Debug.Log("=== TRACK COLLIDER CHECK ===");

        foreach (Collider col in colliders)
        {
            Debug.Log("Found collider: " + col.gameObject.name + " | Type: " + col.GetType());

            // OPTIONAL: disable unwanted colliders for testing
            // Uncomment this if you want to remove blocking colliders temporarily
            /*
            if (!col.isTrigger)
            {
                col.enabled = false;
                Debug.Log("Disabled collider on: " + col.gameObject.name);
            }
            */
        }
    }
}