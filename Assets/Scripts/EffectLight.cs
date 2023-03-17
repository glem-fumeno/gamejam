using UnityEngine;

public class EffectLight : MonoBehaviour
{
    public Light lightRef;
    public Collider2D colliderRef;
    public Camera cameraRef;

    // Update is called once per frame
    private void Update()
    {
        // Stick the position to the mouse cursor
        var mousePosition = cameraRef.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePosition;
    }
}
