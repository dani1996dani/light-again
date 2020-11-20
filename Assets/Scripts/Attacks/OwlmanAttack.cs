using Assets.Scripts.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Attacks
{
    public class OwlmanAttack : MonoBehaviour
    {
        List<GameObject> allChildren;
        bool canAttack = true;
        GameObject owlmanProjectilePrefab;


        private void Awake()
        {
            allChildren = gameObject.GetAllChildren();
            owlmanProjectilePrefab = (GameObject)Resources.Load("Prefabs/OwlmanSpellProjectile");
        }

        public bool isPlayerInAttackRange(Vector3 attackDirection, float attackRange)
        {
            return Physics2D.Raycast(transform.position, attackDirection, attackRange, LayerMask.GetMask("Player"));
        }

        public void AttackPlayer(Vector3 attackDirection, float attackRange)
        {
            if (canAttack)
            {
                IEnumerator dealDamage = DealDamage(attackDirection, attackRange);
                StartCoroutine(dealDamage);
                StartCoroutine("SetAttackCooldown");
            }
        }

        public void CastSpellTowardsPlayer(Vector3 attackDirection, Vector3 initialPosition)
        {
            if (canAttack)
            {
                IEnumerator coroutine = CastSpell(attackDirection, initialPosition);
                StartCoroutine(coroutine);
                StartCoroutine("SetAttackCooldown");
            }
        }

        IEnumerator SetAttackCooldown()
        {
            canAttack = false;
            yield return new WaitForSeconds(Settings.OwlmanAttackCooldownSeconds);
            canAttack = true;
        }

        IEnumerator CastSpell(Vector3 spellDirection, Vector3 initialPosition)
        {
            yield return new WaitForSeconds(1f);
            GameObject prefab = Instantiate(owlmanProjectilePrefab, initialPosition, Quaternion.identity);
            int prefabScale = 10;
            prefab.transform.localScale = new Vector3(spellDirection.x * prefabScale, prefabScale, 1);
        }

        IEnumerator DealDamage(Vector3 attackDirection, float attackRange)
        {
            yield return new WaitForSeconds(0.5f);
            bool isPlayerStillInAttackRange = isPlayerInAttackRange(attackDirection, attackRange);
            if (isPlayerStillInAttackRange)
            {
                Publisher.publish.CallPlayerHit(Settings.OwlmanAttackDamage);
            }
        }
    }
}