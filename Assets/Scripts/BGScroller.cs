using UnityEngine;
using System.Collections;

public class BGScroller : MonoBehaviour 
{
	public float speed;
	private float startPositionZ;

	void Start()
	{
		startPositionZ = transform.position.z;	
	}

	void FixedUpdate()
	{ 
		// transform.localScale.y - exactly y, not x, because background plane is rotated
		float newPosition = Mathf.Repeat (Time.time * speed, transform.localScale.y);
		transform.position = new Vector3(transform.position.x,transform.position.y,startPositionZ + newPosition);
	}
}
