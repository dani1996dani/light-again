using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class OwlmanProjectile : MonoBehaviour
{
    bool didHitPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject colGameObject = collision.gameObject;
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
