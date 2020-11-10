using UnityEngine;
using Assets.Scripts.Helpers;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace Assets.Scripts.Attacks
{
    public class OwlmanHitboxController : MonoBehaviour
    {
        private Health playerHealth;
        private void Start()
        {
            playerHealth = GameObject.FindGameObjectWithTag(Settings.TagPlayer).GetComponent<Health>();
        }

        void OnTriggerStay2D(Collider2D col)
        {
            Debug.Log("OnTriggerEnter2D was called " + col);
            if(col.gameObject.tag == Settings.TagPlayer)
            {
                playerHealth.TakeDamage(Settings.OwlmanAttackDamage);
            }
        }
    }
}