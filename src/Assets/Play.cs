using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    [SerializeField]
    Button BtnPlay;

    void Start()
    {
        BtnPlay.onClick.AddListener(startPlay);
    }

    void startPlay()
    {
        System.Console.WriteLine("start game");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
