using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    [SerializeField]
    Button BtnPlay;
    public string sceneName;

    void Start()
    {
        BtnPlay.onClick.AddListener(startPlay);
    }

    void startPlay()
    {
        SceneManager.LoadScene(sceneName);
    }
}