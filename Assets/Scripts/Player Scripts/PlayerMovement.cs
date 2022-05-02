using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float defaultSpeed;
    [SerializeField] public float runMultiplier;
    [SerializeField] public Animator animator;

    private float playerSpeed;

    public static Collider2D InteractableField;
    
    private Vector2 Move;
    private Rigidbody2D rb;
    private Rigidbody2D Aim;

    private bool Inputxy = false;
    private bool InputShift = false;
    void Start() {
        //Reference rigidbodies
        rb = gameObject.GetComponent<Rigidbody2D>();
        Aim = GameObject.FindGameObjectWithTag("Aim").GetComponent<Rigidbody2D>();
        var childrenCollider = gameObject.GetComponentsInChildren<CircleCollider2D>();
        foreach (CircleCollider2D collider in childrenCollider){
            if (collider.gameObject.name == "Interactable Field"){
                InteractableField = collider;
            }
        }
    }

    void Update() {
        //instantiate horizontal end vertical axis Inputs
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Move = new Vector2 (horizontal, vertical) * playerSpeed * Time.fixedDeltaTime;

        //sets Animator parameters value
        animator.SetBool("Input xy", Inputxy);
        animator.SetBool("InputShift", InputShift);
        setAnimatorParameters();
    }

    void FixedUpdate() {
        //update player position and rotation using rigid bodies
        rb.MovePosition(rb.position + Move);
        rb.rotation = Aim.rotation;
    }
    
    //Manages Animation Parameters
    void setAnimatorParameters(){
        if (Input.GetKey("left shift")) {
            InputShift = true;
            playerSpeed = defaultSpeed * runMultiplier;
        }
        else {
            InputShift = false;
            playerSpeed = defaultSpeed;
        }

        if (Input.GetKey("a") || Input.GetKey("d") || Input.GetKey("w") || Input.GetKey("s")) {
            Inputxy = true;
        }
        else {
            Inputxy = false;
        }
    }
}