﻿using UnityEngine;

namespace Assets.Scripts
{
    public static class LayerHelper
    {
        public static int LayermaskToLayer(LayerMask layerMask)
        {
            int layerNumber = 0;
            int layer = layerMask.value;
            while (layer > 0)
            {
                layer = layer >> 1;
                layerNumber++;
            }
            return layerNumber - 1;
        }
    }
}
