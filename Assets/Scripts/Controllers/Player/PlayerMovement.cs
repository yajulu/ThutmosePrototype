using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    Old_PlayerInput _oldPlayerInput;
    CharacterController characterController;
    GameObject trailObject;
    SlingShotController slingShotController;

    #region Movement
    [SerializeField]
    float playerSpeed;
    [SerializeField]
    float playerSprintSpeed;
    [SerializeField]
    float dodgeSpeed;
    [SerializeField]
    float dodgeCooldown;
    bool isDodgeAllowed;
    float dodgeSpeedMult;
   
    Vector2 currentMovementInput;
    Vector3 playerDirection;
    bool isMovementPressed;
    bool isRunningPressed;

    #endregion

    #region Animation
    Animator animatorController;
    int animSpeedHash, dodgeHash;

    #endregion

    #region Rotation
    Vector3 positionToLookAt;
    Quaternion currentRotation;
    Quaternion targetRotation;
    [SerializeField]
    float rotationFactorPerFrame;

    #endregion





    private void Awake()
    {
        _oldPlayerInput = new Old_PlayerInput();
        characterController = GetComponent<CharacterController>();
        animatorController = GetComponent<Animator>();
        slingShotController = GetComponent<SlingShotController>();
        trailObject = transform.GetChild(3).gameObject;
        isDodgeAllowed = true;
        InitializeInput();
        InitializeAnimation();

    }

  

    private void OnEnable()
    {
        _oldPlayerInput.CharacterActionMap.Enable();
    }

    private void OnDisable()
    {
        _oldPlayerInput.CharacterActionMap.Disable();
    }
    void Update()
    {
        if (!slingShotController.isCurrentlyAiming)
        {
            Move();
            Rotate();
            Animate();
        }
     
        
    }

    private void Move()
    {
        if(!isRunningPressed)
            characterController.Move(playerDirection * Time.deltaTime * playerSpeed * dodgeSpeedMult);
        else if(isRunningPressed)
            characterController.Move(playerDirection * Time.deltaTime * playerSprintSpeed * dodgeSpeedMult);

      
    }   


    void Animate()
    {
        if(isMovementPressed && !isRunningPressed)
        {
            animatorController.SetFloat(animSpeedHash, 0.5f, 0.1f, Time.deltaTime);
        }
        else if (!isMovementPressed)
        {
            animatorController.SetFloat(animSpeedHash, 0f, 0.1f, Time.deltaTime);
        }
        else if(isMovementPressed && isRunningPressed)
        {
            animatorController.SetFloat(animSpeedHash, 1f, 0.1f, Time.deltaTime);
        }
       
    }
    void Rotate()
    {
        positionToLookAt.x = playerDirection.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = playerDirection.z;

        currentRotation = transform.rotation;
        if (isMovementPressed)
        {
            targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
       
    }

    public void Stop()
    {
       
        playerDirection.x = 0;
        playerDirection.z = 0;
        isMovementPressed = false;
        isRunningPressed = false;
        this.enabled = false;
    }

    void InitializeInput()
    {
        dodgeSpeedMult = 1;
        _oldPlayerInput.CharacterActionMap.Move.started += OnMovementInput;
        _oldPlayerInput.CharacterActionMap.Move.canceled += OnMovementInput;
        _oldPlayerInput.CharacterActionMap.Move.performed += OnMovementInput;
        _oldPlayerInput.CharacterActionMap.Run.started += OnRunInput;
        _oldPlayerInput.CharacterActionMap.Run.canceled += OnRunInput;
        _oldPlayerInput.CharacterActionMap.Dodge.started += OnDodgeInput;
      

    }

    private void InitializeAnimation()
    {
     
        animSpeedHash = Animator.StringToHash("Speed");
        dodgeHash = Animator.StringToHash("Dodge");
        
    }


    void OnMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        playerDirection.x = currentMovementInput.x;
        playerDirection.z = currentMovementInput.y;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
      
    }
    void OnRunInput(InputAction.CallbackContext context)
    {

        isRunningPressed = context.ReadValueAsButton();
    }

    void OnDodgeInput(InputAction.CallbackContext context)
    {
        if (isDodgeAllowed)
        {
            StartCoroutine(DodgeCooldown());
            StartCoroutine(DodgeSpeedAdjustor());
        }
      
        
    }

    IEnumerator DodgeSpeedAdjustor()
    {
        trailObject.SetActive(false);
        animatorController.SetTrigger(dodgeHash);
        dodgeSpeedMult = dodgeSpeed;
        yield return new WaitForSeconds(0.2f);
        dodgeSpeedMult = 1;
        trailObject.SetActive(true);
       
        
    }

    IEnumerator DodgeCooldown()
    {
        if (isDodgeAllowed)
        {
            isDodgeAllowed = false;
            float time = dodgeCooldown;
            while (time >= 0f)
            {
                time -= Time.deltaTime;
                yield return null;
            }

            isDodgeAllowed = true;
        }
       
    }



}
