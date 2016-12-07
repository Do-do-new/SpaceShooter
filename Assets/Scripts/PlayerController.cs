using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float tilt;
	public GameObject boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;

	private Rigidbody rb;
	private Properties boundaryProperties; 

	private float nextFire = 0.0f;

	private AudioSource aud;

	void Start()
	{
		aud = GetComponent<AudioSource> ();
		rb = GetComponent<Rigidbody> ();
		boundaryProperties = boundary.GetComponent("Properties") as Properties;
	}

	void FixedUpdate()
	{
		Move ();
	}

	void Update()
	{
		Shoot ();
	}

	void Move()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal, 0f,  moveVertical);
		rb.velocity = movement * speed;

		rb.position = new Vector3 
			(
				Mathf.Clamp(rb.position.x, boundaryProperties.XLeft + transform.localScale.x/2.0f, boundaryProperties.XRight - transform.localScale.x/2.0f),
				0f,
				Mathf.Clamp(rb.position.z, boundaryProperties.ZNear + transform.localScale.z/2.0f, boundaryProperties.ZFar - transform.localScale.z/2.0f)
			);

		// when we move left, x speed is negative, but since unity is left-handed, we want to tilt positive amount when moving left for it to look realistic
		// and vice versa for movinf right
		rb.rotation = Quaternion.Euler(new Vector3(0f, 0f, - rb.velocity.x * tilt));	
	}

	void Shoot()
	{
		if (Input.GetButton("Fire1") && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			aud.Play ();
		}
	}
}
