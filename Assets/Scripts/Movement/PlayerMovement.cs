using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private float playerMovementSpeed = 15.0f;
    private Rigidbody2D playerRigidBody;
    private float jumpForce = 40.0f;
    float distToGround;

    public bool isFacingRight = true;

    private void Awake()
    {
        playerRigidBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        distToGround = 2.1f;
    }

    private void Update()
    {
        MirrorPlayer();
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

        if (isPlayerGrounded && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            playerRigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private bool isGrounded()
    {
        return Physics2D.Raycast(gameObject.transform.position, Vector3.down, distToGround, LayerMask.GetMask("Ground"));
    }

    private void MirrorPlayer()
    {
        float horizontalMovementValue = Input.GetAxis("Horizontal");
        bool didPreviouslyFacedRight = isFacingRight;

        if (horizontalMovementValue < 0)
        {
            isFacingRight = false;
        }
        else if (horizontalMovementValue > 0)
        {
            isFacingRight = true;
        }

        if (didPreviouslyFacedRight != isFacingRight)
        {
            Vector3 vector = gameObject.transform.localScale;
            vector.x = isFacingRight ? 1 : -1;
            gameObject.transform.localScale = vector;
        }
    }
}