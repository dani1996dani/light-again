using Assets.Scripts;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private float playerMovementSpeed;
    private Rigidbody2D playerRigidBody;
    private float jumpForce;
    private float distToGround;

    public bool isFacingRight = true;

    private void Awake()
    {
        playerMovementSpeed = Settings.PlayerMovementSpeed;
        jumpForce = Settings.JumpForce;
        distToGround = Settings.DistToGround;

        playerRigidBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!Settings.isGamePaused)
        {
            MirrorPlayer();
            MoveHorizontally();
            Jump();
        }
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
        bool didPreviouslyFaceRight = isFacingRight;

        if (horizontalMovementValue < 0)
        {
            isFacingRight = false;
        }
        else if (horizontalMovementValue > 0)
        {
            isFacingRight = true;
        }

        if (didPreviouslyFaceRight != isFacingRight)
        {
            Vector3 vector = gameObject.transform.localScale;
            vector.x = isFacingRight ? 1 : -1;
            gameObject.transform.localScale = vector;
        }
    }
}