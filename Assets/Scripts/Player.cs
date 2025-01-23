using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
	private CharacterController character;
	private Vector3 direction;

	public float jumpForce = 8f;
	public float gravity = 9.81f * 2f;
	public float fastFallMultiplier = 6f; 

	private float originalHeight; 
	private Vector3 originalCenter; 
	public float crouchHeight = 1f;

	private GameManager gameManager;

	private void Awake()
	{
		gameManager = GameManager.Instance;
		character = GetComponent<CharacterController>();
		originalHeight = character.height; 
		originalCenter = character.center; 
	}

	private void OnEnable()
	{
		direction = Vector3.zero;
	}

	private void Update()
	{
		HandleGravityAndJump();
		HandleCrouch();

		character.Move(direction * Time.deltaTime);
	}

	private void HandleGravityAndJump()
	{
		if (!character.isGrounded)
		{
			if (Input.GetKey(KeyCode.LeftControl))
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

			if (Input.GetButton("Jump"))
			{
				direction = Vector3.up * jumpForce;
			}
		}
	}

	private void HandleCrouch()
	{
		if (Input.GetKey(KeyCode.LeftControl))
		{
			character.height = crouchHeight;
			character.center = new Vector3(originalCenter.x, originalCenter.y - (originalHeight - crouchHeight) / 2, originalCenter.z);
		}
		else
		{
			character.height = originalHeight;
			character.center = originalCenter;
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
}