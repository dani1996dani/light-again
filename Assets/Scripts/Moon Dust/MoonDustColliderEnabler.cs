using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class MoonDustColliderEnabler : MonoBehaviour
{
    BoxCollider2D moonDustCollider;
    CircleCollider2D circleTrigger;
    //Rigidbody2D rigidBody;

    private void Awake()
    {
        moonDustCollider = gameObject.GetComponent<BoxCollider2D>();

        //rigidBody = gameObject.GetComponent<Rigidbody2D>();
        circleTrigger = gameObject.GetComponent<CircleCollider2D>();
        //rigidBody.isKinematic = true;
        //moonDustCollider.enabled = false;
        circleTrigger.enabled = false;
    }

    private void Start()
    {
        StartCoroutine("EnableCollider");
    }

    IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(Settings.TimeTillMoonDustColliderIsActive);
        circleTrigger.enabled = true;
        //yield return new WaitForSeconds(Settings.TimeTillMoonDustColliderIsActive);
        //moonDustCollider.enabled = true;
        //rigidBody.isKinematic = false;
    }
}
