using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] public GameObject Player;
    [SerializeField] public Camera cam;
    [SerializeField] public float Threshold;
    [SerializeField] public float eagleEyeZoom;
    [SerializeField] public float EEInterpolation;

    private Vector3 mousePos;
    private Vector3 targetPos;
    
    void Update() {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        targetPos = (Player.transform.position + mousePos) / 2.00f;

        targetPos.x = Mathf.Clamp(targetPos.x, -Threshold + Player.transform.position.x, Threshold + Player.transform.position.x);
        targetPos.y = Mathf.Clamp(targetPos.y, -Threshold + Player.transform.position.y, Threshold + Player.transform.position.y);
        gameObject.transform.position = targetPos;
    }

    void FixedUpdate(){
        EagleEyeCam();
    }

    //when alt is pressed expand field of view by enlarging ortho cam 
    void EagleEyeCam(){
        if (Input.GetKey("left alt")){
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, eagleEyeZoom, EEInterpolation * Time.deltaTime);
        }
        else{
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 10.00f, EEInterpolation * Time.deltaTime);
        }
    }
}