namespace Venera
{
    public class HealthSystem
    {
        private int _currentHealth;
        private int _currentMaxHealth;

        public int CurrentHealth { get => _currentHealth; }
        public int CurrentMaxHealth { get => _currentMaxHealth; }

        public float HealthPercent {get => ((float)_currentHealth/_currentMaxHealth * 100);}

        public HealthSystem(int startHealth) {
            _currentHealth = startHealth;
            _currentMaxHealth = startHealth;
        }

        public void Damage(int dmgAmount) {
            _currentHealth -= dmgAmount;
            if (_currentHealth < 0) {
                _currentHealth = 0;
            }
        }

        public void Heal(int healAmount) {

            if(_currentHealth < _currentMaxHealth) {
                _currentHealth += healAmount;
            }

            if(_currentHealth > _currentMaxHealth) {
                _currentHealth = _currentMaxHealth;
            }
        }
    }
}
