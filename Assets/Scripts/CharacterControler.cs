using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    private int _wallIndicator = 0;
    [HideInInspector]public bool isMoving = false;


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
//     void OnEnable() {
//       SceneManager.sceneLoaded += OnSceneLoaded;
//   }
 
//   void OnDisable() {
//       SceneManager.sceneLoaded -= OnSceneLoaded;
//   }
//     private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
//         Input.ResetInputAxes();
//   }
    public static void LoadScene( string SceneNameToLoad)
    {
        PendingPreviousScene = SceneManager.GetActiveScene().name;
        SceneManager.sceneLoaded += ActivatorAndUnloader;
        SceneManager.LoadScene( SceneNameToLoad, LoadSceneMode.Additive);
    }
 
    static string PendingPreviousScene;
    static void ActivatorAndUnloader( Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= ActivatorAndUnloader;
        SceneManager.SetActiveScene( scene);
        SceneManager.UnloadSceneAsync( PendingPreviousScene);
    }

    void Update()
    {
        Move();

        bool isGround = Grounded();
        animator.SetBool("Jumping", !isGround);

        if(InputManager.Instance.GetKeyDown(InputAction.Reset))
        {
            LoadScene(SceneManager.GetActiveScene().name);
        }

        if (isGround && InputManager.Instance.GetKeyDown(InputAction.MovementJump))
        {
            _jumped = true;
            jumpTimeCounter = jumpTime;
            _playerRigidbody.velocity = Vector2.up * jumpHeight;
        }

        if (_jumped && InputManager.Instance.GetKey(InputAction.MovementJump))
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

        if (InputManager.Instance.GetKeyUp(InputAction.MovementJump))
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
        _playerRigidbody.bodyType = RigidbodyType2D.Dynamic;
        var left = InputManager.Instance.GetKey(InputAction.MovementLeft) ? 1 : 0;
        var right = InputManager.Instance.GetKey(InputAction.MovementRight) ? 1 : 0;
        float horizontalInput = right - left;
        if(horizontalInput != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
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
