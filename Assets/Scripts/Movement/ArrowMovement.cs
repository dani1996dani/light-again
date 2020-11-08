using UnityEngine;

namespace Assets.Scripts.Movement
{
    public class ArrowMovement : MonoBehaviour
    {      
        private int speed;

        public ArrowMovement()
        {
            speed = Settings.ArrowSpeed;
        }

        private void Update()
        {
            gameObject.transform.position += Vector3.right * transform.localScale.x * speed * Time.deltaTime;
        }
    }
}
