using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Arrow : MonoBehaviour
    {
        private float timeToLive = 1.0f;
        private int damage = 10;
        private List<int> hitableLayers = new List<int> { 8, 10 }; //ground, enemy

        private void Update()
        {
            timeToLive -= Time.deltaTime;
            if (timeToLive <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider col)
        {
            if (hitableLayers.Contains(col.gameObject.layer))
            {
                Destroy(gameObject);

                if (col.gameObject.layer == 10) //enemy
                {
                    col.gameObject.GetComponent<Health>().TakeDamage(damage);
                }
            }
        }
    }
}
