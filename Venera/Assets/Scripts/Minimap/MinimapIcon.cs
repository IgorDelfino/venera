using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Venera
{
    public class MinimapIcon : MonoBehaviour
    {
        
        [SerializeField] private float _camDistance;
        [SerializeField] private float _minimapSize;

        private float _yPosition;
        private Transform _targetTransform;

        private Transform _minimapCam;

        private void Start() {
            _minimapCam = FindAnyObjectByType<MinimapCamera>().transform;
            _yPosition = _minimapCam.position.y - _camDistance;
        }

        

        void LateUpdate () {
            transform.position = new Vector3 (
                Mathf.Clamp(_targetTransform.position.x, _minimapCam.position.x - _minimapSize, _minimapSize + _minimapCam.position.x),
                _yPosition,
                Mathf.Clamp(_targetTransform.position.z, _minimapCam.position.z - _minimapSize, _minimapSize + _minimapCam.position.z)
            );

            transform.rotation = Quaternion.Euler(90f,_targetTransform.eulerAngles.y,0f);
	    }

        public void Setup(Sprite iconSprite, Color iconColor, Transform targetTransform){
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = iconSprite;
            spriteRenderer.color = iconColor;

            _targetTransform = targetTransform;
        }
    }
}
