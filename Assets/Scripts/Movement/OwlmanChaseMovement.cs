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
        Debug.Log("heh");
        allChildren = gameObject.GetAllChildren();
        rightHandGameObject = allChildren.FirstOrDefault(child => child.tag == Settings.TagOwlmanRightHand);
        leftHandGameObject = allChildren.FirstOrDefault(child => child.tag == Settings.TagOwlmanLeftHand);
    }

    private void Start() {
        // OwlmanStartChasingVisionRange
        
    }

    private void Update() {
        if(isPlayerVisibleOnLeftSide()){
            Debug.Log("Player on the Left!1");
            Chase(Vector3.left);
        }
        if(isPlayerVisibleOnRightSide()){
            Debug.Log("Player on the right!1");
            Chase(Vector3.right);
        }
    }

    public void Chase(Vector3 direction)
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