using UnityEngine;
using System.Collections;
using System.Linq;

namespace Assets.Scripts.Attacks
{
    public class PlayerBasicAttack : MonoBehaviour
    {
        private GameObject arrowPrefab;
        private GameObject arrowSpawningPosition;
        private PlayerMovement playerMovement;
        private bool isShootingRight = true;
        private float cooldown = 0.0f;
        private Animator playerAnimator;
        AnimationClip attackAnimationClip;
        PlayerMovement playerMovementController;
        public bool isAttacking = false;

        private void Awake()
        {
            arrowPrefab = (GameObject)Resources.Load("Prefabs/Arrow", typeof(GameObject));
            arrowSpawningPosition = GameObject.FindGameObjectWithTag("ArrowSpawningPosition");

            GameObject playerGameObject = GameObject.FindGameObjectWithTag(Settings.TagPlayer);
            playerMovement = playerGameObject.GetComponent<PlayerMovement>();
            playerAnimator = playerGameObject.GetComponentInChildren<Animator>();
            attackAnimationClip = playerAnimator.runtimeAnimatorController.animationClips.FirstOrDefault((x) => x.name == "Attack");
            playerMovementController = playerGameObject.GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            cooldown -= Time.deltaTime;
            if (!Settings.isGamePaused && playerMovementController.isGrounded() && Input.GetKeyDown(KeyCode.Space) && cooldown <= 0)
            {
                cooldown = attackAnimationClip.length + 0.2f;
                StartCoroutine("ToggleAttackAnimation");
            }
        }

        // gets called from an animation trigger on the animation clip "Attack"
        public void SpawnArrow()
        {
            isShootingRight = playerMovement.isFacingRight;
            GameObject arrow = Instantiate(arrowPrefab, arrowSpawningPosition.transform.position, Quaternion.identity);

            
            if (!isShootingRight)
            {
                Vector3 vector = arrow.transform.localScale;
                vector.x = -1;
                arrow.transform.localScale = vector;
            }
        }

        IEnumerator ToggleAttackAnimation()
        {
            isAttacking = true;
            playerAnimator.SetBool("isAttacking", isAttacking);
            yield return new WaitForSeconds(attackAnimationClip.length);
            isAttacking = false;
            playerAnimator.SetBool("isAttacking", isAttacking);
        }
    }
}