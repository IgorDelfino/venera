using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Venera
{
    public class MinimapIconSpawn : MonoBehaviour
    {
        [SerializeField] private Sprite _iconSprite;
        [SerializeField] private Color _iconColor;
        
        private MinimapIcon _minimapIcon;
        
        void Start()
        {
            _minimapIcon = Instantiate(Resources.Load("MinimapIcon")).GetComponent<MinimapIcon>();
            _minimapIcon.Setup(_iconSprite,_iconColor,this.transform);
        }
    }
}
