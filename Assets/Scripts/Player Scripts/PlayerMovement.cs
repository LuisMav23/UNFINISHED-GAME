using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_defaultSpeed;
    [SerializeField] private float m_runMultiplier;
    [SerializeField] private float m_rotationSmoothing;
    [SerializeField] private Animator m_animator;

    private float m_playerSpeed;
    
    private Vector2 m_move;
    private Rigidbody2D m_rb;
    private Rigidbody2D m_aim;

    private bool m_isAiming = false;
    private bool m_inputxy = false;
    private bool m_inputShift = false;

    void Start() {

        //Reference rigidbodies
        m_rb = gameObject.GetComponent<Rigidbody2D>();
        m_aim = GameObject.FindGameObjectWithTag("Aim").GetComponent<Rigidbody2D>();
        var childrenCollider = gameObject.GetComponentsInChildren<CircleCollider2D>();

    }

    void Update() {

        //instantiate horizontal end vertical axis Input_s
        ResetRotation();
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_move = new Vector2 (horizontal, vertical) * m_playerSpeed * Time.fixedDeltaTime;

        if (Input.GetButton("Fire2")) {m_isAiming = true;}
        else {m_isAiming = false;}

        //sets Animator parameters value
        m_animator.SetBool("Input xy", m_inputxy);
        m_animator.SetBool("Input Shift", m_inputShift);
        setAnimatorParameters();
    }

    void FixedUpdate() {

        //update player position and rotation using rigid bodies
        m_rb.MovePosition(m_rb.position + m_move);
        RotationByInput();
    }
    
    //Manages Animation Parameters
    void setAnimatorParameters(){

        if (Input.GetKey(KeyCode.LeftShift)) {
            m_inputShift = true;
            m_playerSpeed = m_defaultSpeed * m_runMultiplier;
        }
        else {
            m_inputShift = false;
            m_playerSpeed = m_defaultSpeed;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) {
            m_inputxy = true;
        }
        else {
            m_inputxy = false;
        }
    }

    void RotationByInput(){

        var input_W = Input.GetKey(KeyCode.W);
        var input_A = Input.GetKey(KeyCode.A);
        var input_S = Input.GetKey(KeyCode.S);
        var input_D = Input.GetKey(KeyCode.D);
        
        var rot = m_rb.rotation;

        if (m_isAiming == false) {
            
            if (input_W && !input_A && !input_S && !input_D) {
                if (rot <= 175 && rot >= -175){
                    LerpRotation(0);
                }
                else if (rot > 175){
                    LerpRotation(360);
                }
                else if (rot < - 174){
                    LerpRotation(-360);
                }
            }

            if (input_W && input_A) {
               if (rot <= 220 && rot >= -220){
                   LerpRotation(45);
               }
               else if (rot > 220){
                   LerpRotation(360);
                   LerpRotation(45);
               }
               else if (rot < -220){
                   LerpRotation(-315);
               }
            }

            if (!input_W && input_A && !input_S && !input_D) {
                if (rot <= 265 && rot >= -265){
                   LerpRotation(90);
               }
               else if (rot > 265){
                   LerpRotation(360);
                   LerpRotation(90);
               }
               else if (rot < -265){
                   LerpRotation(-270);
               }
            }

            if (input_A && input_S) {
                if (rot <= 310 && rot >= -310){
                   LerpRotation(135);
               }
               else if (rot > 310){
                   LerpRotation(360);
                   LerpRotation(135);
               }
               else if (rot < -310){
                   LerpRotation(-225);
               }
            }

            if (!input_W && !input_A && input_S && !input_D) {
                if (rot >= -5 && rot <= 355){
                   LerpRotation(180);
               }
               else if (rot <= -5 && rot >= -355){
                   LerpRotation(-185);
               }            
            }

            if (input_S && input_D) {
                if (rot <= 310 && rot >= -310){
                   LerpRotation(-90);
               }
               else if (rot > 310){
                   LerpRotation(225);
               }
               else if (rot < -310){
                   LerpRotation(-360);
                   LerpRotation(-135);
               }                
            }

            if (!input_W && !input_A && !input_S && input_D) {
                if (rot <= 265 && rot >= -265){
                   LerpRotation(-90);
               }
               else if (rot > 265){
                   LerpRotation(270);
               }
               else if (rot < -265){
                   LerpRotation(-360);
                   LerpRotation(-90);
               }
            }

            if (input_D && input_W) {
                if (rot <= 220 && rot >= -220){
                   LerpRotation(-45);
               }
               else if (rot > 220){
                   LerpRotation(315);
               }
               else if (rot < -220){
                   LerpRotation(-360);
                   LerpRotation(-45);
               }
            }
        }

        else if (m_isAiming == true) {
            LerpRotation(m_aim.rotation + 5.00f);
        }
    }

    void LerpRotation(float target){
        var RotInterpolation = m_rotationSmoothing * Time.deltaTime;
        m_rb.rotation = Mathf.Lerp(m_rb.rotation, target, RotInterpolation);
    }

    void ResetRotation(){
        if(m_rb.rotation >= 355.00f || m_rb.rotation <= -355.00f){
            m_rb.rotation = 0.00f;
        }
    }
}