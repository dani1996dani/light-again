using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class StarStrikeArrowLifecycle : MonoBehaviour
{
    void Start()
    {
        StartCoroutine("DestroyAfterTimeToLiveEnded");
    }

    IEnumerator DestroyAfterTimeToLiveEnded()
    {
        yield return new WaitForSeconds(Settings.StarStrikeArrowTimeToLive);
        Publisher.publish.CallStarStrike();
        Destroy(gameObject);
    }
}
