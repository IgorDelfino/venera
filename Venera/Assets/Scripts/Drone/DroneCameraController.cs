using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Linq;
using Unity.VisualScripting;

namespace Venera
{
    public class DroneCameraController : MonoBehaviour
    {
        [Header("Camera Properties")]
        [SerializeField] private CinemachineVirtualCamera _vc;
        [SerializeField] private float _baseFov = 40f;
        [SerializeField] private float _fovMax = 80f;
        [SerializeField] private float _fovMin = 35f;
        private float _endFov;
        [SerializeField] private float _cameraLerpTime = 2f;
        [SerializeField] private float _minNoiseAmplitude = 0f;
        [SerializeField] private float _maxNoiseAmplitude = 0.2f;
        private float _endNoiseAmplitude;
        private float _fovLerpVelocity = 0f;
        private float _noiseLerpVelocity = 0f;
        private CinemachineBasicMultiChannelPerlin _noise;

        private void Awake() {
            _vc = FindObjectOfType<CinemachineVirtualCamera>();
        }

        private void Start()
        {
            _noise = _vc.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        void FixedUpdate()
        {
            HandleCamera();
        }


        private void HandleCamera()
        {
            _endFov = _baseFov + (GameInput.Instance.GetMove().y * _fovMax);
            _endFov = Mathf.Clamp(_endFov, _fovMin, _fovMax);

            _vc.m_Lens.FieldOfView = Mathf.SmoothDamp(_vc.m_Lens.FieldOfView, _endFov, ref _fovLerpVelocity, _cameraLerpTime);


            _endNoiseAmplitude = (GameInput.Instance.GetMove().y);
            _endNoiseAmplitude = Mathf.Clamp(_endNoiseAmplitude, _minNoiseAmplitude, _maxNoiseAmplitude);

            _noise.m_AmplitudeGain = Mathf.SmoothDamp(_noise.m_AmplitudeGain, _endNoiseAmplitude, ref _noiseLerpVelocity, _cameraLerpTime);
        }
    }
}
