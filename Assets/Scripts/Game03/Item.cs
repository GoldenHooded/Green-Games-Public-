using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	public Vector3 destination;
	public Vector3 targetScale;
	private float speed = 7;
	
	void Start()
	{
		destination = transform.position;
		targetScale = transform.localScale;
	}

	void Update()
	{
		transform.position = Vector3.Lerp(transform.position, destination, speed * Time.deltaTime);
		transform.localScale = Vector3.Lerp(transform.localScale, targetScale, speed * Time.deltaTime);
	}
}
