using UnityEngine;
using Assets.Scripts.Helpers;
using System.Linq;
using System.Collections.Generic;

namespace Assets.Scripts.Attacks
{
    public class OwlmanAttack : MonoBehaviour
    {
        GameObject rightHandGameObject;
        GameObject leftHandGameObject;
        List<GameObject> allChildren;

        private void Awake()
        {
            allChildren = gameObject.GetAllChildren();
            rightHandGameObject = allChildren.FirstOrDefault(child => child.tag == Settings.TagOwlmanRightHand);
            leftHandGameObject = allChildren.FirstOrDefault(child => child.tag == Settings.TagOwlmanLeftHand);
        }

        public bool isPlayerInAttackRange()
        {
            Debug.Log("transform.forward " + transform.forward);

            return Physics2D.Raycast(transform.position, transform.forward, Settings.OwlmanAttackRange * 2, LayerMask.GetMask("Player"));
        }
    }
}