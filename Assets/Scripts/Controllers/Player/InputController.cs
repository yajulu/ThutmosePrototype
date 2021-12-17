using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controllers.Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputController : MonoBehaviour
    {
        private PlayerInput _playerInput;

        private InputAction _moveAction;
        private InputAction _lookAction;
        private InputAction _fireAction;
        private InputAction _runAction;
        private InputAction _jumpAction;

        public bool run;
        public bool jump;
        public Vector2 Move { get; private set; }

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _moveAction = _playerInput.actions["Move"];
            _runAction = _playerInput.actions["Run"];
            _jumpAction = _playerInput.actions["Jump"];
        }

        private void OnEnable()
        {
            _runAction.started += RunActionOnStarted;
            _runAction.canceled += RunActionOnCanceled;
            _jumpAction.started += JumpActionOnStarted;
            _jumpAction.canceled += JumpActionOnCanceled;
        }
        
        private void OnDisable()
        {
            _runAction.started -= RunActionOnStarted;
            _runAction.canceled -= RunActionOnCanceled;
        }
        
        private void RunActionOnStarted(InputAction.CallbackContext obj)
        {
            run = true;
        }
        
        private void RunActionOnCanceled(InputAction.CallbackContext obj)
        {
            run = false;
        }
        
        private void JumpActionOnStarted(InputAction.CallbackContext obj)
        {
            jump = true;
        }
        
        private void JumpActionOnCanceled(InputAction.CallbackContext obj)
        {
            jump = false;
        }

        private void Update()
        {
            Move = _moveAction.ReadValue<Vector2>();
        }
    }
}
