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

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            startDrag = rb.drag;
            startAngularDrag = rb.angularDrag;
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
