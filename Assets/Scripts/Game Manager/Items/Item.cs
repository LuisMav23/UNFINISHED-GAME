using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private GameObject m_PlayerHeldItem;
    [SerializeField] private Camera m_cam;
    [SerializeField] private string m_DefaultName;
    [SerializeField] private Sprite[] m_ItemSprites;
    
    private Sprite m_ItemSprite;
    private Transform m_itemTransform;
    private SpriteRenderer m_spriteRenderer;
    private Collider2D[] m_colliders;
    
    private string m_Name;
    private bool m_isHeld = false;
    private bool m_isInteractable = false;
    private bool m_isDropping = false;

    void Awake() {

        m_spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        m_itemTransform = gameObject.GetComponent<Transform>();
        m_colliders = gameObject.GetComponents<Collider2D>();

        m_itemTransform.localScale = new Vector3(7.00f, 7.00f, 1.00f);
    }

    void Update() {

        m_isInteractable = InteractableField.Instance.InteractableItems.Contains(this.gameObject);

        PickUpItem();
        DropItem();
    }

    private void OnMouseOver() {

        if (m_isInteractable){
            if (Input.GetButtonDown("Fire1") && m_isHeld == false){
                m_isHeld = true;
            }
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

    void PickUpItem(){

        if (m_isHeld == true){

            this.m_Name = "Held_" + this.m_Name;
            m_itemTransform.position = m_PlayerHeldItem.transform.position;
            m_itemTransform.rotation = m_PlayerHeldItem.transform.rotation;
            m_itemTransform.parent = m_PlayerHeldItem.transform;

            foreach(Collider2D col in m_colliders){
                col.enabled = false;
            }
            setSprite();
        }
        else if (m_isHeld == false){
            this.m_Name = m_DefaultName;
            setSprite();
        }
    }

    void DropItem(){
        var mousePos = m_cam.ScreenToWorldPoint(Input.mousePosition);
        var HeldItemPos = (Vector2)m_PlayerHeldItem.transform.position;
        var HeldOffset = HeldItemPos + new Vector2(4.00f, 2.00f);

        if(Input.GetKeyDown(KeyCode.E) && m_isHeld == true){
            m_isHeld = false;
            m_isDropping = true;
        }

        if (m_isDropping){
            m_itemTransform.parent = null;
            m_itemTransform.position = Vector2.Lerp(m_itemTransform.position, HeldOffset, 3 * Time.deltaTime);
    
            if (Input.GetButtonDown("Fire1")){

                if (InteractableField.Instance.isMouseOnField){
                    m_isDropping = false;
                    m_itemTransform.position = (Vector2)mousePos;

                    foreach(Collider2D col in m_colliders){
                        col.enabled = true;
                    }
                }   
                else if (!InteractableField.Instance.isMouseOnField) {
                    Debug.Log("Unable to drop");
                }
            }
            else if (Input.GetButtonDown("Fire2")){
                m_isHeld = true;
                m_isDropping = false;
            }
        }
    }
}

