using UnityEngine;

namespace Assets.Scripts
{
    public class Health : MonoBehaviour
    {
        public int maxHealth = 100;

        private int curHealth;
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
