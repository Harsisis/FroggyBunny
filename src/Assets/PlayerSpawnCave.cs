using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    private void Awake()
    {
        if (null != GameObject.FindGameObjectsWithTag("PlayerCave"))
        {
            GameObject.FindGameObjectWithTag("PlayerCave").transform.position = transform.position;
        }
    }
}
