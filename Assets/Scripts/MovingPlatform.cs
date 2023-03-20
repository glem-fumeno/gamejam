using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    // private GameObject player_platform;
    // private CharacterSpriteController csc;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        startPosition = transform.GetChild(0).GetComponent<Transform>().position;
        endPosition = transform.GetChild(1).GetComponent<Transform>().position;
        // player_platform = GameObject.FindGameObjectWithTag("GameController");
        // csc = player_platform.GetComponent<CharacterSpriteController>();
        // if(csc.loaded_platform == gameObject.name)
        //     Destroy(gameObject);
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
            // DontDestroyOnLoad(gameObject);
            // csc.loaded_platform = gameObject.name;
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
            // SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
            // csc.loaded_platform = "";
        }
    }
}
