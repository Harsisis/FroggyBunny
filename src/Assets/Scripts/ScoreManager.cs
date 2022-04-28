using System.Collections;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoosePointsEverySeconds());
    }

    public IEnumerator LoosePointsEverySeconds()
    {
        while (Inventory.instance.coinsCount > 0)
        {
            Inventory.instance.RemoveCoins(10);
            yield return new WaitForSeconds(1);
        }

        if (Inventory.instance.coinsCount <= 0)
        {
            PlayerHealth PlayerHealthInstance = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
            PlayerHealthInstance.Die();
        }
    }

    public void DieMalus() 
    {
        Inventory.instance.RemoveCoins(100);
    }

    public void CheckPointBonus()
    {
        Inventory.instance.AddCoins(500);
    }
}
