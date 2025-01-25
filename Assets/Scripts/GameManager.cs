using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	public float initialGameSpeed = 5f;
	public float gameSpeedIncrease = 0.1f;
	public float gameSpeed { get; private set; }

	[SerializeField] private AudioClip dieClip;
	[SerializeField] private AudioClip hiScoreClip;
	[SerializeField] private AudioClip BGMClip;
	[SerializeField] private TextMeshProUGUI scoreText;
	[SerializeField] private TextMeshProUGUI hiscoreText;
	[SerializeField] private TextMeshProUGUI gameOverText;
	[SerializeField] private Button retryButton;

	private Player player;
	private Spawner spawner;
	private AudioSource audioSource;

	public float score;
	public float Score => score;

	private bool hiScoreAchieved = false; 

	private void Awake()
	{
		if (Instance != null)
		{
			DestroyImmediate(gameObject);
		}
		else
		{
			Instance = this;
		}
	}

	private void OnDestroy()
	{
		if (Instance == this)
		{
			Instance = null;
		}
	}

	private void Start()
	{
		player = FindObjectOfType<Player>();
		spawner = FindObjectOfType<Spawner>();
		audioSource = GetComponent<AudioSource>();

		OnGameStartSFX();
		NewGame();
	}

	public void NewGame()
	{
		Obstacle[] obstacles = FindObjectsOfType<Obstacle>();
		foreach (var obstacle in obstacles)
		{
			Destroy(obstacle.gameObject);
		}

		score = 0f;
		gameSpeed = initialGameSpeed;
		enabled = true;

		hiScoreAchieved = false; 

		if (player != null)
		{
			player.ResetCrouch();
			player.gameObject.SetActive(true);
		}

		if (spawner != null)
		{
			spawner.gameObject.SetActive(true);
		}

		if (gameOverText != null)
		{
			gameOverText.gameObject.SetActive(false);
		}

		if (retryButton != null)
		{
			retryButton.gameObject.SetActive(false);
		}

		UpdateHiscore();
	}

	public void GameOver()
	{
		DieSFX();

		gameSpeed = 0f;
		enabled = false;

		if (player != null)
		{
			player.gameObject.SetActive(false);
		}

		if (spawner != null)
		{
			spawner.gameObject.SetActive(false);
		}

		if (gameOverText != null)
		{
			gameOverText.gameObject.SetActive(true);
		}

		if (retryButton != null)
		{
			retryButton.gameObject.SetActive(true);
		}

		if (audioSource != null && audioSource.clip == BGMClip)
		{
			audioSource.Stop();
		}

		UpdateHiscore();
	}

	private void Update()
	{
		gameSpeed += gameSpeedIncrease * Time.deltaTime;
		//score += gameSpeed * Time.deltaTime;

		if (scoreText != null)
		{
			scoreText.text = Mathf.FloorToInt(score).ToString("D5");
		}

		UpdateHiscore(); 
	}

	private void UpdateHiscore()
	{
		float hiscore = PlayerPrefs.GetFloat("hiscore", 0);

		if (score > hiscore)
		{
			if (!hiScoreAchieved) 
			{
				PlayHiScoreSFX();
				hiScoreAchieved = true;
			}

			hiscore = score;
			PlayerPrefs.SetFloat("hiscore", hiscore);
		}

		if (hiscoreText != null)
		{
			hiscoreText.text = Mathf.FloorToInt(hiscore).ToString("D5");
		}
	}

	private void PlayHiScoreSFX()
	{
		if (hiScoreClip != null && audioSource != null)
		{
			audioSource.PlayOneShot(hiScoreClip);
		}
	}

	public void DieSFX()
	{
		audioSource.clip = dieClip;
		audioSource.Play();
	}

	public void ResetHiscore()
	{
		PlayerPrefs.SetFloat("hiscore", 0); 
		UpdateHiscore(); 
	}

	public void OnRetryButtonPressed()
	{
		SceneManager.LoadScene(0);
	}

	public void OnGameStartSFX()
	{
		if (BGMClip != null && audioSource != null) 
		{
			audioSource.clip = BGMClip;
			audioSource.Play();
		}
	}
}
