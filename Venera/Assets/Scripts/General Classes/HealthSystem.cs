namespace Venera
{
    public class HealthSystem
    {
        private int currentHealth;
        private int currentMaxHealth;

        public int CurrentHealth { get => currentHealth; }
        public int CurrentMaxHealth { get => currentMaxHealth; }

        public float HealthPercent {get => ((float)currentHealth/currentMaxHealth * 100);}

        public HealthSystem(int startHealth) {
            currentHealth = startHealth;
            currentMaxHealth = startHealth;
        }

        public void Damage(int dmgAmount) {
            currentHealth -= dmgAmount;
            if (currentHealth < 0) {
                currentHealth = 0;
            }
        }

        public void Heal(int healAmount) {

            if(currentHealth < currentMaxHealth) {
                currentHealth += healAmount;
            }

            if(currentHealth > currentMaxHealth) {
                currentHealth = currentMaxHealth;
            }
        }
    }
}
