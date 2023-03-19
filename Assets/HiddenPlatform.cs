using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenPlatform : MonoBehaviour
{
    public bool isHidden = true;
    private SpriteRenderer spriteRenderer;
    
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (isHidden)
        {
            Color color = new Color(1, 1, 1, 1/4f);
            spriteRenderer.color = color;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
    
    public void ShowPlatform()
    {
        Color color = new Color(1, 1, 1, 1);
        spriteRenderer.color = color;
        GetComponent<BoxCollider2D>().enabled = true;
    }
    
    public void HidePlatform()
    {
        Color color = new Color(1, 1, 1, 1/4f);
        spriteRenderer.color = color;
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
