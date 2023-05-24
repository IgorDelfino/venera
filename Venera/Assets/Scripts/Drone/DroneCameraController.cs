using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Linq;

namespace Venera
{
    public class DroneCameraController : MonoBehaviour
    {
        [Header("Camera Properties")]
        [SerializeField] private CinemachineVirtualCamera vc;
        [SerializeField] private float baseFov = 40f;
        [SerializeField] private float fovMax = 80f;
        [SerializeField] private float fovMin = 35f;
        private float endFov;
        [SerializeField] private float cameraLerpTime = 2f;
        [SerializeField] private float minNoiseAmplitude = 0f;
        [SerializeField] private float maxNoiseAmplitude = 0.2f;
        private float endNoiseAmplitude;
        private float fovLerpVelocity = 0f;
        private float noiseLerpVelocity = 0f;
        private CinemachineBasicMultiChannelPerlin noise;

        private void Start()
        {
            noise = vc.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        void FixedUpdate()
        {
            HandleCamera();
        }


        private void HandleCamera()
        {
            endFov = baseFov + (GameInput.Instance.GetMove().y * fovMax);
            endFov = Mathf.Clamp(endFov, fovMin, fovMax);

            vc.m_Lens.FieldOfView = Mathf.SmoothDamp(vc.m_Lens.FieldOfView, endFov, ref fovLerpVelocity, cameraLerpTime);


            endNoiseAmplitude = (GameInput.Instance.GetMove().y);
            endNoiseAmplitude = Mathf.Clamp(endNoiseAmplitude, minNoiseAmplitude, maxNoiseAmplitude);

            noise.m_AmplitudeGain = Mathf.SmoothDamp(noise.m_AmplitudeGain, endNoiseAmplitude, ref noiseLerpVelocity, cameraLerpTime);
        }
    }
}
