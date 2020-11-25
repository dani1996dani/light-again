using Assets.Scripts;
using UnityEngine;
using Assets.Scripts.Attacks;

public class PlayerMovement : MonoBehaviour
{

    private float playerMovementSpeed;
    private Rigidbody2D playerRigidBody;
    private float jumpForce;
    private float distToGround;
    private Animator playerAnimator;

    public bool isFacingRight = true;
    private bool shouldJump = false;
    private PlayerBasicAttack basicAttackController;

    private void Awake()
    {
        playerMovementSpeed = Settings.PlayerMovementSpeed;
        jumpForce = Settings.JumpForce;
        distToGround = Settings.DistToGround;

        GameObject playerGameObject = GameObject.FindGameObjectWithTag(Settings.TagPlayer);
        playerRigidBody = playerGameObject.GetComponent<Rigidbody2D>();
        playerAnimator = playerGameObject.GetComponentInChildren<Animator>();
        basicAttackController = playerGameObject.GetComponentInChildren<PlayerBasicAttack>();
    }

    private void Update()
    {
        if (Settings.isLevelBeingTransitioned)
        {
            playerAnimator.SetFloat(Settings.PlayerHorizontalSpeed, 0);
            playerAnimator.SetBool("isJumping", false);
        }

        if (!Settings.isGamePaused && !basicAttackController.isAttacking)
        {
            MirrorPlayer();
            Jump();
        }
    }

    private void FixedUpdate()
    {
        if (!Settings.isGamePaused && !basicAttackController.isAttacking)
        {
            MoveHorizontally();
        }
    }

    private void MoveHorizontally()
    {
        float horizontalMovementValue = Input.GetAxis("Horizontal");
        Vector3 movementDirection = Vector3.right * horizontalMovementValue;
        gameObject.transform.position += movementDirection * playerMovementSpeed * Time.deltaTime;
        playerAnimator.SetFloat(Settings.PlayerHorizontalSpeed, Mathf.Abs(horizontalMovementValue));
    }

    private void Jump()
    {
        bool isPlayerGrounded = isGrounded();
        bool isJumping = playerAnimator.GetBool("isJumping");

        if (isPlayerGrounded && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            shouldJump = true;
            playerRigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (!isPlayerGrounded && shouldJump)
        {
            shouldJump = false;
            playerAnimator.SetBool("isJumping", true);
        }

        if (isJumping && isPlayerGrounded)
        {
            playerAnimator.SetBool("isJumping", false);
        }
    }



    public bool isGrounded()
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