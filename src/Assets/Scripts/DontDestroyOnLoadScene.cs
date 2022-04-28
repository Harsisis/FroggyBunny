using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoadScene : MonoBehaviour
{
    public GameObject[] objects;

    public static DontDestroyOnLoadScene instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de DontDestroyOnLoadScene dans la scène");
            return;
        }

        GameObject DontDestroyOnLoadInstance = GameObject.FindWithTag("InitialInstance");

        if (null != DontDestroyOnLoadInstance)
        {
            Destroy(GameObject.FindWithTag("NoDuplication"));
        }
        else
        {
            GameObject.FindWithTag("NoDuplication").tag = "InitialInstance";
        }

        instance = this;

        foreach (var element in objects)
        {
            DontDestroyOnLoad(element);
            Debug.Log(element.name + " est dans dontDestroyOnLoad");
        }
    }

    public void RemoveFromDontDestroyOnLoad()
    {
        foreach (var element in objects)
        {
            SceneManager.MoveGameObjectToScene(element, SceneManager.GetActiveScene());
        }
    }
}
