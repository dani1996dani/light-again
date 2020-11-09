﻿using UnityEngine;

namespace Assets.Scripts
{
    public class Health : MonoBehaviour
    {
        private int maxHealth;
        private int curHealth;
        private bool isAlive = true;
        private bool isPlayer = false;

        void Start()
        {
            if (gameObject.tag == "Player")
            {
                maxHealth = Settings.PlayerMaxHealth;
                isPlayer = true;
            }
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
            if (isPlayer)
            {
                Publisher.publish.CallPlayerDeath();
            }
        }
    }
}