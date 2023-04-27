using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Venera
{
    [RequireComponent(typeof(GameInput))]
    public class DroneController : BaseRigidbody
    {
        [Header("Control Properties")]
        [SerializeField] private float minMaxPitch = 30f;
        [SerializeField] private float minMAxRoll = 30f;
        [SerializeField] private float yawPower = 4f;
        [SerializeField] private float controlsLerpSpeed = 2f;

        [Header("Engine Properties")]
        [SerializeField] private float maxPower = 4f;
        [SerializeField] private float speedPower = 4f;
        [SerializeField] private float propRotSpeed = 300f;
        [SerializeField] private bool setVerticalStabilization = true;

        public float MaxPower { get => maxPower; }
        public float SpeedPower { get => speedPower; }
        public float PropRotSpeed { get => propRotSpeed; }
        public bool SetVerticalStabilization { get => setVerticalStabilization; }


        [Header("Camera Properties")]
        [SerializeField] private CinemachineVirtualCamera vc;
        [SerializeField] private float baseFov = 40f;
        [SerializeField] private float minMaxFov = 10f;
        [SerializeField] private float fovMax = 80f;
        [SerializeField] private float fovMin = 35f;
        [SerializeField] private float endFov;
        [SerializeField] private float fovLerpTime = 2f;
        private float fovCurrentVelocity = 0f;

        private GameInput gameInput;
        private List<IEngine> engines = new List<IEngine>();

        public int EngineCount { get => engines.Count; }

        private float finalPitch;
        private float finalRoll;
        private float finalYaw=0.0f;
        private Vector2 lookInput;
        float yaw=0.0f;


        private void Start()
        {
            gameInput = GetComponent<GameInput>();
            engines = GetComponentsInChildren<IEngine>().ToList<IEngine>();
        }

        private void LateUpdate()
        {
            HandleCamera();

        }

        protected override void HandlePhysics()
        {
            HandleEngines();
            HandleControls();
        }

        private void HandleEngines()
        {
            foreach(IEngine engine in engines)
            {
                engine.UpdateEngine(rb,gameInput);
            }
        }

        private void HandleControls()
        {
            lookInput = gameInput.GetLook();
            float pitch = gameInput.GetMove().y * minMaxPitch;
            float roll = -gameInput.GetMove().x * minMAxRoll;
            yaw += lookInput.x * yawPower;

            finalPitch = Mathf.Lerp(finalPitch, pitch, Time.deltaTime * controlsLerpSpeed);
            finalRoll = Mathf.Lerp(finalRoll, roll, Time.deltaTime * controlsLerpSpeed);
            finalYaw = Mathf.Lerp(finalYaw, yaw, Time.deltaTime * controlsLerpSpeed);

            Quaternion rotation = Quaternion.Euler(finalPitch, finalYaw, finalRoll);

            rb.MoveRotation(rotation);
        }

        private void HandleCamera()
        {
            endFov = baseFov + (gameInput.GetMove().y * minMaxFov);
            endFov = Mathf.Clamp(endFov, fovMin, fovMax);

            //vc.m_Lens.FieldOfView = Mathf.Lerp(vc.m_Lens.FieldOfView, endFov, Time.deltaTime * fovLerpSpeed);
            vc.m_Lens.FieldOfView = Mathf.SmoothDamp(vc.m_Lens.FieldOfView,endFov,ref fovCurrentVelocity,fovLerpTime);
        }
    }
}