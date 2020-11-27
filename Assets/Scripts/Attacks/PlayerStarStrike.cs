using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using Assets.Scripts.Attacks;
using Assets.Scripts.Movement;
using System.Linq;

public class PlayerStarStrike : MonoBehaviour
{
    AnimationClip starStrikeAnimationClip;
    private Animator playerAnimator;
    private PlayerBasicAttack playerBasicAttackController;
    PlayerMovement playerMovementController;
    MoonDustProgress moonDustProgressController;

    private GameObject starStrikeArrowPrefab;

    void Start()
    {
        GameObject playerGameObject = GameObject.FindGameObjectWithTag(Settings.TagPlayer);
        playerAnimator = playerGameObject.GetComponentInChildren<Animator>();
        playerBasicAttackController = playerGameObject.GetComponentInChildren<PlayerBasicAttack>();
        starStrikeAnimationClip = playerAnimator.runtimeAnimatorController.animationClips.FirstOrDefault((x) => x.name == "StarStrike");
        playerMovementController = playerGameObject.GetComponent<PlayerMovement>();
        moonDustProgressController = GameObject.FindGameObjectWithTag(Settings.TagGameSettings).GetComponent<MoonDustProgress>();

        starStrikeArrowPrefab = (GameObject)Resources.Load("Prefabs/StarStrikeArrow", typeof(GameObject));
    }

    void Update()
    {
        if (!playerBasicAttackController.GetIsAttacking() && playerMovementController.isGrounded() && moonDustProgressController.IsFull())
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                StartCastingStarStrike();
                moonDustProgressController.ResetMoonDustAmount();
            }
        }
    }

    // this function is called by an animation trigger
    public void SpawnVerticalArrow()
    {
        GameObject arrow = Instantiate(starStrikeArrowPrefab, transform.position, Quaternion.identity);
        ArrowMovement arrowMovement = arrow.GetComponent<ArrowMovement>();
        arrowMovement.SetDirection(Vector3.up);
    }

    // this function is called by an animation trigger
    public void DoneCastingStarStrike()
    {
        playerBasicAttackController.SetIsAttacking(false);
        playerAnimator.SetBool("isCastingStarStrike", false);
    }

    public void StartCastingStarStrike()
    {
        playerBasicAttackController.SetIsAttacking(true);
        playerAnimator.SetBool("isCastingStarStrike", true);
    }
}
