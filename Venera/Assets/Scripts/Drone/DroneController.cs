using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Venera
{
    public class DroneController : BaseRigidbody
    {
        [Header("Control Properties")]
        [SerializeField] private float _minMaxPitch = 30f;
        [SerializeField] private float _minMAxRoll = 30f;
        [SerializeField] private float _yawPower = 4f;
        [SerializeField] private float _controlsLerpSpeed = 2f;

        [Header("Engine Properties")]
        [SerializeField] private float _maxPower = 4f;
        [SerializeField] private float _speedPower = 4f;
        [SerializeField] private float _propRotSpeed = 300f;

        public float MaxPower { get => _maxPower; }
        public float SpeedPower { get => _speedPower; }
        public float PropRotSpeed { get => _propRotSpeed; }
     
        private List<IEngine> _engines = new List<IEngine>();

        public int EngineCount { get => _engines.Count; }

        private float _finalPitch;
        private float _finalRoll;
        private float _finalYaw=0.0f;
        private Vector2 _lookInput;
        private float _yaw=0.0f;


        private void Start()
        {
            _engines = GetComponentsInChildren<IEngine>().ToList<IEngine>();
        }

        protected override void HandlePhysics()
        {
            HandleEngines();
            HandleControls();
        }

        private void HandleEngines()
        {
            foreach(IEngine engine in _engines)
            {
                engine.UpdateEngine(_rb);
            }
        }

        private void HandleControls()
        {
            _lookInput = GameInput.Instance.GetLook();
            float pitch = GameInput.Instance.GetMove().y * _minMaxPitch;
            float roll = -GameInput.Instance.GetMove().x * _minMAxRoll;
            _yaw += _lookInput.x * _yawPower;

            _finalPitch = Mathf.Lerp(_finalPitch, pitch, Time.deltaTime * _controlsLerpSpeed);
            _finalRoll = Mathf.Lerp(_finalRoll, roll, Time.deltaTime * _controlsLerpSpeed);
            _finalYaw = Mathf.Lerp(_finalYaw, _yaw, Time.deltaTime * _controlsLerpSpeed);

            Quaternion rotation = Quaternion.Euler(_finalPitch, _finalYaw, _finalRoll);

            _rb.MoveRotation(rotation);
        }
    }
}