using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDisabler : MonoBehaviour
{
	[SerializeField] private GameObject[] upLeftToDisable;
	[SerializeField] private GameObject[] upRightToDisable;
	[SerializeField] private GameObject[] downLeftToDisable;
	[SerializeField] private GameObject[] downRightToDisable;

	private void Start() 
	{
		StartButton startButton = FindObjectOfType<StartButton>();
		if (startButton)
		{
			if (!startButton.upLeft) 
			{
				for (int i = 0; i < upLeftToDisable.Length; i++)
				{
					upLeftToDisable[i].SetActive(false);
				}
			}
			if (!startButton.upRight) 
			{
				for (int i = 0;	i < upRightToDisable.Length; ++i)
				{
					upRightToDisable[i].SetActive(false);
				}
			}
			if (!startButton.downLeft) 
			{
				for (int i = 0;	i < downLeftToDisable.Length; ++i)
				{
					downLeftToDisable[i].SetActive(false);
				}
			}
			if (!startButton.downRight) 
			{
				for (int i = 0;	i < downRightToDisable.Length; ++i)
				{
					downRightToDisable[i].SetActive(false);
				}
			}
		}
	}
}
