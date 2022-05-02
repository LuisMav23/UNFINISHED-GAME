using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableField : MonoBehaviour
{
    public List<GameObject> InteractableItems;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<Item>() != null){
            InteractableItems.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.GetComponent<Item>() != null){
            if (InteractableItems.Contains(other.gameObject)){
                InteractableItems.Remove(other.gameObject);
            }
        }
    }
}

