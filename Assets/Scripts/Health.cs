using UnityEngine;

namespace Assets.Scripts
{
    public class Health : MonoBehaviour
    {
        public int curHealth = 0;
        public int maxHealth = 100;

        // Start is called before the first frame update
        void Start()
        {
            curHealth = maxHealth;
        }

        public void DamagePlayer(int damage)
        {
            curHealth -= damage;

            if(curHealth < 0)
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
