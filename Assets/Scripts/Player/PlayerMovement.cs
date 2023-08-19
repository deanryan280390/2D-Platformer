using UnityEngine;

namespace Platformer
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Input")]
        private float horizontalInput;
        
        [Header("Physics")]
        private Rigidbody2D rigidBody;
        
        [Header("Keybinds")]
        [SerializeField]
        private KeyCode JumpKey = KeyCode.Space;

        [Header("Movement Attributes")]
        [SerializeField]
        private float movementSpeed;

        [Header("Jumping Attributes")]
        [SerializeField]
        private float jumpForce;
        [SerializeField]
        private float groundDistance;
        [SerializeField]
        private float jumpCooldown;
        private bool isReadyToJump;
        
        [Header("Ground Check")]
        [SerializeField]
        private LayerMask whatIsGround;
        [SerializeField] 
        private float playerGroundOffset = 0.16f;

        private bool isFacingRight = true;
        private SpriteRenderer spriteRenderer;
        
        #region  Unity Functions

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            rigidBody.freezeRotation = true;
            spriteRenderer = transform.GetComponent<SpriteRenderer>();
            isReadyToJump = true;
            groundDistance = GetComponent<BoxCollider2D>().bounds.extents.y + playerGroundOffset; // setting the offset here so not to calculate each frame in update
        }
        
        private void Update()
        {
            horizontalInput = Input.GetAxis(Global.StringIdentifiers.HorizontalInputKey) * movementSpeed;
            Jump();
            FlipFacingDirection();
        }

        private void FixedUpdate()
        {
            MovePlayer(horizontalInput);
        }

        #endregion
        
        #region Movement
        
        /// <summary>
        /// Move player via GetAxis horizontal and pass the move float into the x vector value for rigidBody velocity value
        /// </summary>
        /// <param name="move"></param>
        private void MovePlayer(float move)
        {
            var initialVelocity = rigidBody.velocity;
            rigidBody.velocity = new Vector2(move, initialVelocity.y);
        }

        #endregion
        
        #region Jumping
        
        /// <summary>
        /// All Jump input and functionality is implemented here and called in Update()
        /// </summary>
        private void Jump()
        {
            if (!Input.GetKey(JumpKey)) // if key not pressed we dont want to call this
            {
                return;
            }

            if (!IsGrounded() || !isReadyToJump) // if in the air and ready to jump is false, we dont need to go any further
            {
                return;
            }
            
            isReadyToJump = false;
            rigidBody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse); // add impulse force to apply jump physics 
            Invoke(nameof(ResetJump), jumpCooldown); // call the Reset Jump to have a timer for Jump cooldown, so the player cannot repeatedly press space key  
        }
        
        /// <summary>
        /// IsGrounded checks the player is on the ground by casting ray in the down direction from origin
        /// distance is the ground distance of the player y collider bounds + a player ground offset to is to determine the player has a distance between the player object and platform
        /// Have a layer mask set for ground to check if the object is ground or not , we also have a layer set for player - "what is Player" and this let us know that is the player 
        /// </summary>
        /// <returns>boolean</returns>
        private bool IsGrounded()
        {
            return Physics2D.Raycast(transform.position, Vector3.down, groundDistance, whatIsGround);
        }
        
        /// <summary>
        /// Reset Jump so its ready to jump again after cooldown
        /// </summary>
        private void ResetJump()
        {
            isReadyToJump = true;
        }
        
        #endregion

        /// <summary>
        /// Added Flip facing direction to detect what way the player is movement and respond to the input and face correct direction
        /// Using the flip X field to change sprite direction
        /// isFacingRight is set to true by default
        /// checks is the horizontal input value less than 0 and isFacingdirection is true or ie moving left
        /// isFacingdirection false and horizontal input value greater than 0 ie . moving right
        /// </summary>
        private void FlipFacingDirection()
        {
            if (horizontalInput == 0) // if we are not moving just return, no need to check face direction
            {
                return;
            }

            if (isFacingRight && horizontalInput < 0 || !isFacingRight && horizontalInput > 0)
            {
                spriteRenderer.flipX = isFacingRight;
                isFacingRight = !isFacingRight;
            }
                
                
        }
        
    }
}