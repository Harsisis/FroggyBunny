using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int coinsCount;
    public Text coinsCountText;

    public static Inventory instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("Il y a plus d'une instance de Inventory dans la scène. On garde l'instance de dontDestroyOnLoad");
            return;
        }
        else
        {
            instance = this;
        }
    }

    public void AddCoins(int count)
    {
        coinsCount += count;
        coinsCountText.text = coinsCount.ToString();
    }

    public void RemoveCoins(int count)
    {
        coinsCount -= count;
        if (coinsCount <= 0)
        {
            coinsCount = 0;
        }
        coinsCountText.text = coinsCount.ToString();
    }
}
