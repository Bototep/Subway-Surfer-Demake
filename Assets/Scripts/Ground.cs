using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Ground : MonoBehaviour
{
	private MeshRenderer meshRenderer;

	public float speedMultiplier = 0.5f; 

	private void Awake()
	{
		meshRenderer = GetComponent<MeshRenderer>();
	}

	private void Update()
	{
		float speed = (GameManager.Instance.gameSpeed / transform.localScale.x) * speedMultiplier;

		meshRenderer.material.mainTextureOffset += speed * Time.deltaTime * Vector2.right;
	}
}
