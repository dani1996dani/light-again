using UnityEngine;

namespace Assets.Scripts
{
    public class Health : MonoBehaviour
    {
        public int maxHealth;
        public int curHealth;
        private bool isAlive = true;
        private bool isPlayer = false;

        void Start()
        {
            if (gameObject.tag == "Player")
            {
                maxHealth = Settings.PlayerMaxHealth;
                isPlayer = true;
            }
            if (gameObject.tag == Settings.TagOwlman)
            {
                maxHealth = Settings.OwlmanMaxHealth;
            }
            curHealth = maxHealth;
        }

        public bool getIsAlive()
        {
            return this.isAlive;
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
            if (isPlayer)
            {
                Publisher.publish.CallPlayerDeath();
            }
            if (!isPlayer)
            {
                Publisher.publish.CallEnemyDeath(gameObject);
            }
        }
    }
}
