using UnityEngine;
using Assets.Scripts;

public class LayerIgnoreCollision : MonoBehaviour
{
    private void Awake()
    {
        Physics2D.IgnoreLayerCollision(LayerHelper.LayermaskToLayer(LayerMask.GetMask("Player")), LayerHelper.LayermaskToLayer(LayerMask.GetMask("Enemies")), true);
        Physics2D.IgnoreLayerCollision(LayerHelper.LayermaskToLayer(LayerMask.GetMask(Settings.LayerMoonDust)), LayerHelper.LayermaskToLayer(LayerMask.GetMask("Enemies")), true);
    }
}
