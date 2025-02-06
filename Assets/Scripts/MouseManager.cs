using UnityEngine;

public class CrosshairManager : MonoBehaviour
{
    public Texture2D defaultCrosshair;
    public Texture2D enemyCrosshair;

    public Texture2D miningCrosshair;

    void Start()
    {
        Cursor.SetCursor(defaultCrosshair, Vector2.zero, CursorMode.Auto);
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null && hit.collider.gameObject.tag == "Enemy")
        {
            Cursor.SetCursor(enemyCrosshair, Vector2.zero, CursorMode.Auto);
        }
        if (hit.collider != null && hit.collider.gameObject.tag == "Ore")
        {
            Cursor.SetCursor(miningCrosshair, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(defaultCrosshair, Vector2.zero, CursorMode.Auto);
        }
    }
}
