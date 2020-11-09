using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Helpers;
using System.Linq;
using Assets.Scripts;

public class OwlmanChaseMovement : MonoBehaviour {

    GameObject rightHandGameObject;
    GameObject leftHandGameObject;
    List<GameObject> allChildren;

    private void Awake() {
        allChildren = gameObject.GetAllChildren();
        rightHandGameObject = allChildren.FirstOrDefault(child => child.tag == Settings.TagOwlmanRightHand);
        leftHandGameObject = allChildren.FirstOrDefault(child => child.tag == Settings.TagOwlmanLeftHand);
    }

    public bool ShouldChase()
    {
        return isPlayerVisibleOnLeftSide() || isPlayerVisibleOnRightSide();
    }

    public void Chase()
    {
        if (isPlayerVisibleOnLeftSide())
        {
            ChaseInDirection(Vector3.left);
        }
        if (isPlayerVisibleOnRightSide())
        {
            ChaseInDirection(Vector3.right);
        }
    }

    public void ChaseInDirection(Vector3 direction)
    {
        gameObject.transform.position += direction * Time.deltaTime * Settings.OwlmanChaseSpeed;
    }
    
    public bool isPlayerVisibleOnLeftSide(){
        Debug.DrawRay(leftHandGameObject.transform.position, Vector3.left * 10, Color.green);
        return Physics2D.Raycast(leftHandGameObject.transform.position, Vector2.left, Settings.OwlmanStartChasingVisionRange, LayerMask.GetMask("Player"));
    }

    public bool isPlayerVisibleOnRightSide(){
        Debug.DrawRay(leftHandGameObject.transform.position, Vector3.right * 10, Color.blue);
        return Physics2D.Raycast(rightHandGameObject.transform.position, Vector2.right, Settings.OwlmanStartChasingVisionRange, LayerMask.GetMask("Player"));
    }
}