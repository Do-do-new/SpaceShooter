using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;

	private GameController gameControllerScript;

	void Start()
	{
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameControllerScript = gameControllerObject.GetComponent<GameController>();
		}
		if (gameControllerScript == null)
		{
			Debug.Log ("Can not find 'Game Controller' script");
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Boundary")
		{
			return;
		}
		Instantiate (explosion, transform.position, transform.rotation);
		if (other.tag == "Player") 
		{
			Instantiate (playerExplosion, transform.position, transform.rotation);
			gameControllerScript.ProcessGameOver ();
		}
		if (other.tag == "Shot")
		{
			if (gameObject.tag == "Hazard")
				gameControllerScript.AddScore (gameControllerScript.HazardScore);
			if (gameObject.tag == "Enemy")
				gameControllerScript.AddScore (gameControllerScript.EnemyScore);
		}
		Destroy (other.gameObject);
		Destroy (gameObject);
	}
}
