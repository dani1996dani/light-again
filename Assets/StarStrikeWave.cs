using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class StarStrikeWave : MonoBehaviour
{
    float maxScale = 800.0f;
    float expansionVelocity;
    float fadeOutVelocity;
    bool isExpanding = true;
    SpriteRenderer expansionSpriteRenderer;
    float timeBetweenExpansionAndFadeOut = 0.3f;
    float negleableAlphaOffset = 0.01f;
    private HashSet<int> damageableLayers;
    HashSet<int> alreadyDamagedEnemiesGuid = new HashSet<int>();

    private void Start()
    {
        damageableLayers = new HashSet<int>
            {
                LayerHelper.LayermaskToLayer(LayerMask.GetMask("Enemies")),
                LayerHelper.LayermaskToLayer(LayerMask.GetMask("EnemyBoss"))
            };

        expansionSpriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        StartCoroutine("StopExpanding");
    }

    // Update is called once per frame
    void Update()
    {
        if (isExpanding)
        {
            float newScaleValue = Mathf.SmoothDamp(transform.localScale.x, maxScale, ref expansionVelocity, Settings.StarStrikeWaveExpansionTime);
            transform.localScale = new Vector3(newScaleValue, newScaleValue, 1);
        } else
        {
            float newAlphaValue = Mathf.SmoothDamp(expansionSpriteRenderer.color.a, 0, ref fadeOutVelocity, Settings.StarStrikeWaveFadeOutTime);
            Color currentcolor = expansionSpriteRenderer.color;
            expansionSpriteRenderer.color = new Color(currentcolor.r, currentcolor.g, currentcolor.b, newAlphaValue);
            if(newAlphaValue <= negleableAlphaOffset)
            {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator StopExpanding()
    {
        yield return new WaitForSeconds(Settings.StarStrikeWaveExpansionTime + timeBetweenExpansionAndFadeOut);
        isExpanding = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject colGameObject = collision.gameObject;
        if (damageableLayers.Contains(colGameObject.layer))
        {
            int enemyGuid = colGameObject.GetComponent<ObjectGuid>().GetGuid();
            if (!alreadyDamagedEnemiesGuid.Contains(enemyGuid))
            {
                alreadyDamagedEnemiesGuid.Add(enemyGuid);

                Health enemyHealth = colGameObject.GetComponent<Health>();
                enemyHealth.TakeDamage(Settings.StarStrikeDamage);
            }
        }
    }
}
