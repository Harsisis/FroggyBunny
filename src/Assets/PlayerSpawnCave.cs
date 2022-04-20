using UnityEngine;

public class PlayerSpawnCave : MonoBehaviour
{
    private void Awake()
    {
        if (null != GameObject.FindGameObjectsWithTag("PlayerCave"))
        {
            GameObject.FindGameObjectWithTag("PlayerCave").transform.position = transform.position;
        }
    }
}
