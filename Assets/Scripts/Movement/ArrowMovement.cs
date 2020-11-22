using UnityEngine;

namespace Assets.Scripts.Movement
{
    public class ArrowMovement : MonoBehaviour
    {      
        private float initialSpeed;
        private float currentSpeed;
        private float targetSpeed;
        private float velocity;
        private Vector3 direction = Vector3.right;

        public ArrowMovement()
        {
            initialSpeed = Settings.ArrowSpeed;
            currentSpeed = initialSpeed;
            targetSpeed = initialSpeed / 3;
        }

        public void SetDirection(Vector3 newDirection)
        {
            this.direction = newDirection;
        }

        private void Update()
        {
            currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref velocity, Settings.ArrowTimeToLive);
            gameObject.transform.position += direction * transform.localScale.x * currentSpeed * Time.deltaTime;
        }
    }
}
