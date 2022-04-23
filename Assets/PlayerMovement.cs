using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float playerSpeed;
    
    private Rigidbody2D rb;
    private Rigidbody2D Aim;
    private Vector2 Move;

    void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        Aim = GameObject.FindGameObjectWithTag("Aim").GetComponent<Rigidbody2D>();
    }
    void Update() {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        Move = new Vector2 (horizontal, vertical) * playerSpeed * Time.fixedDeltaTime;
        Flip();
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + Move);
    }

    void Flip() {
        if (Aim.rotation <= 45.00f && Aim.rotation >= -45.00f) {rb.rotation = 0.00f;}
        if (Aim.rotation <= 90.00f && Aim.rotation >= 45.00f) {rb.rotation = 90.00f;}
        if (Aim.rotation <= -225.00f && Aim.rotation >= -270.00f) {rb.rotation = 90.00f;}
        if (Aim.rotation <= -45.00f && Aim.rotation >= -135.00f) {rb.rotation = -90.00f;}
        if (Aim.rotation <= -135.00f && Aim.rotation >= -225.00f) {rb.rotation = -180.00f;}
    }
}
