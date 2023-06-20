using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Venera
{
    [RequireComponent(typeof(DroneController))]
    public class DronePickUp : MonoBehaviour
    {
        [SerializeField] private float _interactDistance = 2f;

        private PickUpObj _selectedPickUp;
        private PickUpObj _holdingPickUp;
        

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
                    _holdingPickUp.PickUp(transform);
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

        public void HandleInteractions()
        {
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit raycastHit, _interactDistance))
            {
                if (raycastHit.transform.TryGetComponent(out PickUpObj objectPickUp))
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

            Debug.DrawRay(transform.position, Vector3.down * _interactDistance, Color.green, 0.2f);
        }
    }
}