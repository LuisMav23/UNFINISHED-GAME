using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject m_Player;
    [SerializeField] private float m_Threshold;
    [SerializeField] private float m_EagleEyeZoom;
    [SerializeField] private float m_EEInterpolation;

    private Camera m_cam; 
    private Vector3 m_mousePos;
    private Vector3 m_targetPos;
    
    void Awake() {
        m_cam = Camera.main;
    }
    
    void Update() {
        m_mousePos = (Vector2)m_cam.ScreenToWorldPoint(Input.mousePosition);
        m_targetPos = (m_Player.transform.position + m_mousePos) / 2.00f;

        m_targetPos.x = Mathf.Clamp(m_targetPos.x, m_Player.transform.position.x - m_Threshold, m_Player.transform.position.x + m_Threshold);
        m_targetPos.y = Mathf.Clamp(m_targetPos.y, m_Player.transform.position.y - m_Threshold, m_Player.transform.position.y + m_Threshold);
        gameObject.transform.position = m_targetPos;
    }

    void FixedUpdate(){
        EagleEyeCam();
    }

    //when alt is pressed expand field of view by enlarging ortho m_cam 
    void EagleEyeCam(){
        if (Input.GetKey("left alt")){
            m_cam.orthographicSize = Mathf.Lerp(m_cam.orthographicSize, m_EagleEyeZoom, m_EEInterpolation * Time.deltaTime);
        }
        else {
            m_cam.orthographicSize = Mathf.Lerp(m_cam.orthographicSize, 10.00f, m_EEInterpolation * Time.deltaTime);
        }
    }
}