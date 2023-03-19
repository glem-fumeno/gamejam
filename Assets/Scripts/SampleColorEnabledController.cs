using Unity.VisualScripting;
using UnityEngine;

public class SampleColorEnabledController : MonoBehaviour
{
    private ColorDetector colorDetector;
    private Color mixedColor;

    private SpriteRenderer[] _spriteRenderers;

    private void Start()
    {
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        colorDetector = GetComponent<ColorDetector>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            foreach (var spriteRenderer in _spriteRenderers)
            {
                spriteRenderer.color = Color.white;
                mixedColor = Color.clear;
            }

            colorDetector.amountDetected = 0;
        }
        if (!colorDetector.isColorDetected) return;
        if (colorDetector.amountDetected == 2)
        {
            mixedColor = CharacterLightController.CurrentColor;
            ColorMechanics(mixedColor);
            foreach (var spriteRenderer in _spriteRenderers)
            {
                spriteRenderer.color = mixedColor;
            }
        }
        else
        {
            mixedColor += CharacterLightController.CurrentColor;
            mixedColor.a = 1;
            ColorMechanics(mixedColor);
            foreach (var spriteRenderer in _spriteRenderers)
            {
                spriteRenderer.color = mixedColor;
            }
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
        }
        else if (color == Color.green)
        {
            Debug.Log("Green");
            /*
            Transform parent = GetComponentInParent<Transform>();
            Rigidbody2D rb2d = GetComponentInParent<Rigidbody2D>();
            while (parent.GetComponentInParent<Transform>() != null && rb2d.GetComponentInParent<Rigidbody2D>() != null)
            {
                parent = GetComponentInParent<Transform>();
                rb2d = GetComponentInParent<Rigidbody2D>();
            }
            Debug.Log(parent.name);
            if(rb2d != null)
                rb2d.constraints = RigidbodyConstraints2D.None;
            if (parent != null)
                parent.position = new Vector3(parent.position.x, parent.position.y + 2, 0);
            Debug.Log(parent.position);*/
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
                rb2d.constraints = RigidbodyConstraints2D.None;
                rb2d.constraints = RigidbodyConstraints2D.FreezePositionY;
            }
            
        }
        else if (color == Color.magenta)
        {
            Debug.Log("Magenta");
        }
        else if (color == Color.cyan)
        {
            Debug.Log("Cyan");
        }

    }
}
