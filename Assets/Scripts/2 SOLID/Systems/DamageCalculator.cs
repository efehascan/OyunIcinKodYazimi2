using OrnekSOLID.Assets.Scripts.Interfaces;
using UnityEngine;

namespace OrnekSOLID.Assets.Scripts.Systems
{

    public class DamageCalculator
    {
        public int CalculateDamage(IAttack attack)
        {
            int damage = attack.BaseDamage;
            Debug.Log("Hesaplanan hasar: " + damage);
            return damage;
        }
    }
}