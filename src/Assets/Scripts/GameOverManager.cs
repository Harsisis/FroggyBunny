using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI;

    public static GameOverManager instance;


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de GameOverManager dans la scène");
            return;
        }

        instance = this;
    }

    public void OnPlayerDeath()
    {
        DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnLoad();
        StartCoroutine(DelayBeforeMenuAppears());
    }

    public IEnumerator DelayBeforeMenuAppears()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(GameObject.Find("Player"));
        gameOverUI.SetActive(true);
    }

    public void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameObject.Find("Player").GetComponent<PlayerHealth>().Respawn();
        GameObject.Find("Player").GetComponent<SpriteRenderer>().enabled = true;
        gameOverUI.SetActive(false);
    }

    public void MainMenuButton()
    {
        // JL : Pas sûr que cela soit utile, si ?
        // DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnLoad();
        SceneManager.LoadScene("Menu");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
