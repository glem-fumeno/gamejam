using System;
using UnityEngine;

public class EffectLight : MonoBehaviour
{
    // public Light lightRef;
    public Camera cameraRef;
    public SpriteRenderer spriteRef;
    public CharacterLightController lightRef;
    public GameObject lightObject;

    // Update is called once per frame
    private void Start()
    {
        spriteRef = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(Time.timeScale == 0f)
            return;
        // Stick the position to the mouse cursor
        if (cameraRef != null)
        {
            Vector2 mousePosition = cameraRef.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
            spriteRef.color = CharacterLightController.CurrentColor;
        }

        if (Input.GetMouseButtonDown(0))
        {
            var light = Instantiate(lightObject, transform.position, Quaternion.identity);
            light.GetComponent<SpriteRenderer>().color = CharacterLightController.CurrentColor;
        }
    }
}
