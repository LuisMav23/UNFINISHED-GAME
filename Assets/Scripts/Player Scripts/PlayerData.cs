using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData
{
    private const int c_INVENTORY_SLOTS = 6;
    private static int PLAYER_HEALTH;
    private static GameObject[] Inventory = new GameObject[c_INVENTORY_SLOTS];

    public static void AddInventoryItem(GameObject obj){
        for (int i = 0; i < Inventory.Length; i++){
            if (Inventory[i] == null){
                Inventory[i] = obj;
                return;
            }
        }
        Debug.Log("Inventory Full");
    }

    public static void RemoveInventoryItem(GameObject obj){
        for (int i = 0; i < Inventory.Length; i++){
            if (Inventory[i] == obj){
                Inventory[i] = null;
                break;
            }
        }
    }

    public static bool InventoryContains(GameObject obj){
        foreach (GameObject Item in Inventory){
            if (Item == obj){
                return true;
            }
        }
        return false;
    }

    public static GameObject getInventoryItem(int index){
        return Inventory[index];
    }
}