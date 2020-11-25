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

    void Start()
    {
        StartCoroutine("DestoryAfterTimeToLiveEnded");
        projectileAnimator = GetComponentInChildren<Animator>();
        impactAnimationClip = projectileAnimator.runtimeAnimatorController.animationClips.FirstOrDefault((x) => x.name == "Impact");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject colGameObject = collision.gameObject;
        if (colGameObject.tag == Settings.TagGround)
        {
            Destroy(gameObject);
        }

        if (!didHitPlayer && colGameObject.tag == Settings.TagPlayer)
        {
            IEnumerator onHitCoroutine = OnHit(colGameObject);
            StartCoroutine(onHitCoroutine);
        }
    }

    IEnumerator DestoryAfterTimeToLiveEnded()
    {
        yield return new WaitForSeconds(Settings.OwlmanMageProjectileTimeToLive);
        Destroy(gameObject);
    }

    IEnumerator OnHit(GameObject colGameObject)
    {
        didHitPlayer = true;
        if(projectileAnimator != null)
        {
            projectileAnimator.SetBool("didHit", didHitPlayer);
        }
        Health playerHealth = colGameObject.GetComponent<Health>();
        playerHealth.TakeDamage(Settings.OwlmanMageProjectileDamage);

        yield return new WaitForSeconds(impactAnimationClip.length);
        Destroy(gameObject);
    }
}
