using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
	public bool allowMusic = true;
	
	public bool onPlayMode = false;

	[SerializeField] private AudioSource audioSource1;
	[SerializeField] private AudioSource audioSource2;
	public float targetVol;
	[SerializeField] private float volLerpSpeed;
	
	void Start()
	{
		targetVol = audioSource1.volume;
		audioSource1.volume = 0;
		
		allowMusic = PlayerPrefs.GetInt("AllowMusic", 1) == 1;
		
		MusicManager[] musicManager = FindObjectsOfType<MusicManager>();
		if (musicManager.Length > 1)
		{
			for (int i = 0; i < musicManager.Length; i++)
			{
				if (musicManager[i] != this)
				{
					onPlayMode = true;
				}
			}
			Destroy(gameObject);
		}
		else
		{
			DontDestroyOnLoad(gameObject);
		}
	}
	
	private void Update() 
	{
		if (SceneManager.GetActiveScene().buildIndex <= 1) onPlayMode = false;
		else onPlayMode = true;
		
		if (allowMusic)
		{
			audioSource1.volume = Mathf.Lerp(audioSource1.volume, onPlayMode ? 0 : targetVol, volLerpSpeed * Time.deltaTime); 
			audioSource2.volume = Mathf.Lerp(audioSource2.volume, onPlayMode ? targetVol : 0, volLerpSpeed * Time.deltaTime); 
		}
		else
		{
			audioSource1.volume = Mathf.Lerp(audioSource1.volume, 0, volLerpSpeed * Time.deltaTime);
			audioSource2.volume = Mathf.Lerp(audioSource2.volume, 0, volLerpSpeed * Time.deltaTime); 
		}
	}
	
	public void SwitchAlwMusic()
	{
		allowMusic = !allowMusic;
		
		PlayerPrefs.SetInt("AllowMusic", allowMusic ? 1 : 0);
	}
}
