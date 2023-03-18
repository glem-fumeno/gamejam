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

    [SerializeField]private LayerMask groundMask;
    [SerializeField]private Collider2D rightWallDetector;
    [SerializeField]private Collider2D leftWallDetector;
    [SerializeField]private Collider2D ceilingDetector;
    [SerializeField]private Transform groundDetector;
    [SerializeField]private float jumpTime = 0.5f;
    float jumpTimeCounter;
    private bool _jumped;

    public Animator animator;
    public KeyMapping keyMapping;
    
    private int _wallIndicator = 0;


    private void Start()
    {
        jumpTimeCounter = jumpTime;
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

        if (Input.GetKeyDown(keyMapping.getKeyCode("Jump")) && isGround)
        {
            _jumped = true;
            jumpTimeCounter = jumpTime;
            _playerRigidbody.velocity = Vector2.up * jumpHeight;
        }
        if(Input.GetKey(keyMapping.getKeyCode("Jump")) && _jumped)
        {
            if(jumpTimeCounter > 0 && !ceilingDetector.IsTouchingLayers(groundMask))
            {
                _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, jumpHeight);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                _jumped = false;
            }
        }
        if(Input.GetKeyUp(keyMapping.getKeyCode("Jump")))
        {
            _jumped = false;
        }
        
        if (rightWallDetector.IsTouchingLayers(groundMask))
        {
            _wallIndicator = 1;
        }
        else if(leftWallDetector.IsTouchingLayers(groundMask))
        {
            _wallIndicator = -1;
        }
        else
        {
            _wallIndicator = 0;
        }
    }

    private void Move()
    {
        int left = Input.GetKey(keyMapping.getKeyCode("Left")) ? 1 : 0;
        int right = Input.GetKey(keyMapping.getKeyCode("Right")) ? 1 : 0;
        float horizontalInput = right - left;
        if (_wallIndicator * transform.localScale.x == horizontalInput)
        {
            
            _playerRigidbody.velocity = new Vector2(0, _playerRigidbody.velocity.y);
            
            animator.SetBool("Moving", false);
        }
        else
        {
            _playerRigidbody.velocity = new Vector2(horizontalInput * _playerSpeed, _playerRigidbody.velocity.y);
            animator.SetBool("Moving", horizontalInput != 0);
        }
    }

    private bool Grounded()
    {
        return Physics2D.OverlapCircle(groundDetector.position, 0.1f, groundMask);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Environment"))
        {
            Debug.Log("wall");
        }
    }
}
