using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Venera
{
    [RequireComponent(typeof(DroneController))]
    public class DronePickUp : MonoBehaviour
    {
        [SerializeField] private float interactDistance = 2f;

        private PickUpObj selectedPickUp;
        private PickUpObj holdingPickUp;
        

        private void Start()
        {
            GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
        }

        private void GameInput_OnInteractAction(object sender, System.EventArgs e)
        {
            if(selectedPickUp != null)
            {
                if (holdingPickUp == null)
                {
                    holdingPickUp = selectedPickUp;
                    holdingPickUp.PickUp(transform);
                }
                else
                {
                    holdingPickUp.DropDown();
                    holdingPickUp = null;
                }
            }
        }

        private void Update()
        {
            HandleInteractions();
        }

        public void HandleInteractions()
        {
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit raycastHit, interactDistance))
            {
                if (raycastHit.transform.TryGetComponent(out PickUpObj objectPickUp))
                {
                    if(objectPickUp != selectedPickUp)
                    {
                        selectedPickUp = objectPickUp;
                        selectedPickUp.SetSelected(true);
                    }
                } else
                {
                    selectedPickUp?.SetSelected(false);
                    selectedPickUp = null;
                }
            } else
            {
                selectedPickUp?.SetSelected(false);
                selectedPickUp = null;
            }

            if (holdingPickUp != null)
            {
                holdingPickUp.SetSelected(false); 
            }

            Debug.DrawRay(transform.position, Vector3.down * interactDistance, Color.green, 0.2f);
        }
    }
}