using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject[] carPrefabs;

    private GameObject currentCar;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SpawnCar();
    }

    public void SpawnCar()
    {
        if (carPrefabs == null || carPrefabs.Length == 0)
        {
            Debug.LogError("No car prefabs in GameManager!");
            return;
        }

        int index = PlayerPrefs.GetInt("carIndex", 0);

        if (index < 0 || index >= carPrefabs.Length)
            index = 0;

        GameObject spawnPointObj = GameObject.FindGameObjectWithTag("SpawnPoint");

        if (spawnPointObj == null)
        {
            Debug.LogError("NO SpawnPoint in scene (Tag missing)!");
            return;
        }

        Transform spawnPoint = spawnPointObj.transform;

        if (currentCar != null)
            Destroy(currentCar);

        currentCar = Instantiate(
            carPrefabs[index],
            spawnPoint.position,
            spawnPoint.rotation
        );

        currentCar.tag = "Player";

        Debug.Log("Car spawned: " + currentCar.name);
    }
}