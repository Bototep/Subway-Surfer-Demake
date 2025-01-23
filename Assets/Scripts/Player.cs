using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
	private CharacterController character;
	private Vector3 direction;

	public float jumpForce = 8f;
	public float gravity = 9.81f * 2f;
	public float fastFallMultiplier = 6f; 

	public float originalHeight;
	public Vector3 originalCenter; 
	public float crouchHeight = 1f;

	private Animator animator;
	private GameManager gameManager;

	private float initialXPosition;

	private void Awake()
	{
		gameManager = GameManager.Instance;
		character = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();

		originalHeight = character.height; 
		originalCenter = character.center; 
	}

	private void OnEnable()
	{
		direction = Vector3.zero;
		initialXPosition = transform.position.x;
	}

	private void Update()
	{
		HandleGravityAndJump();
		HandleCrouch();

		character.Move(direction * Time.deltaTime);

		Vector3 position = transform.position;
		position.x = initialXPosition; // Reset x to the initial position
		transform.position = position;
	}

	private void HandleGravityAndJump()
	{
		if (!character.isGrounded)
		{
			if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) || Input.GetMouseButton(1))
			{
				direction += gravity * fastFallMultiplier * Time.deltaTime * Vector3.down;
			}
			else
			{
				direction += gravity * Time.deltaTime * Vector3.down;
			}
		}
		else
		{
			direction = Vector3.down; 

			if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) || Input.GetMouseButton(0))
			{
				direction = Vector3.up * jumpForce;
			}
		}
	}

	private void HandleCrouch()
	{
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) || Input.GetMouseButton(1))
		{
			// Set crouch height
			character.height = crouchHeight;
			character.center = new Vector3(originalCenter.x, originalCenter.y - (originalHeight - crouchHeight) / 2, originalCenter.z);

			// Set roll animation parameter
			if (animator != null)
			{
				animator.SetBool("Roll", true); // Ensure "Roll" matches the parameter in the Animator
			}
		}
		else
		{
			// Restore original height
			character.height = originalHeight;
			character.center = originalCenter;

			// Reset roll animation parameter
			if (animator != null)
			{
				animator.SetBool("Roll", false);
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Obstacle"))
		{
			GameManager.Instance.GameOver();
		}

		if (other.CompareTag("Coin"))
		{
			gameManager.score += 1; 
			Destroy(other.gameObject); 
		}
	}

	public void ResetCrouch()
	{
		CharacterController character = GetComponent<CharacterController>();
		// Reset crouch settings
		character.height = originalHeight;
		character.center = originalCenter;

		// Reset crouch animation state
		if (animator != null)
		{
			animator.SetBool("Roll", false);
		}
	}
}