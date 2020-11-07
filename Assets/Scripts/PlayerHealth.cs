using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerHealth : MonoBehaviour
    {
        private int curHealth;
        private int maxHealth = 100;

        void Start()
        {
            curHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            curHealth -= damage;

            if(curHealth <= 0)
            {
                Die();
            }

        }

        private void Die()
        {
            curHealth = 0;
            Publisher.publish.CallPlayerDeath();
        }
    }
}
