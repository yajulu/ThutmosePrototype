using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotor : MonoBehaviour
{
    private Animator _playerAnimator;
    private int _animatorX;
    private int _animatorY;
    private int _animatorIsRunning;
    private int _animatorIsCrouching;

    private Vector2 _moveInput;
    private Vector2 _animatorDirection;
    private Vector3 _moveDirection;
    private Vector3 _movementVelocity;
    private Vector3 _lookDirection;
    private bool _isRunning;

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
    

    [SerializeField] private float speed = 3f;
    [SerializeField] private float drag = 2.5f;
    [SerializeField, Range(0f, 1f)] private float dragInputFactor = 0.5f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float rotationSpeed = 200f;
    [SerializeField] private float jumpSpeed = 10f;
    [SerializeField, Range(1, 3)] private float runModifier;
    
    private void Awake()
    {
        _animatorX = Animator.StringToHash("x");
        _animatorY = Animator.StringToHash("z");
        _animatorIsRunning = Animator.StringToHash("isRunning");
        _animatorIsCrouching = Animator.StringToHash("isCrouching");
        
        _oldPlayerInput = GetComponent<PlayerInput>();
        _controller = GetComponent<CharacterController>();
        _mainCamera = Camera.main;
        _mainCameraTransform = _mainCamera?.gameObject.transform;
        _moveAction = _oldPlayerInput.actions["Move"];
        _runAction = _oldPlayerInput.actions["Run"];
        _jumpAction = _oldPlayerInput.actions["Jump"];
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

    private void RunActionOnCanceled(InputAction.CallbackContext obj)
    {
        _isRunning = false;
    }

    private void RunActionOnStarted(InputAction.CallbackContext obj)
    {
        _isRunning = true;
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
        if (!_controller.isGrounded)
        {
            //Apply Gravity
            _movementVelocity += Physics.gravity * (Time.fixedTime);    
        }
        else
        {
            _movementVelocity.y = 0; //ignore vertical velocity on ground
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
            if (_jumpAction.ReadValue<float>() > 0.5f)
            {
                Debug.Log("Jump");
                _movementVelocity.y += jumpSpeed;
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
        _movementVelocity -= _movementVelocity.normalized * ( (1 - (_moveDirection.magnitude * dragInputFactor)) * drag * Time.deltaTime); 
        
        _movementVelocity = Vector3.ClampMagnitude(_movementVelocity, _isRunning? maxSpeed * runModifier : maxSpeed);   
        _movementVelocity.y = verticalVelocity;
        _collisionFlags = _controller.Move(_movementVelocity * Time.deltaTime);
    }

    private void UpdateAnimator()
    {
        _playerAnimator.SetFloat(_animatorX, _animatorDirection.x, 0.1f, Time.deltaTime);
        _playerAnimator.SetFloat(_animatorY, _animatorDirection.y, 0.1f, Time.deltaTime);
        _playerAnimator.SetBool(_animatorIsRunning, _isRunning);
    }
    
    
}
