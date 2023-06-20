using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Venera
{
    [RequireComponent(typeof(BoxCollider))]
    public class DroneEngine : MonoBehaviour, IEngine
    {
        private DroneController _controller;

        [SerializeField] private Transform _propeller;

        private void Awake()
        {
            _controller = GetComponentInParent<DroneController>();
        }

        public void InitEngine()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateEngine(Rigidbody rb)
        {
            HandleForce(rb);

            HandlePropellers();
        }

        private void HandleForce(Rigidbody rb)
        {
            Vector3 engineForce = Vector3.zero;

            engineForce = transform.up * ((rb.mass * (Physics.gravity.magnitude)) + (GameInput.Instance.GetVertical() * _controller.MaxPower));
            engineForce += ((transform.forward * (GameInput.Instance.GetMove().y * _controller.SpeedPower)) + (transform.right * (GameInput.Instance.GetMove().x * _controller.SpeedPower)));
            

            rb.AddForce(engineForce/_controller.EngineCount, ForceMode.Force);
        }

        private void HandlePropellers()
        {
            if (!_propeller) return;

            _propeller.Rotate(Vector3.up, _controller.PropRotSpeed);
        }
    }
}