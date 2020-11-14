using UnityEngine;

namespace Assets.Scripts.Attacks
{
    public class PlayerBasicAttack : MonoBehaviour
    {
        private GameObject arrowPrefab;
        private GameObject arrowSpawningPosition;
        private PlayerMovement playerMovement;
        private bool isShootingRight = true;
        private float cooldown = 0.0f;

        private void Awake()
        {
            arrowPrefab = (GameObject)Resources.Load("Prefabs/Arrow", typeof(GameObject));
            arrowSpawningPosition = GameObject.FindGameObjectWithTag("ArrowSpawningPosition");
            playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            cooldown -= Time.deltaTime;
            if (!Settings.isGamePaused && Input.GetKeyDown(KeyCode.Space) && cooldown <= 0)
            {
                isShootingRight = playerMovement.isFacingRight;
                GameObject arrow = Instantiate(arrowPrefab, arrowSpawningPosition.transform.position, Quaternion.identity);

                if (!isShootingRight)
                {
                    Vector3 vector = arrow.transform.localScale;
                    vector.x = -1;
                    arrow.transform.localScale = vector;
                }
                cooldown = 0.5f;
            }
        }
    }
}