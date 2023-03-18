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
        if (!colorDetector.isColorDetected) return;
        foreach (var spriteRenderer in _spriteRenderers)
        {
            spriteRenderer.color = CharacterLightController.CurrentColor;
        }
    }
}
