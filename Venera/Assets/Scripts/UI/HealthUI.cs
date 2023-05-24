using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Venera
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private DroneIntegrity droneIntegrity;
        [SerializeField] private TextMeshProUGUI healthText;
        void Start()
        {
            droneIntegrity.OnDamaged += DroneIntegrity_OnDamaged;
        }

        private void DroneIntegrity_OnDamaged(object sender, System.EventArgs e){
            healthText.text = (droneIntegrity.health.HealthPercent + "%");
        }
    }
}
