using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    public float interactionRange = 2f; // Adjust this value according to your scene

    public DummyInventory inventory;

    private void Start()
    {


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            TryInteract();
        }
    }

    private void TryInteract()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionRange))
        {
            if (hit.collider.CompareTag("Object"))
            {
                ItemSO item = hit.collider.GetComponent<ItemSO>(); // Assuming ItemSO is attached to the object
                if (item != null)
                {
                    AddItemToInventory(item);
                }
            }
        }
    }

    private void AddItemToInventory(ItemSO item)
    {
        // Check if the item is already in the inventory
        if (inventory != null && !inventory.itemList.Contains(item))
        {
            inventory.itemList.Add(item);
            // Optionally, you can update UI elements here
            Debug.Log(item.itemName + " added to inventory.");
        }
        else
        {
            Debug.Log(item.itemName + " is already in the inventory.");
        }
    }
}
