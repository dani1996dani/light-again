using Assets.Scripts;
using UnityEngine;
using System.Collections.Generic;

public class PlayerMoonDustPickup : MonoBehaviour
{
    MoonDustProgress moonDustProgressController;
    HashSet<int> collectedMoonDustGuids = new HashSet<int>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject colGameObject = collision.gameObject;
        if (colGameObject.tag == Settings.TagMoonDust)
        {    
            int moonDustGuid = colGameObject.GetComponent<ObjectGuid>().GetGuid();
            if (!collectedMoonDustGuids.Contains(moonDustGuid))
            {
                CollectMoonDust(colGameObject, moonDustGuid);
            }
        }
    }

    private void CollectMoonDust(GameObject moonDust, int moonDustGuid)
    {
        collectedMoonDustGuids.Add(moonDustGuid);

        if(moonDustProgressController == null)
        {
            moonDustProgressController = GameObject.FindGameObjectWithTag(Settings.TagGameSettings).GetComponent<MoonDustProgress>();
        }
        moonDustProgressController.IncrementMoonDustAmount();

        Destroy(moonDust);
    }
}