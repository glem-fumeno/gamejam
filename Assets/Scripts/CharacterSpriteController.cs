using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpriteController : MonoBehaviour
{
    private Transform _transform;
    public Camera cam;
    private Vector2 _mousePos;
    public MenuManager menu;
    public string loaded_platform;
    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!menu.is_paused)
                menu.Pause();
            else
                menu.Resume();
        }
        if(Time.timeScale == 0f)
            return;

        _mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (_mousePos.x > _transform.position.x)
            _transform.localScale = new Vector3(1f, 1f, 1f);
        else
            _transform.localScale = new Vector3(-1f, 1f, 1f);

    }
}
