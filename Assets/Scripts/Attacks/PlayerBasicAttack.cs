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

        private void Awake()
        {
            arrowPrefab = (GameObject)Resources.Load("Prefabs/Arrow", typeof(GameObject));
            arrowSpawningPosition = GameObject.FindGameObjectWithTag("ArrowSpawningPosition");
            playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
            playerAnimator = GameObject.FindGameObjectWithTag(Settings.TagPlayer).GetComponentInChildren<Animator>();
            attackAnimationClip = playerAnimator.runtimeAnimatorController.animationClips.FirstOrDefault((x) => x.name == "Attack");
        }

        private void Update()
        {
            cooldown -= Time.deltaTime;
            if (!Settings.isGamePaused && Input.GetKeyDown(KeyCode.Space) && cooldown <= 0)
            {
                StartCoroutine("ToggleAttackAnimation");
                StartCoroutine("SpawnArrow");
                //isShootingRight = playerMovement.isFacingRight;
                //GameObject arrow = Instantiate(arrowPrefab, arrowSpawningPosition.transform.position, Quaternion.identity);

                //StartCoroutine("ToggleAttackAnimation");
                //if (!isShootingRight)
                //{
                //    Vector3 vector = arrow.transform.localScale;
                //    vector.x = -1;
                //    arrow.transform.localScale = vector;
                //}
                cooldown = 0.5f;
            }
        }

        IEnumerator SpawnArrow()
        {
            yield return new WaitForSeconds(attackAnimationClip.length / 2);
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
            playerAnimator.SetBool("isAttacking", true);
            yield return new WaitForSeconds(attackAnimationClip.length);
            playerAnimator.SetBool("isAttacking", false);
        }
    }
}