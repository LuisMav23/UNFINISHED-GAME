using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableField : MonoBehaviour
{
    public static InteractableField Instance { get; private set;}
    [SerializeField] public List<GameObject> InteractableItems;
    public bool isMouseOnField = false;

    private void Awake() {
        if (Instance != null && Instance != this) { 
            Destroy(this); 
        } 
        else { 
            Instance = this; 
        } 
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<Item>() != null){
            InteractableItems.Add(other.gameObject);
        }

        else if (other.gameObject.tag == "Mouse" && other.isTrigger){
            isMouseOnField = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.GetComponent<Item>() != null){
            if (InteractableItems.Contains(other.gameObject)){
                InteractableItems.Remove(other.gameObject);
            }
        }

        else if (other.gameObject.tag == "Mouse" && other.isTrigger){
            isMouseOnField = false;
        }
    }
}

