using UnityEngine;

namespace Assets.Scripts
{
    public class Health : MonoBehaviour
    {
        private int maxHealth;
        private int curHealth;
        private bool isAlive = true;
        private bool isPlayer = false;
        private bool isBoss = false;

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
            if (gameObject.tag == Settings.TagOwlmanStrong)
            {
                maxHealth = Settings.OwlmanStrongMaxHealth;
            }
            if (gameObject.tag == Settings.TagOwlmanMage)
            {
                maxHealth = Settings.OwlmanMageMaxHealth;
            }
            if (gameObject.tag == Settings.TagOwlmanBoss)
            {
                maxHealth = Settings.OwlmanBossMaxHealth;
            }
            curHealth = maxHealth;

            if (gameObject.tag == Settings.TagOwlmanBoss)
            {
                isBoss = true;
            }
        }

        public int GetCurrentHealth()
        {
            return this.curHealth;
        }

        public int GetMaxHealth()
        {
            return this.maxHealth;
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
                if (isBoss)
                {
                    Publisher.publish.CallBossDeath();
                }
                Publisher.publish.CallEnemyDeath(gameObject);
            }
        }
    }
}
