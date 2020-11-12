using Assets.Scripts;
using UnityEngine;

public class PlayerMoonDustPickup : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        GameObject colGameObject = col.gameObject;
        if(colGameObject.tag == Settings.TagMoonDust)
        {
            CollectMoonDust(colGameObject);
        }
    }

    private void CollectMoonDust(GameObject moonDust)
    {
        Destroy(moonDust);
    }
}