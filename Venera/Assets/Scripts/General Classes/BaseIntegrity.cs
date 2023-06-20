using UnityEngine;

namespace Venera
{
    public class BaseIntegrity : MonoBehaviour
    {
        [SerializeField] private int _startHealth = 1000;
        [SerializeField] private float _collisionCutoff = 50f;
        [SerializeField, Range(0,5)] private float _dmgMultiplier = 1f;
        public HealthSystem health;

        private void Awake() {
            health = new HealthSystem(_startHealth);
        }
        private void OnCollisionEnter(Collision col) {
            Vector3 collisionForce = col.impulse / Time.fixedDeltaTime;

            if(collisionForce.magnitude >= _collisionCutoff){
                Debug.Log(collisionForce.magnitude);
                int damage = ((int)(collisionForce.magnitude * _dmgMultiplier));
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
