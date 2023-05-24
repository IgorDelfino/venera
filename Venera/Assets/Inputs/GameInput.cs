using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

        private PlayerInputActions playerInputActions;

        [Header("Character Input Values")]
        [SerializeField] private Vector2 move;
        [SerializeField] private Vector2 look;
        [SerializeField] private float vertical;


        [Header("Mouse Cursor Settings")]
        [SerializeField] private bool cursorLocked = true;

        private void Awake()
        {
            Instance = this;

            playerInputActions = new PlayerInputActions();
            playerInputActions.Player.Enable();

            playerInputActions.Player.Interact.performed += Interact_performed;
            playerInputActions.Player.Pause.performed += Pause_performed;
        }

        private void OnDestroy() {
            playerInputActions.Player.Interact.performed -= Interact_performed;
            playerInputActions.Player.Pause.performed -= Pause_performed;

            playerInputActions.Dispose();
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
            move = playerInputActions.Player.Move.ReadValue<Vector2>();
            return move;
        }

        public Vector2 GetLook()
        {
            look = playerInputActions.Player.Look.ReadValue<Vector2>();
            return look;
        }

        public float GetVertical()
        {
            vertical = playerInputActions.Player.Vertical.ReadValue<float>();
            return vertical;
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            SetCursorLockState(cursorLocked);
        }

        public void SetCursorLockState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }

        public void SetPlayerMapEnabled(bool state){
            if(state){
                playerInputActions.Player.Enable();
            } else {
                playerInputActions.Player.Disable();
            }
        }

    }
}