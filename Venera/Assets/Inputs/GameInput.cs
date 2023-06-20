using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Venera
{
    [RequireComponent(typeof(PlayerInput))]
    public class GameInput : MonoBehaviour
    {

        public static GameInput Instance { get; private set; }

        public event EventHandler OnInteractAction;
        public event EventHandler OnPauseAction;

        private PlayerInputActions _playerInputActions;

        [Header("Character Input Values")]
        [SerializeField] private Vector2 _move;
        [SerializeField] private Vector2 _look;
        [SerializeField] private float _vertical;


        [Header("Mouse Cursor Settings")]
        [SerializeField] private bool _cursorLocked = true;

        private void Awake()
        {
            Instance = this;

            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Player.Enable();

            _playerInputActions.Player.Interact.performed += Interact_performed;
            _playerInputActions.Player.Pause.performed += Pause_performed;
        }

        private void OnDestroy() {
            _playerInputActions.Player.Interact.performed -= Interact_performed;
            _playerInputActions.Player.Pause.performed -= Pause_performed;

            _playerInputActions.Dispose();
        }

        private void Pause_performed(InputAction.CallbackContext obj)
        {
            OnPauseAction?.Invoke(this, EventArgs.Empty);
        }

        private void Interact_performed(InputAction.CallbackContext obj)
        {
            OnInteractAction?.Invoke(this, EventArgs.Empty);
        }

        public Vector2 GetMove()
        {
            _move = _playerInputActions.Player.Move.ReadValue<Vector2>();
            return _move;
        }

        public Vector2 GetLook()
        {
            _look = _playerInputActions.Player.Look.ReadValue<Vector2>();
            return _look;
        }

        public float GetVertical()
        {
            _vertical = _playerInputActions.Player.Vertical.ReadValue<float>();
            return _vertical;
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            SetCursorLockState(_cursorLocked);
        }

        public void SetCursorLockState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }

        public void SetPlayerMapEnabled(bool state){
            if(state){
                _playerInputActions.Player.Enable();
            } else {
                _playerInputActions.Player.Disable();
            }
        }

    }
}