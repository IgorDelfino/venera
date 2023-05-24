using System;

namespace Venera
{
    public class DroneIntegrity : BaseIntegrity
    {
        public event EventHandler OnDamaged;

        public override void Damage(int dmg)
        {
            base.Damage(dmg);
            OnDamaged?.Invoke(this,EventArgs.Empty);
        }
    }
}
