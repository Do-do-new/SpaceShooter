using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject asteroid;
	public GameObject boundary;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	private float leftBoundary;
	private float rightBoundary;
	private Properties boundaryProperties;

	void Start () 
	{
		boundaryProperties = boundary.GetComponent("Properties") as Properties;
		Properties asteroidProperties = asteroid.GetComponent("Properties") as Properties;
		// make sure that asteroid always completely on screen
		leftBoundary  = boundaryProperties.XLeft  + asteroidProperties.MaxScale / 2.0f;
		rightBoundary = boundaryProperties.XRight - asteroidProperties.MaxScale / 2.0f;

		StartCoroutine (SpawnAsteroid ());
	}

	IEnumerator SpawnAsteroid()
	{
		yield return new WaitForSeconds (startWait);

		while (true)
		{
			for (uint i = 0; i<hazardCount; ++i) 
			{
				asteroid.transform.position = new Vector3 (Random.Range (leftBoundary, rightBoundary), boundary.transform.position.y, boundaryProperties.ZFar);
				asteroid.transform.rotation = Quaternion.identity;
				Instantiate (asteroid, asteroid.transform.position, asteroid.transform.rotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
		}
	}

}
