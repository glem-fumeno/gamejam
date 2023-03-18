using System;
using System.Collections.Generic;
using UnityEngine;

public class EffectLight : MonoBehaviour
{
    // public Light lightRef;
    public Camera cameraRef;
    public SpriteRenderer spriteRef;
    public CharacterLightController lightRef;
    public GameObject lightObject;
    private List<GameObject> _staticLigts = new List<GameObject>();

    [SerializeField] private int maxLights = 2;
    private int _currentLights = 0;

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

        if (Input.GetMouseButtonDown(0) && _currentLights < maxLights)
        {
            var light = Instantiate(lightObject, transform.position, Quaternion.identity);
            light.GetComponent<SpriteRenderer>().color = CharacterLightController.CurrentColor;
            _staticLigts.Add(light); 
            _currentLights++;
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Right click");
            for (int i = 0; i < _currentLights; i++)
            {
                Debug.Log("Destroying light" + i);
                Destroy(_staticLigts[i]);
            }
            _staticLigts.Clear();
            _currentLights = 0;
        }
        
    }
}
