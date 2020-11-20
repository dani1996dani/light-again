using UnityEngine;

namespace Assets.Scripts.Movement
{
    public class OwlmanProjectileMovement : MonoBehaviour
    {      
        private float initialSpeed;
        private float currentSpeed;
        private float targetSpeed;
        private float velocity;

        public OwlmanProjectileMovement()
        {
            initialSpeed = Settings.OwlmanProjectileSpeed;
            currentSpeed = initialSpeed;
            targetSpeed = initialSpeed / 3;
        }

        private void Update()
        {
            float direction = transform.localScale.x > 0 ? 1 : -1;
            //currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref velocity, Settings.ArrowTimeToLive);
            gameObject.transform.position += Vector3.right * direction * currentSpeed * Time.deltaTime;
        }
    }
}
