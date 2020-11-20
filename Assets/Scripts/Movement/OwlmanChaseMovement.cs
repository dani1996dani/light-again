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
    float chaseSpeed;
    OwlmanType owlmanType;

    private void Start()
    {
        allChildren = gameObject.GetAllChildren();
        rightHandGameObject = allChildren.FirstOrDefault(child => child.tag == Settings.TagOwlmanRightHand);
        leftHandGameObject = allChildren.FirstOrDefault(child => child.tag == Settings.TagOwlmanLeftHand);

        owlmanType = gameObject.GetComponent<OwlmanActionManager>().GetOwlmanType();
        if (owlmanType == OwlmanType.Regular)
        {
            chaseSpeed = Settings.OwlmanChaseSpeed;
        }
        if (owlmanType == OwlmanType.Strong)
        {
            chaseSpeed = Settings.OwlmanStrongChaseSpeed;
        }
    }

    public bool ShouldChase()
    {
        return isPlayerVisibleOnLeftSide() || isPlayerVisibleOnRightSide();
    }


    public void Chase(Vector3 direction)
    {
        gameObject.transform.position += direction * Time.deltaTime * chaseSpeed;
    }

    public bool isPlayerVisibleOnLeftSide()
    {
        return Physics2D.Raycast(leftHandGameObject.transform.position, Vector2.left, Settings.OwlmanStartChasingVisionRange, LayerMask.GetMask("Player"));
    }

    public bool isPlayerVisibleOnRightSide()
    {
        return Physics2D.Raycast(rightHandGameObject.transform.position, Vector2.right, Settings.OwlmanStartChasingVisionRange, LayerMask.GetMask("Player"));
    }
}