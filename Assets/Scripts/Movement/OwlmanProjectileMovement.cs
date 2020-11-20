using UnityEngine;

namespace Assets.Scripts.Movement
{
    public class OwlmanProjectileMovement : MonoBehaviour
    {      
        private float initialSpeed;
        private float currentSpeed;

        public OwlmanProjectileMovement()
        {
            initialSpeed = Settings.OwlmanProjectileSpeed;
            currentSpeed = initialSpeed;
        }

        private void Update()
        {
            float direction = transform.localScale.x > 0 ? 1 : -1;
            gameObject.transform.position += Vector3.right * direction * currentSpeed * Time.deltaTime;
        }
    }
}
