using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlingShotController : MonoBehaviour
{
    [SerializeField]
    GameObject stonePrefab;
    [SerializeField]
    float throwingForce;
    [HideInInspector]
    public bool isCurrentlyAiming,isCurrentlyShooting;
    PlayerMovement playerMovement;
    PlayerInput playerInput;
    InventoryManager inventory;
    Animator animatorController;
    int aimHash, shootHash;
   
    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animatorController = GetComponent<Animator>();
        inventory = GetComponent<InventoryManager>();
        playerInput = new PlayerInput();
        InitializeInput();
        aimHash = Animator.StringToHash("AimTrigger");
        shootHash = Animator.StringToHash("ShootTrigger");
    }


    private void OnEnable()
    {
        playerInput.CharacterActionMap.Enable();
    }

    private void OnDisable()
    {
        playerInput.CharacterActionMap.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        if(isCurrentlyAiming)
            Turning();
    }
    void InitializeInput()
    {
        playerInput.CharacterActionMap.Shoot.started += AimStart;
        playerInput.CharacterActionMap.Shoot.canceled += Shoot;

    }


    void AimStart(InputAction.CallbackContext context)
    {
        if(inventory.stonesCollected > 0)
        {
            isCurrentlyAiming = true;
            animatorController.SetTrigger(aimHash);
        }
        
    }

    void Shoot(InputAction.CallbackContext context)
    {

        if (inventory.stonesCollected > 0)
        {
            animatorController.SetTrigger(shootHash);
            if (!isCurrentlyShooting)
            {
                StartCoroutine(StartShooting());
                inventory.stonesCollected--;
            }
        }
       
            
        
    }
    void Turning()
    {
        
        
        Ray camRay = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit))
        {
          
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 5 * Time.deltaTime);
        }
    }

    IEnumerator StartShooting()
    {
        isCurrentlyShooting = true;
        yield return new WaitForSeconds(0.6f);
        isCurrentlyAiming = false;
        Vector3 launchPos = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
        GameObject stone = Instantiate(stonePrefab, launchPos, Quaternion.identity);
        stone.GetComponent<Rigidbody>().AddRelativeForce(transform.forward * throwingForce, ForceMode.Impulse);
        isCurrentlyShooting = false;
    }
}
