using Assets.Scripts;
using UnityEngine;

public class PlayerMoonDustPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject colGameObject = collision.gameObject;
        if (colGameObject.tag == Settings.TagMoonDust)
        {
            CollectMoonDust(colGameObject);
        }
    }

    private void CollectMoonDust(GameObject moonDust)
    {
        Destroy(moonDust);
    }
}