using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Quit : MonoBehaviour
{
    [SerializeField]
    Button BtnQuit;

    void Start()
    {
        BtnQuit.onClick.AddListener(QuitApplication);
    }

    void QuitApplication()
    {
        Application.Quit();
    }
}
