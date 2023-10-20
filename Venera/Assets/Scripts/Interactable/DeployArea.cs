using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Venera;

namespace Venera
{
    public class DeployArea : MonoBehaviour
    {
        public event EventHandler OnWin;

        [Serializable]
        private class WinObject{
            public PickUpObj pickUpObj;
            public bool isCollected;
        }
        [SerializeField] private List<WinObject> _winObjects;

        private void OnTriggerEnter(Collider col) {
            if (col.TryGetComponent(out PickUpObj pickUpObj)){
                Debug.Log("inside");
                AddObject(pickUpObj);
            }
        }

        private void OnTriggerExit(Collider col) {
            if (col.TryGetComponent(out PickUpObj pickUpObj)){
                Debug.Log("outside");
                RemoveObject(pickUpObj);
            }
        }

        void AddObject(PickUpObj pickUpObj){
            for(int i=0; i< _winObjects.Count; i++){
                if(_winObjects[i].pickUpObj == pickUpObj){
                    _winObjects[i].isCollected = true;

                    CheckWin();

                    return;
                }
            }
        }

        void RemoveObject(PickUpObj pickUpObj){
            for(int i=0; i< _winObjects.Count; i++){
                if(_winObjects[i].pickUpObj == pickUpObj){
                    _winObjects[i].isCollected = false;
                    return;
                }
            }
        }

        void CheckWin(){
            for(int i=0; i< _winObjects.Count; i++){
                if(_winObjects[i].isCollected == false){
                    return;
                }
            }

            Debug.Log("WIN!!!");
            OnWin?.Invoke(this, EventArgs.Empty);
        }
    }
}
