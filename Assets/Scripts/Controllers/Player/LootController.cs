using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LootController : MonoBehaviour
{
    InventoryManager inventory;
    Old_PlayerInput _oldPlayerInput;
    void Awake()
    {
        inventory = GetComponent<InventoryManager>();
       
        _oldPlayerInput = new Old_PlayerInput();
        InitializeInput();
    }

    private void OnEnable()
    {
        _oldPlayerInput.CharacterActionMap.Enable();
    }

    private void OnDisable()
    {
        _oldPlayerInput.CharacterActionMap.Disable();
    }

    void InitializeInput()
    {
        _oldPlayerInput.CharacterActionMap.Interact.started += Loot;

    }

    void Loot(InputAction.CallbackContext context) //NEED TO ADD LAYERMASK FOR OPTIMIZATION
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.2f);
        Collider colliderToLoot = null;
        float minDistance = Mathf.Infinity;
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Collectible"))
            {
                

                if (Vector3.Distance(hitCollider.transform.position, transform.position) < minDistance)
                {
                   
                    minDistance = Vector3.Distance(hitCollider.transform.position, transform.position);
                    colliderToLoot = hitCollider;
                }
            }
           
        }
        if (colliderToLoot != null)
        {
            Destroy(colliderToLoot.gameObject);
            inventory.stonesCollected++;
        }
            
      
    }

    
}
