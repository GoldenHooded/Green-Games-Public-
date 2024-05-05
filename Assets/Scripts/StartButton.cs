using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
	public bool upLeft;
	public bool downLeft;
	public bool upRight;
	public bool downRight;
	
	public Button startButton;
	[SerializeField] private int scenesAmount;
	
	private void Start() 
	{
		if (FindObjectsOfType<StartButton>().Length > 1)
		{
			for (int i = 1; i < FindObjectsOfType<StartButton>().Length	; i++)
			{
				Destroy(FindObjectsOfType<StartButton>()[i].gameObject);
			}
		}
		else
		{
			DontDestroyOnLoad(gameObject);
		}
	}
	
	private void Update() 
	{
		int actAmount = 0;
		
		if (upLeft) actAmount++;
		if (downLeft) actAmount++;
		if (upRight) actAmount++;
		if (downRight) actAmount++;

		if (startButton != null)
		{
			if (actAmount >= 2)
			{
				startButton.interactable = true;
			} 
			else
			{
				startButton.interactable = false;
			}
		}
	}
	
	bool trigger1 = false;
	public void Play()
	{
		int rdm = Random.Range(0, scenesAmount);
		Debug.Log(rdm);
		if (startButton)
		{
			if (trigger1) SceneManager.LoadScene(rdm + 2);
			Invoke("Play", 1);
		}
		else
		{
			trigger1 = false;
		}
		
		trigger1 = true;
	}
}