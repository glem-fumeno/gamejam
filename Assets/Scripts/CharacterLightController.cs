using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterLightController : MonoBehaviour
{
    public Color[] BaseColors;
    public Color[] Colors;
    public int currentColor = 0;
    private int _previousColorIdx = 0;
    public SpriteRenderer spriteRef;

    public static Color CurrentColor;

    private float _lerpTemp = 0f;
    private const float LerpTime = 0.1f;

    // Update is called once per frame
    private void Update()
    {
        if(Colors.Length == 0)
        {
            spriteRef.color = Color.white;
            return;
        }
        if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            _lerpTemp = 0f;
            _previousColorIdx = currentColor;
            currentColor--;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            _lerpTemp = 0f;
            _previousColorIdx = currentColor;
            currentColor++;
        }
        currentColor = ProperMod(currentColor, Colors.Length);

        _lerpTemp += Time.deltaTime / LerpTime;
        CurrentColor = Color.Lerp(Colors[_previousColorIdx], Colors[currentColor], _lerpTemp);
        spriteRef.color = CurrentColor;
    }
    public void AddColor(int index){
        Array.Resize(ref Colors, Colors.Length + 1);
        Colors[Colors.Length - 1] = BaseColors[index];
    }

    private static int ProperMod(int x, int m)
    {
        return (x % m + m) % m;
    }
}
