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
    [SerializeField]
    LayerMask targetLayer;
    [HideInInspector]
    public bool isCurrentlyAiming,isCurrentlyShooting;
    PlayerMovement playerMovement;
    Old_PlayerInput _oldPlayerInput;
    InventoryManager inventory;
    Animator animatorController;
    int aimHash, shootHash;
    RaycastHit boxHit;
    bool isHitDetect;
   
    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animatorController = GetComponent<Animator>();
        inventory = GetComponent<InventoryManager>();
        _oldPlayerInput = new Old_PlayerInput();
        InitializeInput();
        aimHash = Animator.StringToHash("AimTrigger");
        shootHash = Animator.StringToHash("ShootTrigger");
    }


    private void OnEnable()
    {
        _oldPlayerInput.CharacterActionMap.Enable();
    }

    private void OnDisable()
    {
        _oldPlayerInput.CharacterActionMap.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        if(isCurrentlyAiming)
            Turning();
    }
    void InitializeInput()
    {
        _oldPlayerInput.CharacterActionMap.Shoot.started += AimStart;
        _oldPlayerInput.CharacterActionMap.Shoot.canceled += Shoot;

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
        isHitDetect = Physics.BoxCast(transform.position, transform.localScale * 10, transform.forward, out boxHit, transform.rotation, 2000, targetLayer);
        Vector3 forceDir = transform.forward;
        if (isHitDetect)
        {
            //Output the name of the Collider your Box hit
            Debug.Log("Hit : " + boxHit.collider.name);
            forceDir = Vector3.Normalize( boxHit.point - transform.position);
        }
    

   
    isCurrentlyAiming = false;
        Vector3 launchPos = new Vector3(transform.position.x, transform.position.y  +  1, transform.position.z);
        GameObject stone = Instantiate(stonePrefab, launchPos, Quaternion.identity);
        stone.GetComponent<Rigidbody>().AddRelativeForce(forceDir * throwingForce, ForceMode.Impulse);
        isCurrentlyShooting = false;
    }

   
   
}
