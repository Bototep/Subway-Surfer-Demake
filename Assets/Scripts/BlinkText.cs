using TMPro;
using UnityEngine;
using System.Collections;

public class InstantBlinkText : MonoBehaviour
{
	public TextMeshProUGUI tmpText; // Reference to the TMP text component
	public float blinkInterval = 1f; // Time in seconds between visibility toggles

	private bool isBlinking = true;

	void Start()
	{
		// Automatically find the TMP component if not assigned
		if (tmpText == null)
			tmpText = GetComponent<TextMeshProUGUI>();

		// Start the blinking coroutine
		if (tmpText != null)
			StartCoroutine(Blink());
		else
			Debug.LogError("TextMeshProUGUI component is missing!");
	}

	private IEnumerator Blink()
	{
		while (isBlinking)
		{
			// Toggle the text visibility
			SetAlpha(tmpText.color.a == 1f ? 0f : 1f);

			// Wait for the specified interval
			yield return new WaitForSeconds(blinkInterval);
		}
	}

	private void SetAlpha(float alpha)
	{
		Color color = tmpText.color;
		tmpText.color = new Color(color.r, color.g, color.b, alpha);
	}

	public void StopBlinking()
	{
		isBlinking = false;
		StopCoroutine(Blink());
	}
}
