using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class MoonDustDrop : MonoBehaviour
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
        Debug.Log("raycastHitData" + raycastHitData.distance);
        Vector3 offset = new Vector3(0, -raycastHitData.distance + 1 /* +1 is to accomodate the moon dust sprite height */, 0);
        Instantiate(moonDustPrefab, enemy.transform.position + offset, Quaternion.identity);
        Destroy(enemy);
    }

    private void OnDisable()
    {
        Publisher.publish.EnemyDeath -= OnEnemyDeath;
    }
}
