using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Arrow : MonoBehaviour
    {
        private float timeToLive = Settings.ArrowTimeToLive;
        private int damage = 10;
        private HashSet<int> hitableLayers;
        private HashSet<int> damageableLayers;


        private void Awake()
        {
            hitableLayers = new HashSet<int>
            {
                LayerHelper.LayermaskToLayer(LayerMask.GetMask("Ground")),
                LayerHelper.LayermaskToLayer(LayerMask.GetMask("Enemies")),
                LayerHelper.LayermaskToLayer(LayerMask.GetMask("EnemyBoss"))
            };

            damageableLayers = new HashSet<int>
            {
                LayerHelper.LayermaskToLayer(LayerMask.GetMask("Enemies")),
                LayerHelper.LayermaskToLayer(LayerMask.GetMask("EnemyBoss"))
            };
        }

        private void Update()
        {
            timeToLive -= Time.deltaTime;
            if (timeToLive <= 0 || DidRegularArrowCollideWithGround())
            {
                Destroy(gameObject);
            }
        }

        private bool DidRegularArrowCollideWithGround()
        {
            if(gameObject.tag != Settings.TagRegularArrow)
            {
                return false;
            }

            bool didHitGroundLeft = Physics2D.Raycast(gameObject.transform.position, Vector2.left, 1.7f, LayerMask.GetMask(Settings.TagGround));
            if (didHitGroundLeft)
            {
                return true;
            }
            bool didHitGroundRight = Physics2D.Raycast(gameObject.transform.position, Vector2.right, 1.7f, LayerMask.GetMask(Settings.TagGround));
            if (didHitGroundRight)
            {
                return true;
            }

            return false;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (hitableLayers.Contains(col.gameObject.layer))
            {
                if(damageableLayers.Contains(col.gameObject.layer))
                {
                    col.gameObject.GetComponent<Health>().TakeDamage(damage);
                }

                Destroy(gameObject);
            }
        }
    }
}

