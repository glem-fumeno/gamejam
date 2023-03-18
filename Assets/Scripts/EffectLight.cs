using UnityEngine;

public class EffectLight : MonoBehaviour
{
    public Light lightRef;
    public Color[] Colors;
    private int current_color = 0;
    public Collider2D colliderRef;
    public Camera cameraRef;
    public SpriteRenderer spriteRef;

    // Update is called once per frame
    private void Update()
    {
        // Stick the position to the mouse cursor
        Vector2 mousePosition = cameraRef.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);

        if(Input.GetAxis("Mouse ScrollWheel") < 0)
            current_color--;
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
            current_color++;
        current_color %= Colors.Length;
        if(current_color < 0)
            current_color += Colors.Length - 1;

        spriteRef.color = Colors[current_color];
    }
}
