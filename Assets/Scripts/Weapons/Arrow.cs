using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Arrow : MonoBehaviour
    {
        private float timeToLive = 1.0f;
        private int damage = 10;

        private void Update()
        {
            timeToLive -= Time.deltaTime;
            if(timeToLive <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider col)
        {
            Destroy(gameObject);
            col.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
