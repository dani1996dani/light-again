using UnityEngine;

namespace Assets.Scripts.Movement
{
    public class ArrowMovement : MonoBehaviour
    {      
        private float initialSpeed;
        private float currentSpeed;
        private float targetSpeed;
        private float velocity;

        public ArrowMovement()
        {
            initialSpeed = Settings.ArrowSpeed;
            currentSpeed = initialSpeed;
            targetSpeed = initialSpeed / 3;
        }

        private void Update()
        {
            currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref velocity, Settings.ArrowTimeToLive);
            gameObject.transform.position += Vector3.right * transform.localScale.x * currentSpeed * Time.deltaTime;
        }
    }
}
