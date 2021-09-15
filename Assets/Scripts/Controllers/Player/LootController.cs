using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LootController : MonoBehaviour
{
    InventoryManager inventory;
    PlayerInput playerInput;
    void Awake()
    {
        inventory = GetComponent<InventoryManager>();
       
        playerInput = new PlayerInput();
        InitializeInput();
    }

    private void OnEnable()
    {
        playerInput.CharacterActionMap.Enable();
    }

    private void OnDisable()
    {
        playerInput.CharacterActionMap.Disable();
    }

    void InitializeInput()
    {
        playerInput.CharacterActionMap.Interact.started += Loot;

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
                
                Debug.Log(Vector3.Distance(hitCollider.transform.position, transform.position));
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
