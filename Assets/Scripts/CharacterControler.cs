using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterControler : MonoBehaviour
{
    [SerializeField] private float _playerSpeed = 10;
    [SerializeField]private float jumpHeight = 8.5f;
    private Rigidbody2D _playerRigidbody;

    private LayerMask groundMask = ~1 << 3;

    public Animator animator;


    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        if (_playerRigidbody == null)
        {
            Debug.LogError("No rigidbody on player");
        }
        if (animator == null)
        {
            Debug.LogError("No animator chosen");
        }
    }

    void Update()
    {
        Move();

        bool isGround = Grounded();
        animator.SetBool("Jumping", !isGround);

        if (Input.GetKeyDown("space") && isGround)
        {
            Jump();
        }
    }

    private void Move()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        _playerRigidbody.velocity = new Vector2(horizontalInput * _playerSpeed, _playerRigidbody.velocity.y);
        animator.SetBool("Moving", horizontalInput != 0);
    }

    private void Jump()
    {
        _playerRigidbody.velocity = new Vector2(0, jumpHeight);
    }

    private bool Grounded()
    {
        
        var position = new Vector3(transform.position.x, transform.position.y - 1.1f, transform.position.z);
        var checkGround = Physics2D.Raycast(position, Vector2.down, 0.1f);
        //Debug.Log(checkGround.collider.tag);
        Debug.DrawLine(position, new Vector3(position.x, position.y - .1f, position.z), Color.black);
        return checkGround.collider != null && checkGround.collider.CompareTag("Ground");
    }
}
