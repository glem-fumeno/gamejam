using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLightController : MonoBehaviour
{
    public Color[] Colors;
    public int currentColor = 0;
    private int _previousColorIdx = 0;
    public SpriteRenderer spriteRef;

    public static Color CurrentColor;

    private float _lerpTemp = 0f;
    private const float _lerpTime = 0.1f;

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
        currentColor %= Colors.Length;
        if(currentColor < 0)
        {
            currentColor += Colors.Length - 1;
        }

        _lerpTemp += Time.deltaTime / _lerpTime;
        CurrentColor = Color.Lerp(Colors[_previousColorIdx], Colors[currentColor], _lerpTemp);
        spriteRef.color = CurrentColor;
    }
}
