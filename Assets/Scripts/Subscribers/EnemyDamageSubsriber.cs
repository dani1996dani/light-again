using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using System.Linq;

public class EnemyDamageSubsriber : MonoBehaviour
{
    GameObject moonDustPrefab;

    private void Awake()
    {
        moonDustPrefab = (GameObject)Resources.Load("Prefabs/MoonDust", typeof(GameObject));
    }

    private void OnEnable()
    {
        Publisher.publish.EnemyDeath += OnEnemyDeath;
    }

    private void OnEnemyDeath(GameObject enemy)
    {
        Vector3 enemyPosition = enemy.transform.position;
        RaycastHit2D raycastHitData = Physics2D.Raycast(enemyPosition, Vector2.down, Mathf.Infinity, LayerMask.GetMask("Ground"));
        Vector3 offset = new Vector3(0, -raycastHitData.distance + 1 /* +1 is to accomodate the moon dust collider height */, 0);

        Instantiate(moonDustPrefab, enemy.transform.position + offset, Quaternion.identity);

        Animator enemyAnimator = enemy.GetComponentInChildren<Animator>();
        enemyAnimator.SetBool("isAlive", false);
        AnimationClip deathAnimation = enemyAnimator.runtimeAnimatorController.animationClips.FirstOrDefault((x) => x.name == "Death");
        IEnumerator destroyCoroutine = DestoryEnemyGameObject(deathAnimation.length, enemy);
        StartCoroutine(destroyCoroutine);
    }

    private void OnDisable()
    {
        Publisher.publish.EnemyDeath -= OnEnemyDeath;
    }

    IEnumerator DestoryEnemyGameObject(float secondsToWait, GameObject enemy)
    {
        yield return new WaitForSeconds(secondsToWait + Settings.EnemyDeathDestroyOffset);
        Destroy(enemy);
    }
}
