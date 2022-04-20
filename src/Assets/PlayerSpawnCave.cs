using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
<<<<<<< HEAD
 
=======
    private void Awake()
    {
        if (null != GameObject.FindGameObjectsWithTag("PlayerCave"))
        {
            GameObject.FindGameObjectWithTag("PlayerCave").transform.position = transform.position;
        }
    }
>>>>>>> 58ce8d1292d3774c85641c0091c6994f9a7da539
}
