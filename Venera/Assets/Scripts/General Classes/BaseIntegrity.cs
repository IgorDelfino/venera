using UnityEngine;

namespace Venera
{
    public class BaseIntegrity : MonoBehaviour
    {
        [SerializeField] private int startHealth = 1000;
        [SerializeField] private float collisionCutoff = 50f;
        [SerializeField, Range(0,5)] private float dmgMultiplier = 1f;
        public HealthSystem health;

        private void Awake() {
            health = new HealthSystem(startHealth);
        }
        private void OnCollisionEnter(Collision col) {
            Vector3 collisionForce = col.impulse / Time.fixedDeltaTime;

            if(collisionForce.magnitude >= collisionCutoff){
                Debug.Log(collisionForce.magnitude);
                int damage = ((int)(collisionForce.magnitude * dmgMultiplier));
                Damage(damage);
            }
        }

        public virtual void Damage(int dmg){
            health.Damage(dmg);
        }

        public virtual void Heal(int healAmount){
            health.Heal(healAmount);
        }
    }
}
