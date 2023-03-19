using UnityEngine;

public class SampleColorEnabledController : MonoBehaviour
{
    public ColorDetector colorDetector;

    private SpriteRenderer[] _spriteRenderers;

    private void Start()
    {
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            foreach (var spriteRenderer in _spriteRenderers)
            {
                spriteRenderer.color = Color.white;
            }

            colorDetector.amountDetected = 0;
        }
        if (!colorDetector.isColorDetected || CharacterLightController.CurrentColor == _spriteRenderers[0].color) return;
        if (colorDetector.amountDetected == 2)
        {
            foreach (var spriteRenderer in _spriteRenderers)
            {
                spriteRenderer.color = CharacterLightController.CurrentColor;
            }
        }
        else
        {
            foreach (var spriteRenderer in _spriteRenderers)
            {
                spriteRenderer.color += CharacterLightController.CurrentColor;
            }

        }
        colorDetector.isColorDetected = false;
    }
}
