using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class OwlmanProjectile : MonoBehaviour
{
    bool didHitPlayer = false;

    void Start()
    {
        StartCoroutine("DestoryAfterTimeToLiveEnded");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject colGameObject = collision.gameObject;
        if(colGameObject.tag == Settings.TagGround)
        {
            Destroy(gameObject);
        }

        if (!didHitPlayer && colGameObject.tag == Settings.TagPlayer)
        {
            didHitPlayer = true;
            Health playerHealth = colGameObject.GetComponent<Health>();
            playerHealth.TakeDamage(Settings.OwlmanMageProjectileDamage);
            Destroy(gameObject);
        }
    }

    IEnumerator DestoryAfterTimeToLiveEnded()
    {
        yield return new WaitForSeconds(Settings.OwlmanMageProjectileTimeToLive);
        Destroy(gameObject);
    }
}
