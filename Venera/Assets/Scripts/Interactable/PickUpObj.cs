using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Venera
{
    [RequireComponent(typeof(Rigidbody))]
    public class PickUpObj : MonoBehaviour
    {

        [SerializeField] private PickUpObjSO pickUpObjectSO;
        [SerializeField] private GameObject selectedVisual;
        [SerializeField] private Vector3 positionOffset;
        
        private Joint joint;

        public void PickUp(Transform targetObj)
        {
            CreateJoint(targetObj);
        }

        public void DropDown()
        {
            Destroy(joint);
        }

        private void CreateJoint(Transform targetObj)
        {
            transform.position = targetObj.position - positionOffset;

            joint = gameObject.AddComponent<FixedJoint>();
            joint.connectedMassScale = pickUpObjectSO.connectedMassScale;

            joint.connectedBody = targetObj.GetComponent<Rigidbody>();
        }
        
        public void SetSelected(bool state)
        {
            selectedVisual.SetActive(state);
        }
    }
}
