using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Venera
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private DroneIntegrity _droneIntegrity;
        [SerializeField] private TextMeshProUGUI _healthText;
        void Start()
        {
            _droneIntegrity.OnDamaged += DroneIntegrity_OnDamaged;
        }

        private void DroneIntegrity_OnDamaged(object sender, System.EventArgs e){
            _healthText.text = (_droneIntegrity.health.HealthPercent + "%");
        }
    }
}
