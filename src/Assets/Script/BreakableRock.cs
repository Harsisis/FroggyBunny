using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BreakableRock : MonoBehaviour
{
    // Start is called before the first frame update
    SpriteRenderer Item;
    Sprite[] States;
    int Counter = 0;
    int NextImage = 0;
    [SerializeField]
    GameObject ItemToChange;
    void ChangeSprite(Sprite ns)
    {
        Item.sprite = ns;
    }

    void Start()
    {
        Item = gameObject.GetComponent<SpriteRenderer>();
        
    }
    void Update()
    {
        Breakable(2);


    }
 Boolean OnSprite(GameObject name)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            Debug.Log("je rentre dans le onsprite");
            if (hit.collider != null)
            {
                Debug.Log(name.name + "    " + hit.collider.gameObject.name);
                Debug.Log(hit.collider.gameObject.name);
               // hit.collider.attachedRigidbody.AddForce(Vector2.up);
                if (name == hit.collider.gameObject)
                {
                    Debug.Log("je retourne true");
                    return true;
                }

            }else
            {
                return false;
            }
        }
        return false;
    }

        void Breakable(int nbClicks)
        {
            if (OnSprite(ItemToChange))
            {
                Debug.Log("je suis cliqué");
                Debug.Log("Il y a " + NextImage + " images chargées et " + States.Length + " encore à charger, et le compteur c'est " + Counter);
                if (NextImage < States.Length)
                {

                    Counter++;
                    Debug.Log("Je change de compteur");

                    if (Counter == nbClicks)
                    {
                        Counter = 0;
                        ChangeSprite(States[NextImage]);
                        Debug.Log("je change d'image");
                        NextImage++;

                    }
                }

            }
        }
    
}
