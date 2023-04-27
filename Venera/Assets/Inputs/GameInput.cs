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
        private PlayerInputActions playerInputActions;

        [Header("Character Input Values")]
        [SerializeField] private Vector2 move;
        [SerializeField] private Vector2 look;
        [SerializeField] private float vertical;


        [Header("Mouse Cursor Settings")]
        [SerializeField] private bool cursorLocked = true;

        private void Awake()
        {
            playerInputActions = new PlayerInputActions();
            playerInputActions.Player.Enable();
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
            SetCursorState(cursorLocked);
        }

        public void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }

    }
}