using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class OwlmanGroundSmashReverb : MonoBehaviour
{
    bool didHitPlayer = false;

    private void Start()
    {
        StartCoroutine("DestoryAfterTimeToLiveEnded");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject colGameObject = collision.gameObject;

        if (!didHitPlayer && colGameObject.tag == Settings.TagPlayer)
        {
            didHitPlayer = true;
            Health playerHealth = colGameObject.GetComponent<Health>();
            playerHealth.TakeDamage(Settings.OwlmanBossGroundSmashReverbDamage);
        }
    }


    IEnumerator DestoryAfterTimeToLiveEnded()
    {
        yield return new WaitForSeconds(Settings.OwlmannBossGroundSmashReverbTimeToLive);
        Destroy(gameObject);
    }
}
