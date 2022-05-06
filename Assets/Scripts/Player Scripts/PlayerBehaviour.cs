using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
   
    [SerializeField] private GameObject m_PlayerHeldItem;
    private GameObject m_HeldItem;
    private bool m_isInventoryOpen = false;
    void Awake() {
        // PlayerData.PLAYER_HEALTH = 100;
    }

    void Update() {
        Debug.Log(m_HeldItem);
        ItemPickUp();
        ItemDrop();
        openInventory();
    }

    void FixedUpdate() {
        
        setItemPos();
    }

    public GameObject getHeldItem(){
        return m_HeldItem;
    }

    void ItemPickUp(){
        var InteractableItems = InteractableField.Instance.InteractableItems;

        if (InteractableItems != null){
            foreach (GameObject item in InteractableField.Instance.InteractableItems) {

                var isInteractable = item.GetComponent<Item>().m_isMouseOver;

                if (isInteractable){

                    if (Input.GetButtonDown("Fire1")){
                        
                        if (m_HeldItem == null){
                            m_HeldItem = item;
                            PlayerData.AddInventoryItem(item);
                        }
                        else{
                            PlayerData.AddInventoryItem(item);
                        }
                    }
                }
            }
        }
    }

    void ItemDrop(){
        if (m_HeldItem != null){
            var ItemScript = m_HeldItem.GetComponent<Item>();       
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetKeyDown(KeyCode.E) && ItemScript.m_isHeld){

                ItemScript.m_isDropping = true;
            }

            if (ItemScript.m_isDropping){

                if (Input.GetButtonDown("Fire1")){

                    if (InteractableField.Instance.isMouseOnField){

                        m_HeldItem.transform.position = (Vector2)mousePos;  
                        ItemScript.m_isDropping = false;
                        PlayerData.RemoveInventoryItem(m_HeldItem);
                        m_HeldItem = null;
                    }   
                    else if (!InteractableField.Instance.isMouseOnField) {

                        Debug.Log("Unable to drop");
                    }
                }
                else if (Input.GetButtonDown("Fire2")){
                    ItemScript.m_isDropping = false;
                }
            }
        }
    }

    void setItemPos(){
        if (m_HeldItem != null) {

            var isDropping = m_HeldItem.GetComponent<Item>().m_isDropping;
            var isStored = m_HeldItem.GetComponent<Item>().m_isStored;
            var isHeld = m_HeldItem.GetComponent<Item>().m_isHeld;

            var HeldOffset = (Vector2)m_PlayerHeldItem.transform.position + new Vector2(4.00f, 4.00f);

            if (isHeld && !isDropping){

                m_HeldItem.transform.position = m_PlayerHeldItem.transform.position;
                m_HeldItem.transform.rotation = m_PlayerHeldItem.transform.rotation;
                m_HeldItem.transform.parent = m_PlayerHeldItem.transform;
            }        
            else if (isHeld && isStored && isDropping){

                m_HeldItem.transform.parent = null;
                m_HeldItem.transform.position = Vector2.Lerp(m_HeldItem.transform.position, HeldOffset, 3 * Time.deltaTime);
            }
        }
    }

    void openInventory(){
        
        if (Input.GetKeyDown(KeyCode.Tab)){
            
            m_isInventoryOpen = true;
        }

        if (m_isInventoryOpen){
            
            if (Input.GetKeyDown(KeyCode.Alpha1)){
                m_HeldItem = PlayerData.getInventoryItem(0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2)){
                m_HeldItem = PlayerData.getInventoryItem(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3)){
                m_HeldItem = PlayerData.getInventoryItem(2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4)){
                m_HeldItem = PlayerData.getInventoryItem(3);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5)){
                m_HeldItem = PlayerData.getInventoryItem(4);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6)){
                m_HeldItem = PlayerData.getInventoryItem(5);
            }
            else if (Input.GetKeyDown(KeyCode.Escape)){
                Time.timeScale = 1;
                m_isInventoryOpen = false;
            }
        }
    }
}
