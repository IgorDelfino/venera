using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Venera
{
    [RequireComponent(typeof(DroneController))]
    public class DronePickUp : MonoBehaviour
    {
        [SerializeField] private float _interactDistance = 2f;
        [SerializeField] private float _offsetDistance = 1f;
        [SerializeField] private Vector3 _positionOffset;
        [SerializeField] private LayerMask _pickupLayer;

        private Vector3 _boxOffset;

        private PickUpObj _selectedPickUp;
        private PickUpObj _holdingPickUp;
        
        private void Awake() {
            _interactDistance = _interactDistance + _offsetDistance;
            _boxOffset = new Vector3(0f,_offsetDistance,0f);
        }

        private void Start()
        {
            GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
        }

        private void GameInput_OnInteractAction(object sender, System.EventArgs e)
        {
            if(_selectedPickUp != null)
            {
                if (_holdingPickUp == null)
                {
                    _holdingPickUp = _selectedPickUp;
                    _holdingPickUp.PickUp(transform, _positionOffset);
                }
                else
                {
                    _holdingPickUp.DropDown();
                    _holdingPickUp = null;
                }
            }
        }

        private void Update()
        {
            HandleInteractions();
        }
        /*
        public void HandleInteractions()
        {
            //if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit raycastHit, _interactDistance))
            if(Physics.BoxCast(transform.position, transform.lossyScale / 2, Vector3.down, out RaycastHit raycastHit, transform.rotation, _interactDistance))
            {
                if(raycastHit.transform.TryGetComponent(out PickUpObj objectPickUp))
                {
                    if(objectPickUp != _selectedPickUp)
                    {
                        _selectedPickUp = objectPickUp;
                        _selectedPickUp.SetSelected(true);
                    }
                } else
                {
                    _selectedPickUp?.SetSelected(false);
                    _selectedPickUp = null;
                }
            } else
            {
                _selectedPickUp?.SetSelected(false);
                _selectedPickUp = null;
            }

            if (_holdingPickUp != null)
            {
                _holdingPickUp.SetSelected(false); 
            }

            //Debug.DrawRay(transform.position, Vector3.down * _interactDistance, Color.green, 0.2f);
        }*/

        
        public void HandleInteractions()
        {
            if(Physics.BoxCast(transform.position + _boxOffset, transform.lossyScale / 2, Vector3.down, out RaycastHit raycastHit, transform.rotation, _interactDistance, _pickupLayer))
            {
                if(raycastHit.transform.TryGetComponent(out PickUpObj objectPickUp))
                {
                    if(objectPickUp != _selectedPickUp)
                    {
                        _selectedPickUp = objectPickUp;
                        _selectedPickUp.SetSelected(true);

                        return;
                    }
                    return;
                }
            }

            if(_selectedPickUp != null){
                _selectedPickUp.SetSelected(false);
                _selectedPickUp = null;
            }
            

            if (_holdingPickUp != null)
            {
                _holdingPickUp.SetSelected(false); 
            }
        }
        

        private void OnDrawGizmos() {
            RaycastHit hit;

            bool isHit = Physics.BoxCast(transform.position + _boxOffset, transform.lossyScale / 2, Vector3.down, out hit, transform.rotation, _interactDistance, _pickupLayer);

            if(isHit){
                Gizmos.color = Color.red;
                Gizmos.DrawRay(transform.position + _boxOffset, transform.forward * hit.distance);
                Gizmos.DrawWireCube(transform.position + _boxOffset + Vector3.down * hit.distance, transform.localScale);
            } else {
                Gizmos.color = Color.green;
                Gizmos.DrawRay(transform.position + _boxOffset, Vector3.down * _interactDistance);
            }
        }

    }
}