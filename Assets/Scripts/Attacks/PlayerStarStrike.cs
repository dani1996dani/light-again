using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using Assets.Scripts.Attacks;
using System.Linq;

public class PlayerStarStrike : MonoBehaviour
{
    AnimationClip starStrikeAnimationClip;
    private Animator playerAnimator;
    private PlayerBasicAttack playerBasicAttackController;
    PlayerMovement playerMovementController;

    void Start()
    {
        GameObject playerGameObject = GameObject.FindGameObjectWithTag(Settings.TagPlayer);
        playerAnimator = playerGameObject.GetComponentInChildren<Animator>();
        playerBasicAttackController = playerGameObject.GetComponentInChildren<PlayerBasicAttack>();
        starStrikeAnimationClip = playerAnimator.runtimeAnimatorController.animationClips.FirstOrDefault((x) => x.name == "StarStrike");
        playerMovementController = playerGameObject.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (!playerBasicAttackController.GetIsAttacking() && playerMovementController.isGrounded() && Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Debug.Log("MOON SHOT");
                StartCoroutine("CastStarStrike");
            }
        }
    }

    IEnumerator CastStarStrike()
    {
        playerBasicAttackController.SetIsAttacking(true);
        playerAnimator.SetBool("isCastingStarStrike", true);
        yield return new WaitForSeconds(starStrikeAnimationClip.length);
        playerBasicAttackController.SetIsAttacking(false);
        playerAnimator.SetBool("isCastingStarStrike", false);
    }
}
