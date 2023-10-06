using System;
using UnityEngine;

namespace Venera
{
    public class DroneIntegrity : BaseIntegrity
    {
        public event EventHandler OnDamaged;
        public event EventHandler OnKilled;

        [SerializeField] private bool isImmortal;

        public override void Damage(int dmg)
        {
            if(isImmortal) return;

            base.Damage(dmg);
            OnDamaged?.Invoke(this, EventArgs.Empty);

            if(health.CurrentHealth <= 0){
                OnKilled?.Invoke(this, EventArgs.Empty);
            }
        }

        
    }
}
