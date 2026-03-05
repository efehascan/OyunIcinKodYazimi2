using OrnekSOLID.Assets.Scripts.Managers;

namespace OrnekSOLID.Assets.Scripts.Core
{
    public class Enemy
    {
        private Health health;

        public Enemy(int startHealth)
        {
            health = new Health(startHealth);
        }
        
        public void ReceiveDamage(int damage)
        {
            health.TakeDamage(damage);
            
            if (health.IsDead())
            {
                GameManager.Instance.AddScore(10);
            }
        }
    }
}