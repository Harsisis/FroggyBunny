using UnityEngine;
using UnityEngine.SceneManagement;

public class Return : MonoBehaviour
{
    public GameObject settingWindow;

    public void CloseSettingWindow()
    {
        settingWindow.SetActive(false);
    }
}
