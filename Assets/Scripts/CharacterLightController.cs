using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLightController : MonoBehaviour
{
    public Color[] Colors;
    public int currentColor = 0;
    public SpriteRenderer spriteRef;

    public static Color CurrentColor;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") < 0)
            currentColor--;
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
            currentColor++;
        currentColor %= Colors.Length;
        if(currentColor < 0)
            currentColor += Colors.Length - 1;

        spriteRef.color = Colors[currentColor];
        CurrentColor = Colors[currentColor];
    }
}
