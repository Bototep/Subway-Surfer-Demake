using TMPro;
using UnityEngine;
using System.Collections;

public class InstantBlinkText : MonoBehaviour
{
	public TextMeshProUGUI tmpText; 
	public float blinkInterval = 1f; 

	private bool isBlinking = true;

	void Start()
	{
		if (tmpText == null)
			tmpText = GetComponent<TextMeshProUGUI>();

		if (tmpText != null)
			StartCoroutine(Blink());
		else
			Debug.LogError("TextMeshProUGUI component is missing!");
	}

	private IEnumerator Blink()
	{
		while (isBlinking)
		{
			SetAlpha(tmpText.color.a == 1f ? 0f : 1f);

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
