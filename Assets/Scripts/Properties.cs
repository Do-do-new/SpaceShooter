using UnityEngine;
using System.Collections;

public class Properties : MonoBehaviour {

	public float MaxScale
	{
		get 
		{
			float maxScale = transform.localScale.x;
			if (transform.localScale.y > maxScale)
				maxScale = transform.localScale.y;
			if (transform.localScale.z > maxScale)
				maxScale = transform.localScale.z;
			return maxScale;
		}
	}

	public float XLeft
	{
		get 
		{
			return transform.position.x - transform.localScale.x / 2.0f;
		}
	}

	public float XRight
	{
		get 
		{
			return transform.position.x + transform.localScale.x / 2.0f;
		}
	}

	public float ZNear
	{
		get 
		{
			return transform.position.z - transform.localScale.z / 2.0f;
		}
	}

	public float ZFar
	{
		get 
		{
			return transform.position.z + transform.localScale.z / 2.0f;
		}
	}
}
