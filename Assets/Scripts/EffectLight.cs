using UnityEngine;

public class EffectLight : MonoBehaviour
{
    // public Light lightRef;
    public Collider2D colliderRef;
    public Camera cameraRef;
    public SpriteRenderer spriteRef;
    public CharacterLightController lightRef;

    // Update is called once per frame
    private void Update()
    {
        if(Time.timeScale == 0f)
            return;
        // Stick the position to the mouse cursor
        Vector2 mousePosition = cameraRef.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
        spriteRef.color = lightRef.Colors[lightRef.currentColor];
    }
}
