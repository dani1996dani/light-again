using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using System.Linq;

public class OwlmanProjectile : MonoBehaviour
{
    bool didHitPlayer = false;
    Animator projectileAnimator;
    AnimationClip impactAnimationClip;
    SFXManager sfxManager;

    void Start()
    {
        StartCoroutine("DestoryAfterTimeToLiveEnded");
        projectileAnimator = GetComponentInChildren<Animator>();
        impactAnimationClip = projectileAnimator.runtimeAnimatorController.animationClips.FirstOrDefault((x) => x.name == "Impact");
        sfxManager = GameObject.FindGameObjectWithTag(Settings.TagSFX).GetComponent<SFXManager>();
        sfxManager.PlaySFX(SFXType.FireballRelease);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject colGameObject = collision.gameObject;

        if (!didHitPlayer && colGameObject.tag == Settings.TagPlayer)
        {
            IEnumerator onHitCoroutine = OnHit(colGameObject);
            StartCoroutine(onHitCoroutine);
        }
    }

    private void Update()
    {
        GroundHitCheck();
    }

    private void GroundHitCheck()
    {
        bool didHitGroundLeft = Physics2D.Raycast(gameObject.transform.position, Vector2.left, 0.5f, LayerMask.GetMask(Settings.TagGround));
        if (didHitGroundLeft)
        {
            Destroy(gameObject);
        }
        bool didHitGroundRight = Physics2D.Raycast(gameObject.transform.position, Vector2.right, 0.5f, LayerMask.GetMask(Settings.TagGround));
        if (didHitGroundRight)
        {
            Destroy(gameObject);
        }
        bool didHitGroundUp = Physics2D.Raycast(gameObject.transform.position, Vector2.up, 0.5f, LayerMask.GetMask(Settings.TagGround));
        if (didHitGroundUp)
        {
            Destroy(gameObject);
        }
        bool didHitGroundDown = Physics2D.Raycast(gameObject.transform.position, Vector2.down, 0.5f, LayerMask.GetMask(Settings.TagGround));
        if (didHitGroundDown)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DestoryAfterTimeToLiveEnded()
    {
        yield return new WaitForSeconds(Settings.OwlmanMageProjectileTimeToLive);
        Destroy(gameObject);
    }

    IEnumerator OnHit(GameObject colGameObject)
    {
       
        sfxManager.PlaySFX(SFXType.FireballImpact);

        didHitPlayer = true;
        projectileAnimator.SetBool("didHit", didHitPlayer);

        Health playerHealth = colGameObject.GetComponent<Health>();
        playerHealth.TakeDamage(Settings.OwlmanMageProjectileDamage);

        yield return new WaitForSeconds(impactAnimationClip.length);
        Destroy(gameObject);
    }
}
