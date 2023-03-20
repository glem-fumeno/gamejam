using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public WandSpriteController wand;
    private AudioSource audioSource;
    public void SetMaxLights(int lights){
        maxLights = lights;
    }

    // Update is called once per frame
    private void Start()
    {
        spriteRef = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        if(maxLights == 0)
            wand.gameObject.SetActive(false);
    }

    private void Update()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        if(Time.timeScale == 0f)
            return;
        // Stick the position to the mouse cursor
        if(SceneManager.GetActiveScene().name == "EndGame")
            return;
        if(maxLights > 0)
            wand.gameObject.SetActive(true);
        if (cameraRef != null)
        {
            Vector2 mousePosition = cameraRef.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
            spriteRef.color = CharacterLightController.CurrentColor;
        }
        spriteRef.enabled = _currentLights < maxLights;

        if (Input.GetMouseButtonDown(0) && _currentLights < maxLights)
        {
            var light = Instantiate(lightObject, transform.position, Quaternion.identity);
            light.GetComponent<SpriteRenderer>().color = CharacterLightController.CurrentColor;
            _staticLigts.Add(light); 
            _currentLights++;
            audioSource.Play();
        }

        if (Input.GetMouseButtonDown(1))
        {
            for (int i = 0; i < _currentLights; i++)
            {
                Destroy(_staticLigts[i]);
                audioSource.Play();
            }
            _staticLigts.Clear();
            _currentLights = 0;
        }
        
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        for (int i = 0; i < _currentLights; i++)
        {
            Destroy(_staticLigts[i]);
        }
        _staticLigts.Clear();
        _currentLights = 0;
    }
}
