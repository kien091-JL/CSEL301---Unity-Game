using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] carPrefabs;
    public Transform spawnPoint;

    void Start()
    {
        Debug.Log("CarSpawner Running");

        if (carPrefabs == null || carPrefabs.Length == 0)
        {
            Debug.LogError("No car prefabs assigned!");
            return;
        }

        if (spawnPoint == null)
        {
            Debug.LogError("SpawnPoint missing!");
            return;
        }

        int index = PlayerPrefs.GetInt("carIndex", 0);

        if (index < 0 || index >= carPrefabs.Length)
            index = 0;

        GameObject car = Instantiate(
            carPrefabs[index],
            spawnPoint.position,
            spawnPoint.rotation
        );

        car.tag = "Player";

        Debug.Log("Car spawned: " + car.name);
    }
}