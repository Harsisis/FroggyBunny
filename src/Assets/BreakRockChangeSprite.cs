using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakRockChangeSprite : MonoBehaviour
{
    [SerializeField]
    public SpriteRenderer spriteRenderer;
    [SerializeField]
    public Sprite[] spriteArray;
    public int currentSprite;
    void ChangeSprite()
    {
        spriteRenderer.sprite = spriteArray[currentSprite];
        currentSprite++;
        if (currentSprite >= spriteArray.Length)
        {
            currentSprite = 0;
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ChangeSprite();
        }
    }
}
