using UnityEngine;

public class OwlmanChaseMovement : MonoBehaviour {

    GameObject rightHandGameObject;
    GameObject leftHandGameObject;

    private void Awake() {
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
        }
        if(isPlayerVisibleOnRightSide()){
            Debug.Log("Player on the right!1");
        }
        
    }

    private void isPlayerVisibleOnRightSide(){
        return Physics2D.Raycast(leftHandGameObject.transform.position, Vector2.left, Settings.OwlmanStartChasingVisionRange, LayerMask.GetMask("Ground"));
    }

    private void isPlayerVisibleOnLeftSide(){
        return Physics2D.Raycast(rightHandGameObject.transform.position, Vector2.right, Settings.OwlmanStartChasingVisionRange, LayerMask.GetMask("Ground"));
    }
}