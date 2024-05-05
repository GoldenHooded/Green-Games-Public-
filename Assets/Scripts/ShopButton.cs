using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButton : MonoBehaviour
{
	[SerializeField] private Animator anim;
	[SerializeField] private bool usePrefs;
	[SerializeField] private string key;
	
	public void Trigger()
	{
		anim.SetTrigger("Pressed");
	}
	
	public void Pressed()
	{
		FindObjectOfType<MusicManager>().SwitchAlwMusic();
	}
	
	private void Start() 
	{
		if (usePrefs && PlayerPrefs.GetInt(key, 1) == 0)
		{
			anim.SetTrigger("Pressed");
		}
	}
}
