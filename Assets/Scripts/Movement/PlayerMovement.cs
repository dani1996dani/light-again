using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private float playerMovementSpeed = 15.0f;
    private Rigidbody2D playerRigidBody;
    private float jumpForce = 40.0f;
    float distToGround;

    private void Awake()
    {
        playerRigidBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        distToGround = 2.1f;
    }

    private void Update()
    {
        MoveHorizontally();
        Jump();
    }

    private void MoveHorizontally()
    {
        float horizontalMovementValue = Input.GetAxis("Horizontal");
        Vector3 movementDirection = Vector3.right * horizontalMovementValue;
        gameObject.transform.position += movementDirection * playerMovementSpeed * Time.deltaTime;
    }

    private void Jump()
    {
        bool isPlayerGrounded = isGrounded();
        
        if (isPlayerGrounded && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            playerRigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private bool isGrounded()
    {
        return Physics2D.Raycast(gameObject.transform.position, Vector3.down, distToGround, LayerMask.GetMask("Ground"));
    }
}