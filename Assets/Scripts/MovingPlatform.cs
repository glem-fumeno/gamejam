using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector2 startPosition;

    public Vector2 endPosition;
    public float movingSpeed = 10f;
    private float distance = 0f;
    public float offset = 0f;
    private Rigidbody2D rb2d;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        distance = Mathf.PingPong((offset + Time.time) * movingSpeed * Time.fixedDeltaTime, 1);
        Vector2 new_pos = new Vector2(Mathf.Lerp(startPosition.x, endPosition.x, distance), 
                                      Mathf.Lerp(startPosition.y, endPosition.y, distance));
        rb2d.position = new_pos;
    }
}
