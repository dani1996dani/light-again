using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Helpers;
using System.Linq;
using Assets.Scripts;

public class OwlmanChaseMovement : MonoBehaviour
{

    GameObject rightHandGameObject;
    GameObject leftHandGameObject;
    List<GameObject> allChildren;

    private void Start()
    {
        allChildren = gameObject.GetAllChildren();
        rightHandGameObject = allChildren.FirstOrDefault(child => child.tag == Settings.TagOwlmanRightHand);
        leftHandGameObject = allChildren.FirstOrDefault(child => child.tag == Settings.TagOwlmanLeftHand);
    }

    public bool ShouldChase(float visionRange)
    {
        return isPlayerVisibleOnLeftSide(visionRange) || isPlayerVisibleOnRightSide(visionRange);
    }


    public void Chase(Vector3 direction, float chaseSpeed)
    {
        gameObject.transform.position += direction * Time.deltaTime * chaseSpeed;
    }

    public bool isPlayerVisibleOnLeftSide(float visionRange)
    {
        return Physics2D.Raycast(leftHandGameObject.transform.position, Vector2.left, visionRange, LayerMask.GetMask("Player"));
    }

    public bool isPlayerVisibleOnRightSide(float visionRange)
    {
        return Physics2D.Raycast(rightHandGameObject.transform.position, Vector2.right, visionRange, LayerMask.GetMask("Player"));
    }
}