using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
	public bool started;
	
	[SerializeField] private Text text;
	
	private void Start() 
	{
		text.text = "3";
	}
	
	public static bool Started()
	{
		bool started = FindObjectOfType<StartScreen>().started;

		return started;
	}
	
	public void Change()
	{
		if (text.text == "3")
		{
			text.text = "2";
		}
		else if (text.text == "2")
		{
			text.text = "1";
		}
		else if (text.text == "1")
		{
			text.text = "GO!";
		}
	}
}
