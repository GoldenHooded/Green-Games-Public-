using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePlayer : MonoBehaviour
{
	public bool active;
	
	private Animator anim;
	
	private StartButton startButton;
	
	public enum ScreenSide
	{
		upLeft = 1,
		downLeft = 2,
		upRight = 3,
		downRight = 4
	}

	[SerializeField] private ScreenSide side;
	
	private void Start()
	{
		anim = GetComponent<Animator>();
		startButton = FindObjectOfType<StartButton>();
	}
	
	public void Change()
	{
		active = !active;
		anim.SetBool("Active", active);
		
		if (side == ScreenSide.upLeft)
			startButton.upLeft = active;
		if (side == ScreenSide.downLeft)
			startButton.downLeft = active;
		if (side == ScreenSide.upRight)
			startButton.upRight = active;
		if (side == ScreenSide.downRight)
			startButton.downRight = active;
	}
}
