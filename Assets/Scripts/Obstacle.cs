using UnityEngine;

public class Obstacle : MonoBehaviour
{
	private float leftEdge;
	public float speedMultiplier = 0.2f; 

	private void Start()
	{
		leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 2f;
	}

	private void Update()
	{
		transform.position += GameManager.Instance.gameSpeed * speedMultiplier * Time.deltaTime * Vector3.left;

		if (transform.position.x < leftEdge)
		{
			Destroy(gameObject);
		}
	}
}
