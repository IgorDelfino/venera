using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Venera
{
    public class TestDroneController : MonoBehaviour
    {
        [SerializeField] private GameInput gameInput;

        [Header("Move Speeds")]
        [SerializeField] private float forwardSpeed = 4f;
        [SerializeField] private float horizontalSpeed = 4f;
        [SerializeField] private float verticalSpeed = 4f;

        [Header("Camera")]
        [SerializeField] private Transform cameraTarget;
        [SerializeField] private float TopClamp = 70.0f;
        [SerializeField] private float BottomClamp = -30.0f;
        [Range(0.0f, 0.3f)]
        [SerializeField] private float RotationSmoothTime = 0.12f;

        private float targetYaw;
        private float targetPitch;
        private float rotationVelocity;

        private Vector3 moveDir;
        private Vector2 lookInput;

        private Transform mainCamera;


        private void Awake()
        {
            mainCamera = Camera.main.transform;
        }

        private void Update()
        {
            Move();
        }

        private void LateUpdate()
        {
            CameraRotation();
        }

        private void Move()
        {
            Vector2 moveInput = gameInput.GetMove();
            float verticalMoveInput = gameInput.GetVertical();

            moveDir = new Vector3(moveInput.x * horizontalSpeed, verticalMoveInput * verticalSpeed, moveInput.y * forwardSpeed);


            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, mainCamera.eulerAngles.y, ref rotationVelocity, RotationSmoothTime);

            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);


            transform.Translate(moveDir * Time.deltaTime);
        }

        private void CameraRotation()
        {
            lookInput = gameInput.GetLook();

            targetYaw += lookInput.x;
            targetPitch += lookInput.y;

            targetYaw = ClampAngle(targetYaw, float.MinValue, float.MaxValue);
            targetPitch = ClampAngle(targetPitch, BottomClamp, TopClamp);

            cameraTarget.rotation = Quaternion.Euler(targetPitch, targetYaw, 0.0f);
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
    }
}