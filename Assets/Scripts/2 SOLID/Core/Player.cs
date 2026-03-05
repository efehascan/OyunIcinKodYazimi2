using OrnekSOLID.Assets.Scripts.Attacks;
using OrnekSOLID.Assets.Scripts.Interfaces;
using OrnekSOLID.Assets.Scripts.Systems;
using UnityEngine;

namespace OrnekSOLID.Assets.Scripts.Core
{

    public class Player : MonoBehaviour
    {
        private IAttack attackType;
        private DamageCalculator damageCalculator;
        private Enemy enemy;

        void Start()
        {
            attackType = new SwordAttack();

            damageCalculator = new DamageCalculator();

            enemy = new Enemy(50);

            PerformAttack();
        }

        void PerformAttack()
        {
            int damage = damageCalculator.CalculateDamage(attackType);
            enemy.ReceiveDamage(damage);
        }
    }
}