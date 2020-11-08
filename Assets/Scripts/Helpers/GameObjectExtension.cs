using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public static class GameObjectExtension
    {
        public static List<GameObject> GetAllChildren(this GameObject gameObject)
        {
            List<GameObject> childrenList = new List<GameObject>();
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                GameObject currChild = gameObject.transform.GetChild(i).gameObject;
                childrenList.Add(currChild);
            }
            return childrenList;
        }
    }
}
