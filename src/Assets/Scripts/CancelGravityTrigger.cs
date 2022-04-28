using UnityEngine;
using System.Collections;

public class ExampleClass : MonoBehaviour
{
    public Collider coll;
     GameObject apple =  GameObject.Find("Apple_0");

    void Start()
    {
        coll = GetComponent<Collider>();
        coll.isTrigger = true;
    }

    // Disables gravity on all rigidbodies entering this collider.
    void OnTriggerEnter(Collider other)
    {
        apple.GetComponent<Rigidbody2D>().gravityScale = 0;
    }
}
