using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [HideInInspector]public Vector2 startPosition;

    [HideInInspector]public Vector2 endPosition;
    public float movingSpeed = 10f;
    private float distance = 0f;
    public float offset = 0f;
    private Rigidbody2D rb2d;
    private bool onPlatform = false;
    private GameObject player;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        startPosition = transform.GetChild(0).GetComponent<Transform>().position;
        endPosition = transform.GetChild(1).GetComponent<Transform>().position;

    }

    void FixedUpdate()
    {
        //Debug.Log(startPosition.position);
        if(player != null && player.GetComponent<Rigidbody2D>().bodyType != RigidbodyType2D.Kinematic && onPlatform && !player.GetComponent<CharacterControler>().isMoving)
        {
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        }
        distance = Mathf.PingPong((offset + Time.time) * movingSpeed * Time.fixedDeltaTime, 1);
        Vector2 new_pos = new Vector2(Mathf.Lerp(startPosition.x, endPosition.x, distance), 
                                      Mathf.Lerp(startPosition.y, endPosition.y, distance));
        rb2d.position = new_pos;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("GameController"))
        {
            player = other.gameObject;
            onPlatform = true;
            other.transform.SetParent(gameObject.transform);
            other.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("GameController"))
        {
            onPlatform = false;
            other.transform.SetParent(null);
            other.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            DontDestroyOnLoad(other.gameObject);
        }
    }
}
