using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject[] hazards;
	public Vector3 spawnBoundaries;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public GUIText scoreText;
	public GUIText gameOverText;
	public GUIText restartText;

	private uint score;

	private bool restartAllowed;

	public uint HazardScore
	{
		get 
		{
			return 1;
		}
	}

	public uint EnemyScore
	{
		get 
		{
			return 5;
		}
	}

	public void AddScore(uint val)
	{
		score += val;
		UpdateScoreText ();
	}

	public void ProcessGameOver()
	{
		restartAllowed = true;
		gameOverText.enabled = true;
		restartText.enabled = true;
	}

	void Start () 
	{
		restartAllowed = false;
		gameOverText.enabled = false;
		restartText.enabled = false;
		score = 0;
		UpdateScoreText ();

		StartCoroutine (SpawnAsteroid ());
	}

	void Update ()
	{
		if (restartAllowed)
		{
			if (Input.GetKeyDown (KeyCode.R))
			{
				SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
			}
		}
	}

	void UpdateScoreText()
	{
		scoreText.text = "Score: " + score;
	}

	IEnumerator SpawnAsteroid()
	{
		yield return new WaitForSeconds (startWait);

		while (true)
		{
			for (uint i = 0; i<hazardCount; ++i) 
			{
				GameObject hazard = hazards [Random.Range (0, hazards.Length)];
				hazard.transform.position = new Vector3 (Random.Range (-spawnBoundaries.x, spawnBoundaries.x), spawnBoundaries.y, spawnBoundaries.z);
				if (hazard.CompareTag ("Hazard"))
				{
					hazard.transform.rotation = Quaternion.identity;
				}
				else if (hazard.CompareTag("Enemy"))
				{
					hazard.transform.rotation = new Quaternion (0, 180, 0, 0);
				}
				Instantiate (hazard, hazard.transform.position, hazard.transform.rotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
		}
	}
}
