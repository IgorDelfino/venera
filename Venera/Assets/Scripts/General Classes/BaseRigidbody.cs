using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Venera
{
    [RequireComponent(typeof(Rigidbody))]
    public class BaseRigidbody : MonoBehaviour
    {
        protected Rigidbody _rb;
        protected float _startDrag;
        protected float _startAngularDrag;
        protected float _startMass;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _startDrag = _rb.drag;
            _startAngularDrag = _rb.angularDrag;
            _startMass = _rb.mass;
        }

        private void FixedUpdate()
        {
            if (!_rb)
            {
                return;
            }

            HandlePhysics();
        }

        protected virtual void HandlePhysics() { }
    }
}
