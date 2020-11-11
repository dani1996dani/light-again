using Assets.Scripts.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Attacks
{
    public class OwlmanAttack : MonoBehaviour
    {
        List<GameObject> allChildren;
        bool canAttack = true;
        Health playerHealth;


        private void Awake()
        {
            allChildren = gameObject.GetAllChildren();

            playerHealth = GameObject.FindGameObjectWithTag(Settings.TagPlayer).GetComponent<Health>();
        }

        public bool isPlayerInAttackRange(Vector3 attackDirection)
        {
            return Physics2D.Raycast(transform.position, attackDirection, Settings.OwlmanAttackRange, LayerMask.GetMask("Player"));
        }

        public void AttackPlayer(Vector3 attackDirection)
        {
            if (canAttack)
            {
                IEnumerator dealDamage = DealDamage(attackDirection);
                StartCoroutine(dealDamage);
                StartCoroutine("SetAttackCooldown");
            }
        }

        IEnumerator SetAttackCooldown()
        {
            canAttack = false;
            yield return new WaitForSeconds(Settings.OwlmanAttackCooldownSeconds);
            canAttack = true;
        }

        IEnumerator DealDamage(Vector3 attackDirection)
        {
            yield return new WaitForSeconds(0.5f);
            bool isPlayerStillInAttackRange = isPlayerInAttackRange(attackDirection);
            if (isPlayerStillInAttackRange)
            {
                playerHealth.TakeDamage(Settings.OwlmanAttackDamage);
            }
        }
    }
}