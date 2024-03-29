using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Venera
{
    public class MinimapCamera : MonoBehaviour
    {
        [SerializeField] private Transform _player;

        private void LateUpdate() {
            Vector3 newPosition = _player.position;
            newPosition.y = transform.position.y;
            transform.position = newPosition;
        }
    }
}