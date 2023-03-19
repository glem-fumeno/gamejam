using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SampleColorEnabledController : MonoBehaviour
{
    private ColorDetector colorDetector;
    private Color mixedColor;
    private Collider2D _collider2D;
    private bool touching = false;
    private List<HiddenPlatform> hiddenPlatforms;

    private SpriteRenderer[] _spriteRenderers;

    private void Start()
    {
        hiddenPlatforms = new List<HiddenPlatform>();
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        colorDetector = GetComponent<ColorDetector>();
        _collider2D = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        /*if (!_collider2D.IsTouchingLayers(~1 << LayerMask.NameToLayer("Ignore Raycast")))
        {
            Rigidbody2D platformRigidbody2D = GetComponentInParent<Rigidbody2D>();
            touching = true;
            if (platformRigidbody2D != null)
            {
                platformRigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY;
                platformRigidbody2D.velocity = Vector2.zero;
            }
        }*/
        if (Input.GetMouseButtonDown(1))
        {
            foreach (var spriteRenderer in _spriteRenderers)
            {
                spriteRenderer.color = Color.white;
                mixedColor = Color.clear;
            }

            colorDetector.amountDetected = 0;
            //check if there are any hidden platforms and if not return 
            if (hiddenPlatforms == null) return;
            Debug.Log(hiddenPlatforms.Count);
            
            foreach (var platform in hiddenPlatforms)
            {
                platform.GetComponent<HiddenPlatform>().HidePlatform();
            }

            hiddenPlatforms.Clear();
        }
        if (!colorDetector.isColorDetected) return;
        if (colorDetector.amountDetected == 2)
        {
            mixedColor = CharacterLightController.CurrentColor;
            foreach (var spriteRenderer in _spriteRenderers)
            {
                spriteRenderer.color = mixedColor;
            }
            
            ColorMechanics(mixedColor);
        }
        else
        {
            mixedColor += CharacterLightController.CurrentColor;
            mixedColor.a = 1;
            foreach (var spriteRenderer in _spriteRenderers)
            {
                spriteRenderer.color = mixedColor;
            }
            ColorMechanics(mixedColor);
        }
        colorDetector.isColorDetected = false;
    }

    private void ColorMechanics(Color color)
    {
        if (color == Color.red)
        {
            Debug.Log("Red");
        }
        else if (color == Color.blue)
        {
            MovingPlatform platform = GetComponentInParent<MovingPlatform>();
            Rigidbody2D rb2d = GetComponentInParent<Rigidbody2D>();
            if (platform != null)
                platform.enabled = false;
            if (platform != null && rb2d != null)
                rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            touching = false;
        }
        else if (color == Color.green)
        {
            Debug.Log("Green");
            MovingPlatform rb2d = GetComponentInParent<MovingPlatform>();
            Rigidbody2D platformRigidbody2D = GetComponentInParent<Rigidbody2D>();
            if (rb2d != null && !rb2d.enabled && platformRigidbody2D != null && !touching)
            {
                platformRigidbody2D.constraints = RigidbodyConstraints2D.None;
                platformRigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
                platformRigidbody2D.velocity = Vector2.up * 0.5f;
            }
        }
        else if (color == new Color(1,1,0,1))
        {
            Debug.Log("Yellow");
            MovingPlatform platform = GetComponentInParent<MovingPlatform>();
            Rigidbody2D rb2d = GetComponentInParent<Rigidbody2D>();
            if (platform != null)
                platform.enabled = true;
            if(platform != null && rb2d != null)
            {
                platform.startPosition.y = transform.position.y;
                platform.endPosition.y = transform.position.y;
                rb2d.constraints = RigidbodyConstraints2D.None;
                rb2d.constraints = RigidbodyConstraints2D.FreezePositionY;
                rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
            
        }
        else if (color == Color.magenta)
        {
            Debug.Log("Magenta");
            HiddenPlatform hiddenPlatform = GetComponentInParent<HiddenPlatform>();
            if(hiddenPlatform.gameObject == null) return;
            Debug.Log("Found hidden platform");
            hiddenPlatform.ShowPlatform();
            hiddenPlatforms.Add(hiddenPlatform);
        }
        else if (color == Color.cyan)
        {
            Debug.Log("Cyan");
            MovingPlatform rb2d = GetComponentInParent<MovingPlatform>();
            Rigidbody2D platformRigidbody2D = GetComponentInParent<Rigidbody2D>();
            if (rb2d != null && !rb2d.enabled && platformRigidbody2D != null && !touching)
            {
                platformRigidbody2D.constraints = RigidbodyConstraints2D.None;
                platformRigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
                platformRigidbody2D.velocity = Vector2.down * 0.5f;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("some collision");
        if(col.gameObject.layer == LayerMask.NameToLayer("Ignore Raycast"))
            return;
        Debug.Log("collision serious");
        Rigidbody2D platformRigidbody2D = GetComponentInParent<Rigidbody2D>();
        touching = true;
        if (platformRigidbody2D != null)
        {
            platformRigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY;
            platformRigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            platformRigidbody2D.velocity = Vector2.zero;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Ignore Raycast"))
            return;
        touching = false;
    }
}
