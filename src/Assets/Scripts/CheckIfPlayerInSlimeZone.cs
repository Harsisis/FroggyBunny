using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfPlayerInSlimeZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D colliderSlimeZone2D)
    {
        if (colliderSlimeZone2D.transform.CompareTag("Player"))
        {
            Mouvement mouvement = colliderSlimeZone2D.transform.GetComponent<Mouvement>();
            mouvement.ChangeMoveSpeedAndJumpForce(20.0f,100);

        }
    }
    private void OnCollisionExit2D(Collision2D colliderSlimeZone2D)
    {
        if (colliderSlimeZone2D.transform.CompareTag("Player"))
        {
            Mouvement mouvement = colliderSlimeZone2D.transform.GetComponent<Mouvement>();
            mouvement.ChangeMoveSpeedAndJumpForce(115.0f,140.0f);
        }
    }
}
