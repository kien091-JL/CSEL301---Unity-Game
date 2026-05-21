using UnityEngine;
using UnityEngine.SceneManagement;

public class CarSelection : MonoBehaviour
{
    private int index = 0;
    public GameObject[] cars;

    void Start()
    {
        if (cars == null || cars.Length == 0) return;

        for (int i = 0; i < cars.Length; i++)
        {
            cars[i].SetActive(false);
        }

        cars[index].SetActive(true);
    }

    public void Next()
    {
        cars[index].SetActive(false);

        index++;

        if (index >= cars.Length)
            index = 0;

        cars[index].SetActive(true);

        Debug.Log("Index = " + index);
    }

    public void Prev()
    {
        cars[index].SetActive(false);

        index--;

        if (index < 0)
            index = cars.Length - 1;

        cars[index].SetActive(true);

        Debug.Log("Index = " + index);
    }

    public void SelectCar()
    {
        PlayerPrefs.SetInt("carIndex", index);
        PlayerPrefs.Save();

        Debug.Log("SAVED INDEX = " + index);

        SceneManager.LoadScene(2); // Game Scene
    }
}