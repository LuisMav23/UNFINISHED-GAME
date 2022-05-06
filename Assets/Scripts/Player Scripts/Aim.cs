using UnityEngine;

public class Aim : MonoBehaviour
{
    
    private Rigidbody2D playerRB;
    private Rigidbody2D rb;


    private Vector2 mousePos;
    
    void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        playerRB = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
    }
    
    void Update() {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rb.position = playerRB.position;
    }
    
    void FixedUpdate() {
        Vector2 lookDir = mousePos - rb.position;
        float lookAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = lookAngle;
    }
}
