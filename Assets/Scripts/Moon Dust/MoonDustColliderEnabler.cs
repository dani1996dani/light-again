using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class MoonDustColliderEnabler : MonoBehaviour
{
    CircleCollider2D circleTrigger;

    private void Awake()
    {
        circleTrigger = gameObject.GetComponent<CircleCollider2D>();
        circleTrigger.enabled = false;
    }

    private void Start()
    {
        StartCoroutine("EnableCollider");
    }

    IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(Settings.TimeTillMoonDustCollectionTriggerIsActive);
        circleTrigger.enabled = true;
    }
}
