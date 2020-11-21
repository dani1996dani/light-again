using UnityEngine;

namespace Assets.Scripts.Movement
{
    public class OwlmanProjectileMovement : MonoBehaviour
    {      
        private float initialSpeed;
        private float currentSpeed;
        private Vector3 direction = Vector3.right;

        public OwlmanProjectileMovement()
        {
            initialSpeed = Settings.OwlmanProjectileSpeed;
            currentSpeed = initialSpeed;
        }

        public void SetDirection(Vector3 newDirection)
        {
            direction = newDirection;
        }

        private void Update()
        {
            gameObject.transform.position += this.direction * currentSpeed * Time.deltaTime;
        }
    }
}
