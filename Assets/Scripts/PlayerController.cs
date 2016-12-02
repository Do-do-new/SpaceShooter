using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	[System.Serializable]
	public class Boundaries
	{
		public float minX, maxX, minZ, maxZ;
	}

	public float speed;

	public float tilt;

	public Boundaries boundaries;

	private Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody> ();
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal, 0f,  moveVertical);
		rb.velocity = movement * speed;

		rb.position = new Vector3 
		(
			Mathf.Clamp(rb.position.x, boundaries.minX, boundaries.maxX),
			0f,
			Mathf.Clamp(rb.position.z, boundaries.minZ, boundaries.maxZ)
		);

		// when we move left, x speed is negative, but since unity is left-handed, we want to tilt positive amount when moving left for it to look realistic
		// and vice versa for movinf right
		rb.rotation = Quaternion.Euler(new Vector3(0f, 0f, - rb.velocity.x * tilt));
	}
}
