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

        public void UpdateEngine(Rigidbody rb, GameInput gameInput)
        {
            HandleForce(rb, gameInput);

            HandlePropellers();
        }

        private void HandleForce(Rigidbody rb, GameInput gameInput)
        {
            Vector3 engineForce = Vector3.zero;

            engineForce = transform.up * ((rb.mass * (Physics.gravity.magnitude)) + (gameInput.GetVertical() * controller.MaxPower));
            engineForce += ((transform.forward * (gameInput.GetMove().y * controller.SpeedPower)) + (transform.right * (gameInput.GetMove().x * controller.SpeedPower)));

            if (controller.SetVerticalStabilization)
            {
                engineForce.y = ((rb.mass * (Physics.gravity.magnitude)) + (gameInput.GetVertical() * controller.MaxPower));
            }
                

            //Vector3 diff = new Vector3(0f, (engineForce.magnitude - engineForce.y), 0f);

            //engineForce += diff;

            rb.AddForce(engineForce/controller.EngineCount, ForceMode.Force);
        }

        private void HandlePropellers()
        {
            if (!propeller) return;

            propeller.Rotate(Vector3.up, controller.PropRotSpeed);
        }
    }
}