using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerHealth : MonoBehaviour
    {
        private int curHealth;
        private int maxHealth = 100;
        private bool isAlive = true;

        void Start()
        {
            curHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            if (curHealth > 0)
            {
                curHealth -= damage;
            }
            if (curHealth <= 0 && isAlive)
            {
                Die();
            }

        }

        private void Die()
        {
            curHealth = 0;
            isAlive = false;
            Publisher.publish.CallPlayerDeath();
        }
    }
}
