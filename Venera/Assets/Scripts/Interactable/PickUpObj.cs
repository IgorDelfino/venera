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
        private Vector3 _positionOffset;
        
        private Joint _joint;
        private void Awake() {
            float height = transform.localScale.y * GetComponent<BoxCollider>().size.y;
            _positionOffset = new Vector3(0,height/2,0);
        }

        public void PickUp(Transform targetObj, Vector3 ObjPositionOffset)
        {
            CreateJoint(targetObj, ObjPositionOffset);
        }

        public void DropDown()
        {
            Destroy(_joint);
        }

        private void CreateJoint(Transform targetObj, Vector3 objPositionOffset)
        {
            transform.position = targetObj.position - (_positionOffset + objPositionOffset);

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
