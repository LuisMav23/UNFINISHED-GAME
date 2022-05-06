using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private string m_DefaultName;
    [SerializeField] private Sprite[] m_ItemSprites;
    
    private GameObject m_Player;
    private Sprite m_ItemSprite;
    private SpriteRenderer m_spriteRenderer;
    private Collider2D[] m_colliders;
    
    private string m_Name;
    public bool m_isStored = false;
    public bool m_isHeld = false;
    public bool m_isInteractable = false;
    public bool m_isMouseOver = false;
    public bool m_isDropping = false;

    void Awake() {

        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        m_colliders = gameObject.GetComponents<Collider2D>();

        gameObject.transform.localScale = new Vector3(7.00f, 7.00f, 1.00f);
    }

    void Update() {

        m_isInteractable = InteractableField.Instance.InteractableItems.Contains(this.gameObject);

        if (PlayerData.InventoryContains(gameObject)){
            m_isStored = true;
        }
        else{
            m_isStored = false;
        }

        ManageProperty();
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag == "Mouse"){
            if (m_isInteractable){
                m_isMouseOver = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Mouse"){
            m_isMouseOver = false;
        }
    }

    void setSprite(){
        
        foreach(Sprite sprite in m_ItemSprites){
            if (sprite.name == this.m_Name) {
                this.m_ItemSprite = sprite;
                this.m_spriteRenderer.sprite = this.m_ItemSprite;
            }
        }
    }

    void ManageProperty(){

        if (m_isHeld && m_isStored){
            
            foreach(Collider2D col in m_colliders){
                col.enabled = false;
            }
            setSprite();
        }
        else if (!m_isHeld && !m_isStored){
            this.m_Name = m_DefaultName;
            foreach(Collider2D col in m_colliders){
                col.enabled = true;
            }
            setSprite();
        }

        if (m_isDropping){
            gameObject.GetComponent<Rigidbody2D>().rotation += 180.00f * Time.deltaTime;
            if (gameObject.GetComponent<Rigidbody2D>().rotation >= 360.00f){
                gameObject.GetComponent<Rigidbody2D>().rotation = 0;
            }
        }

        if (m_isStored && !m_isHeld && !m_isDropping){
            
            gameObject.transform.position = m_Player.transform.position;
            gameObject.transform.parent = m_Player.transform;
            m_spriteRenderer.sprite = null;
        }

        if(m_Player.GetComponent<PlayerBehaviour>().getHeldItem() == gameObject){
            m_isHeld = true;
        }
        else{
            m_isHeld = false;
        }
    }
}

