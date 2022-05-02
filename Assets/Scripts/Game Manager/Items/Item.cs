using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private GameObject PlayerHeldItem;
    [SerializeField] private string DefaultName;
    [SerializeField] private float dropDistance;

    private string Name;
    private bool isHeld = false;
    private bool isInteractable = true;
    
    private Sprite ItemSprite;
    private Transform itemTransform;
    private SpriteRenderer spriteRenderer;
    private ItemManager itemManager;
    private Collider2D[] colliders;
    private InteractableField intField;
    

    void Awake() {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        itemTransform = gameObject.GetComponent<Transform>();
        itemManager = GameObject.Find("Item Manager").GetComponent<ItemManager>();
        intField = GameObject.Find("Interactable Field").GetComponent<InteractableField>();
        colliders = gameObject.GetComponents<Collider2D>();

        itemTransform.localScale = new Vector3(6.00f, 6.00f, 1.00f);
    }

    void Update() {
        isInteractable = intField.InteractableItems.Contains(this.gameObject);
        
    }

    private void OnMouseOver() {
        //Debug.Log(this.gameObject.name + ": Can Pick Up");
        if (isInteractable){
            if (Input.GetButtonDown("Fire1") && isHeld == false){
                isHeld = true;
            }
        }
    }

    void setSprite(){
        foreach(Sprite sprite in this.itemManager.ItemSprites){
            if (sprite.name == this.Name) {
                this.ItemSprite = sprite;
                this.spriteRenderer.sprite = this.ItemSprite;
            }
        }
    }

    void PickUpItem(){
        if (isHeld == true){
            this.Name = "Held_" + this.Name;
            itemTransform.position = PlayerHeldItem.transform.position;
            itemTransform.rotation = PlayerHeldItem.transform.rotation;
            itemTransform.parent = PlayerHeldItem.transform;

            foreach(Collider2D col in colliders){
                col.enabled = false;
            }
            setSprite();
               
            if(Input.GetKeyDown("e")){
                
                itemTransform.position = PlayerHeldItem.transform.position + PlayerHeldItem.transform.forward * dropDistance;
                itemTransform.position -= new Vector3(0.00f, 0.00f, dropDistance);
                itemTransform.parent = null;
                isHeld = false;

                foreach(Collider2D col in colliders){
                    col.enabled = true;
                }
            }
        }
        else if (isHeld == false){
            this.Name = DefaultName;
            setSprite();
        }
    }
}