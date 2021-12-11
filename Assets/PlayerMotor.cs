using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using Unity.Collections;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotor : MonoBehaviour
{
    private Animator _playerAnimator;
    private int _animatorX;
    private int _animatorY;
    private int _animatorIsRunning;
    private int _animatorIsJumping;
    private int _animatorIsCrouching;
    private int _animatorPlayerAltitude;

    private Vector2 _moveInput;
    private Vector2 _animatorDirection;
    private Vector3 _moveDirection;
    private Vector3 _movementVelocity;
    private Vector3 _lookDirection;
    [SerializeField, ReadOnly] private bool isRunning;

    private float _accumulator;

    private PlayerInput _oldPlayerInput;

    private InputAction _moveAction;
    private InputAction _lookAction;
    private InputAction _fireAction;
    private InputAction _runAction;
    private InputAction _jumpAction;

    private CharacterController _controller;
    private CollisionFlags _collisionFlags;
    
    private Camera _mainCamera;
    private Transform _mainCameraTransform;

    private Ray _groundCheckRay;
    private RaycastHit _groundRaycastHit;
    

    [SerializeField] private float speed = 3f;
    [SerializeField] private float groundDrag = 10f;
    [SerializeField] private float airDrag = 15f;
    [SerializeField, Range(0f, 1f)] private float dragInputFactor = 0.5f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float rotationSpeed = 200f;
    [SerializeField] private float jumpSpeed = 10f;
    [SerializeField, Range(0f, 1f)] private float jumpUpFactor = 0.7f;
    [SerializeField, Range(1f, 3f)] private float runModifier;

    [SerializeField] private float gravity = -9.8f;

    [SerializeField] private float maxJumpHeight = 2f;
    [SerializeField] private float maxJumpTime = 1f;
    [SerializeField, ReadOnly] private float initialJumpingVelocity;
    [SerializeField, ReadOnly] private float playerAltitude;
    
    [SerializeField, ReadOnly] private bool isJumpPressed = false;
    [SerializeField, ReadOnly] private bool isJumping = false;
    [SerializeField, ReadOnly] private bool hasJumped = false;
    [SerializeField, ReadOnly] private bool hasLanded = false;
    

    private void Awake()
    {
        _animatorX = Animator.StringToHash("x");
        _animatorY = Animator.StringToHash("z");
        _animatorIsRunning = Animator.StringToHash("isRunning");
        _animatorIsJumping = Animator.StringToHash("isJumping");
        _animatorIsCrouching = Animator.StringToHash("isCrouching");
        _animatorPlayerAltitude = Animator.StringToHash("playerAltitude");
        
        _oldPlayerInput = GetComponent<PlayerInput>();
        _controller = GetComponent<CharacterController>();
        _mainCamera = Camera.main;
        _mainCameraTransform = _mainCamera?.gameObject.transform;
        _moveAction = _oldPlayerInput.actions["Move"];
        _runAction = _oldPlayerInput.actions["Run"];
        _jumpAction = _oldPlayerInput.actions["Jump"];
        InitializeJumpVariables();
    }

    private void OnEnable()
    {
        _playerAnimator = GetComponent<Animator>();
        _animatorDirection = Vector2.zero;
        _moveDirection = Vector3.zero;
        _movementVelocity = Vector3.zero;
        _runAction.started += RunActionOnStarted;
        _runAction.canceled += RunActionOnCanceled;
    }

    private void OnDisable()
    {
        _runAction.started -= RunActionOnStarted;
        _runAction.canceled -= RunActionOnCanceled;
    }

    private void InitializeJumpVariables()
    {
        var timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight / Mathf.Pow(timeToApex, 2));
        initialJumpingVelocity = (2 * maxJumpHeight) / timeToApex;
    }
    

    private void RunActionOnCanceled(InputAction.CallbackContext obj)
    {
        isRunning = false;
    }

    private void RunActionOnStarted(InputAction.CallbackContext obj)
    {
        isRunning = true;
    }
    
    

    private void Update()
    {
        UpdateInput();  
        UpdateAnimator();
        // if (Input.GetKeyDown(KeyCode.R))
        // {
        //     switch (_playerAnimator.recorderMode)
        //     {
        //         case AnimatorRecorderMode.Offline:
        //             _playerAnimator.StartRecording(200);
        //             break;
        //         case AnimatorRecorderMode.Playback:
        //             break;
        //         case AnimatorRecorderMode.Record:
        //             _playerAnimator.StopRecording();
        //             break;
        //         default:
        //             throw new ArgumentOutOfRangeException();
        //     }
        // }
        // else if(Input.GetKeyDown(KeyCode.T) && _playerAnimator.recorderMode == AnimatorRecorderMode.Record)
        // {
        //     _playerAnimator.StartPlayback();
        //     _accumulator -= 200;
        // }

        if (_playerAnimator.recorderMode == AnimatorRecorderMode.Playback)
        {
            _accumulator -= Time.deltaTime;
            _playerAnimator.playbackTime = _accumulator;    
        }
    }

    private void FixedUpdate()
    {
        if (_controller.isGrounded)
        {
            _movementVelocity.y = 0; //ignore vertical velocity on ground
        }
        else
        {
            //Apply Gravity
            _movementVelocity.y += gravity * Time.deltaTime;
        }

        GroundCheck();
    }

    private void GroundCheck()
    {
        var transform1 = transform;
        _groundCheckRay.origin = transform1.position;
        _groundCheckRay.direction = -transform1.up;
        if (Physics.SphereCast(_groundCheckRay, 0.2f, out _groundRaycastHit, 10f))
        {
            Debug.Log($"Jumping: {playerAltitude}");
            playerAltitude = transform.position.y - _groundRaycastHit.point.y;
        }
    }

    private void UpdateInput()
    {
        //Read the user input
        _moveInput = _moveAction.ReadValue<Vector2>();
        
        //Update the animation direction
        _animatorDirection = _moveInput;
        
        //Update the movement direction
        var cameraForward = _mainCameraTransform.forward;
        var verticalVelocity = _movementVelocity.y;
        
        _moveDirection = transform.right * _moveInput.x + transform.forward * _moveInput.y;
        _moveDirection.y = 0;
        _movementVelocity.y = verticalVelocity;

        _moveDirection.Normalize();
        
        //Rotate the Player direction to movement Direction
        _lookDirection = Vector3.Lerp(transform.forward, cameraForward, Time.deltaTime * rotationSpeed * _moveDirection.magnitude).ProjectOntoPlane(Vector3.up).normalized;
        transform.rotation = Quaternion.LookRotation(_lookDirection, Vector3.up);

        if (_controller.isGrounded)
        {
            if (_jumpAction.ReadValue<float>() > 0.5f && !isJumping)
            {
                Debug.Log("Jump");
                isJumping = true;
                _movementVelocity += ((_moveDirection * (1 - jumpUpFactor))  + (Vector3.up * jumpUpFactor)).normalized * initialJumpingVelocity;
            }
            else
            {
                //Reset Jumping Flag
                isJumping = false;
            }
        }
        else
        {
            //Air Borne
            
        }
        
        //Apply the movement
        verticalVelocity = _movementVelocity.y;
        _movementVelocity.y = 0;
        _movementVelocity += _moveDirection * (speed * Time.deltaTime);
        
        //Applying Drag
        _movementVelocity.y = 0;
        _movementVelocity -= _movementVelocity.normalized *
                             ((1 - (_moveDirection.magnitude * dragInputFactor)) * (_controller.isGrounded ? groundDrag : airDrag) * Time.deltaTime); 
        
        _movementVelocity = Vector3.ClampMagnitude(_movementVelocity, isRunning? maxSpeed * runModifier : maxSpeed);   
        _movementVelocity.y = verticalVelocity;
        _collisionFlags = _controller.Move(_movementVelocity * Time.deltaTime);
    }

    private void UpdateAnimator()
    {
        _playerAnimator.SetFloat(_animatorX, _animatorDirection.x, 0.1f, Time.deltaTime);
        _playerAnimator.SetFloat(_animatorY, _animatorDirection.y, 0.1f, Time.deltaTime);
        _playerAnimator.SetFloat(_animatorPlayerAltitude, playerAltitude);
        _playerAnimator.SetBool(_animatorIsRunning, isRunning);
        _playerAnimator.SetBool(_animatorIsJumping, isJumping);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, _movementVelocity);
        Gizmos.DrawRay(transform.position, _movementVelocity);
    }
}
