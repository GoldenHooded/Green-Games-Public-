using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SBMethods : MonoBehaviour
{
	private AudioSource audioSource;
	private MusicManager musicManager;
	
	[SerializeField] private bool startButton;
	
	private void Start() 
	{
		audioSource = GetComponent<AudioSource>();
		musicManager = FindObjectOfType<MusicManager>();
	}

	private void Awake() 
	{
		if (startButton) 
		{
			StartButton startButton = FindObjectOfType<StartButton>();
			startButton.startButton = GetComponent<Button>();
			startButton.upLeft = false;
			startButton.upRight = false;
			startButton.downLeft = false;
			startButton.downRight = false;
		}
	}
	
	public void StartPressed()
	{
		audioSource.Play();
		FindObjectOfType<StartButton>().Play();
	}
}