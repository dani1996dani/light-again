using UnityEngine;
using Assets.Scripts.Helpers;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace Assets.Scripts.Attacks
{
    public class OwlmanAttack : MonoBehaviour
    {
        GameObject attackHitbox;
        BoxCollider2D attackHitboxCollider;
        List<GameObject> allChildren;
        bool canAttack = true;

        private void Awake()
        {
            allChildren = gameObject.GetAllChildren();
            attackHitbox = allChildren.FirstOrDefault(child => child.tag == Settings.TagOwlmanAttackHitbox);
            attackHitboxCollider = attackHitbox.GetComponent<BoxCollider2D>();
            attackHitboxCollider.enabled = false;
        }

        public bool isPlayerInAttackRange(Vector3 lookingAtDirection)
        {
            return Physics2D.Raycast(transform.position, lookingAtDirection, Settings.OwlmanAttackRange, LayerMask.GetMask("Player"));
        }

        public void AttackPlayer()
        {
            if (canAttack)
            {
                StartCoroutine("FlashAttackHitbox");
                StartCoroutine("SetAttackCooldown");
            }
        }

        IEnumerator SetAttackCooldown()
        {
            Debug.Log("SetAttackCooldown was called");
            canAttack = false;
            yield return new WaitForSeconds(Settings.OwlmanAttackCooldownSeconds);
            canAttack = true;
        }

        IEnumerator FlashAttackHitbox()
        {
            Debug.Log("FlashAttackHitbox was called");
            attackHitboxCollider.enabled = true;
            yield return new WaitForEndOfFrame();
            //attackHitboxCollider.enabled = false;
        }
    }
}