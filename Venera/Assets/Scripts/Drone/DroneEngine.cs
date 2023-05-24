using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Venera
{
    [RequireComponent(typeof(BoxCollider))]
    public class DroneEngine : MonoBehaviour, IEngine
    {
        private DroneController controller;

        [SerializeField] private Transform propeller;

        private void Awake()
        {
            controller = GetComponentInParent<DroneController>();
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

            engineForce = transform.up * ((rb.mass * (Physics.gravity.magnitude)) + (GameInput.Instance.GetVertical() * controller.MaxPower));
            engineForce += ((transform.forward * (GameInput.Instance.GetMove().y * controller.SpeedPower)) + (transform.right * (GameInput.Instance.GetMove().x * controller.SpeedPower)));
            

            rb.AddForce(engineForce/controller.EngineCount, ForceMode.Force);
        }

        private void HandlePropellers()
        {
            if (!propeller) return;

            propeller.Rotate(Vector3.up, controller.PropRotSpeed);
        }
    }
}