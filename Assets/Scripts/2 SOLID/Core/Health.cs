using UnityEngine;

namespace OrnekSOLID
{
    // Bu sınıf sadece health yönetir.
    public class Health
    {
        private int currentHealth;

        public Health(int startHealth)
        {
            currentHealth = startHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            Debug.Log("Can azaldı. Kalan can: " + currentHealth);
        }
        
        public bool IsDead()
        {
            return currentHealth <= 0;
        }
        
    }
}