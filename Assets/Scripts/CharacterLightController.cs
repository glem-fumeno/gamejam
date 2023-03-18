using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterLightController : MonoBehaviour
{
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

    /// <summary>
    /// Actually computes a modulo operation, but always returns a positive result.
    /// </summary>
    /// <remarks>C#'s '%' operator is not modulo, but remainder</remarks>
    /// <param name="x">Number to mod</param>
    /// <param name="m">Modulus</param>
    /// <returns>Result</returns>
    private static int ProperMod(int x, int m)
    {
        return (x % m + m) % m;
    }
}
