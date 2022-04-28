using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyIfAlreadyLoaded : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GameObject GUI = GameObject.Find("GUI");

        if (null != GUI)
        {
            Destroy(GameObject.FindWithTag("NoDuplication"));
        }
    }
}
