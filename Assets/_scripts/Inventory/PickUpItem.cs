using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [Header("<потеряно>")]
    public float radius;
    public InventoryManager inventoryManager;
    public void PickUp()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.GetComponent<ItemObject>() != null)
            {
                inventoryManager.AddItem(hitCollider.GetComponent<ItemObject>());
                break;
            }
        }
    }
}
