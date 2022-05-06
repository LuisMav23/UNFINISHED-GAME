using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    [SerializeField] private Transform followTarget;
    [SerializeField] private float smoothSpeed = 0.00f;
    [SerializeField] private Texture2D cursor;
    [SerializeField] private Texture2D cursorOnClick;

    void FixedUpdate() {
        Vector3 targetPos = new Vector3(followTarget.position.x, followTarget.position.y, -15.00f);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);
        gameObject.transform.position = smoothedPosition;
        setCursor();
    }

    //ilipat sa game manager pag meron na
    void setCursor(){
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);

        if (Input.GetButton("Fire1")) {
            Cursor.SetCursor(cursorOnClick, Vector2.zero, CursorMode.ForceSoftware);
        }
    }
}
