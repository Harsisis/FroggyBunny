using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyIfAlreadyLoaded : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GameObject canvas = GameObject.Find("Canvas");
        GameObject inventory = GameObject.Find("Inventory");

        if (null != canvas || null != inventory)
        {
            Destroy(GameObject.FindWithTag("CanvasDuplication"));
            Destroy(GameObject.FindWithTag("InventoryDuplication"));
        }
    }
}
