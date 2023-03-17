using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandSpriteController : MonoBehaviour
{
    private Transform _transform;
    public Camera cam;
    private Vector2 _mousePos;
    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    void Update()
    {
        _mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = _mousePos - new Vector2(_transform.position.x, _transform.position.y);
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        _transform.eulerAngles =  new Vector3(0, 0, angle);
    }
}
