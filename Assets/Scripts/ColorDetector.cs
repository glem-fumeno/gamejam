using UnityEngine;

public class ColorDetector : MonoBehaviour
{
    public bool isColorDetected;
    public int amountDetected = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("EffectLight") || !Input.GetMouseButton(0)) return;
        //Debug.Log("Color Detected");
        amountDetected++;
        isColorDetected = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("EffectLight")) return;
        // Debug.Log("Color Lost");
        isColorDetected = false;
    }
}
