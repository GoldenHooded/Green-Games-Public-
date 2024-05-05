using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingBackground : MonoBehaviour
{
	[SerializeField] private RawImage backgroundImg;
	[SerializeField] private float x, y;
	
	void Update()
	{
		backgroundImg.uvRect = new Rect
		(
			backgroundImg.uvRect.position + new Vector2(x, y) * Time.deltaTime, 
			backgroundImg.uvRect.size
		);
	}
}
