using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrip : MonoBehaviour
{
    [SerializeField]
    GameObject Fire;
    [SerializeField]
    string FireName;
    [SerializeField]
    GameObject Light;
    SpriteRenderer m_SpriteRenderer;
    [SerializeField]
    Sprite Off;


    // Start is called before the first frame update
    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void ToggleLight()
    {
        Destroy(GetComponent<Animator>());
        m_SpriteRenderer.sprite = Off;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                if(hit.collider.name == FireName)
                {
                    ToggleLight();
                    Debug.Log(hit.collider.name);
                }

            }
        }
    }
}
