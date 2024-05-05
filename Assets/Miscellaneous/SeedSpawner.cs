using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedSpawner : MonoBehaviour
{
	[SerializeField] private GameObject prefab;
	
	[SerializeField] private Transform secondaryTransform;
	
	[SerializeField] private float minDist = 1;
	
	private void Start() 
	{
		float xDiference = secondaryTransform.position.x - transform.position.x;
		float yDiference = secondaryTransform.position.y - transform.position.y;
		
		int xDivider = 1;
		while (Mathf.Abs(xDiference) / xDivider > minDist)
		{
			xDivider++;
		}
		
		int yDivider = 1;
		while (Mathf.Abs(yDiference) / yDivider > minDist)
		{
			yDivider++;
		}
		
		Debug.Log(xDivider + " " + yDivider);
		
		for (int i = 0; i <= xDivider; i++)
		{
			float xOffset = i * (xDiference / xDivider);
			
			for (int j = 0; j <= yDivider; j++)
			{
				float yOffset = j * (yDiference / yDivider);
				
				Instantiate(prefab, transform.position + Vector3.right * xOffset + Vector3.up * yOffset, Quaternion.identity);
			}
		}
		
		Destroy(secondaryTransform.gameObject);
		Destroy(gameObject);
	}
}
