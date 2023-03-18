using UnityEngine;

public class ColorDetector : MonoBehaviour
{
    public bool isColorDetected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("EffectLight")) return;
        Debug.Log("Color Detected");
        isColorDetected = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("EffectLight")) return;
        Debug.Log("Color Lost");
        isColorDetected = false;
    }
}
