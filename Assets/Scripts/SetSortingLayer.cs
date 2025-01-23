using UnityEngine;

public class SetSortingLayer : MonoBehaviour
{
	public string sortingLayerName = "Ground"; // Name of the sorting layer
	public int sortingOrder = 0;               // Order in layer

	private void Start()
	{
		MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

		if (meshRenderer != null)
		{
			// Set the sorting layer by name
			meshRenderer.sortingLayerName = sortingLayerName;

			// Set the order in layer
			meshRenderer.sortingOrder = sortingOrder;
		}
		else
		{
			Debug.LogWarning("MeshRenderer not found on this GameObject.");
		}
	}
}
