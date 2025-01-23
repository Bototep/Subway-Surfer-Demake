using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Ground : MonoBehaviour
{
	private MeshRenderer meshRenderer;

	// Speed multiplier to slow down the ground movement
	public float speedMultiplier = 0.5f; // Adjust this value to slow the ground movement

	private void Awake()
	{
		meshRenderer = GetComponent<MeshRenderer>();
	}

	private void Update()
	{
		// Calculate the adjusted speed
		float speed = (GameManager.Instance.gameSpeed / transform.localScale.x) * speedMultiplier;

		// Offset the texture
		meshRenderer.material.mainTextureOffset += speed * Time.deltaTime * Vector2.right;
	}
}
