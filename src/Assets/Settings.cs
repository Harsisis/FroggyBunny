using UnityEngine;
using UnityEngine.SceneManagement;
public class Settings : MonoBehaviour
{
    public GameObject settingsWindow;
    public void SettingsButton()
    {
        settingsWindow.SetActive(true);
    }

}
