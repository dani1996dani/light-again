using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Arrow : MonoBehaviour
    {
        private float timeToLive = 1.0f;
        private int damage = 10;
        private List<int> hitableLayers;

        private void Awake(){
            hitableLayers = new List<int> { LayerMask.GetMask("Ground"), LayerMask.GetMask("Enemies") };
        }

        private void Update()
        {
            timeToLive -= Time.deltaTime;
            if (timeToLive <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (hitableLayers.Contains(col.gameObject.layer))
            {
                if (col.gameObject.layer == LayerMask.GetMask("Enemies"))
                {
                    col.gameObject.GetComponent<Health>().TakeDamage(damage);
                }

                Destroy(gameObject);                
            }
        }
    }
}
