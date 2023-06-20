using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Venera
{
    [RequireComponent(typeof(Rigidbody))]
    public class PickUpObj : MonoBehaviour
    {

        [SerializeField] private PickUpObjSO _pickUpObjectSO;
        [SerializeField] private GameObject _selectedVisual;
        [SerializeField] private Vector3 _positionOffset;
        
        private Joint _joint;

        public void PickUp(Transform targetObj)
        {
            CreateJoint(targetObj);
        }

        public void DropDown()
        {
            Destroy(_joint);
        }

        private void CreateJoint(Transform targetObj)
        {
            transform.position = targetObj.position - _positionOffset;

            _joint = gameObject.AddComponent<FixedJoint>();
            _joint.connectedMassScale = _pickUpObjectSO.connectedMassScale;

            _joint.connectedBody = targetObj.GetComponent<Rigidbody>();
        }
        
        public void SetSelected(bool state)
        {
            _selectedVisual.SetActive(state);
        }
    }
}
