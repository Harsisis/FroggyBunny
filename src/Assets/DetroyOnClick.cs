using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetroyOnClick : MonoBehaviour
{
    void OnMouseDown()
    {
        Destroy(gameObject);
    }
}
