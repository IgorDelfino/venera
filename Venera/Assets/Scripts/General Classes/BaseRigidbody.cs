using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Venera
{
    [RequireComponent(typeof(Rigidbody))]
    public class BaseRigidbody : MonoBehaviour
    {
        protected Rigidbody rb;
        protected float startDrag;
        protected float startAngularDrag;
        protected float startMass;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            startDrag = rb.drag;
            startAngularDrag = rb.angularDrag;
            startMass = rb.mass;
        }

        private void FixedUpdate()
        {
            if (!rb)
            {
                return;
            }

            HandlePhysics();
        }

        protected virtual void HandlePhysics() { }
    }
}
