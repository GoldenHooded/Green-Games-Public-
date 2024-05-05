using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinManager : MonoBehaviour
{
	public bool type1;
	public bool type2;
	public bool type3;
	public bool type4;

	[SerializeField] private GameObject[] type1Objects;
	[SerializeField] private GameObject[] type1SecondaryObjects;
	[SerializeField] private PlayerControllerJoystick[] type1players;
	[SerializeField] private Aspiradora[] aspiradoras;
	[SerializeField] private Seed[] seeds;
	private bool allnull;

	public Animator anim;
	public Image image;
	public bool hasTimer;
	[SerializeField] private Text timerText;
	[SerializeField] private int mins;
	[SerializeField] private int secs;

	private bool trigger;

	private void Start()
	{
		if (hasTimer) Invoke("Timer", 3.51f);
	}

	private void Timer()
	{
		secs--;

		if (secs < 0 && mins != 0)
		{
			mins--;
			secs = 59;
		}
		else if (secs < 0)
		{
			secs = 0;
			anim.SetBool("Finished", true);	
		}

		string minsText = mins > 9 ? mins.ToString() : "0" + mins.ToString();
		string secsText = secs > 9 ? secs.ToString() : "0" + secs.ToString();

		timerText.text = minsText + ":" + secsText;

		Invoke("Timer", 1f);
	}

	private void Update()
	{
		if (type1)
		{
			allnull = true;
			for (int i = 0; i < type1Objects.Length; i++)
			{
				allnull = type1Objects[i].gameObject == null ? allnull : false;
				//                            null => allnull = allnull; !null => all null = false
			}

			anim.SetBool("Finished", allnull);

			int iMaxPoints = 0;
			for (int i = 0; i < type1players.Length; i++)
			{
				if (type1players[i].points > iMaxPoints)
				{
					iMaxPoints = type1players[i].points;
					image.color = type1players[i].GetComponent<SpriteRenderer>().color;
				}
			}

			if (allnull && !trigger)
			{
				PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + iMaxPoints);
				trigger = true;
			}
			else
			{
				trigger = false;
			}
		}
		else if (type2)
		{
			allnull = true;
			for (int i = 0; i < type1SecondaryObjects.Length; i++)
			{
				allnull = type1SecondaryObjects[i].gameObject == null ? allnull : false;
				//                            null => allnull = allnull; !null => all null = false
			}
			if (!allnull)
			{
				allnull = true;
				for (int i = 0; i < type1Objects.Length; i++)
				{
					allnull = type1Objects[i].gameObject == null ? allnull : false;
					//                            null => allnull = allnull; !null => all null = false
				}
			}

			anim.SetBool("Finished", allnull);

			int iMaxPoints = 0;
			for (int i = 0; i < type1players.Length; i++)
			{
				if (type1players[i].points > iMaxPoints)
				{
					iMaxPoints = type1players[i].points;
					image.color = type1players[i].GetComponent<SpriteRenderer>().color;
				}
			}

			if (allnull && !trigger)
			{
				PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + iMaxPoints);
				trigger = true;
			}
			else
			{
				trigger = false;
			}
		}
		else if (type3)
		{
			allnull = true;
			for (int i = 0; i < type1Objects.Length; i++)
			{
				allnull = type1Objects[i].gameObject == null ? allnull : false;
				//                            null => allnull = allnull; !null => all null = false
			}

			anim.SetBool("Finished", allnull);

			int iMaxPoints = 0;
			for (int i = 0; i < aspiradoras.Length; i++)
			{
				if (aspiradoras[i].points > iMaxPoints)
				{
					iMaxPoints = aspiradoras[i].points;
					image.color = aspiradoras[i].GetComponent<SpriteRenderer>().color;
				}
			}

			if (allnull && !trigger)
			{
				PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + iMaxPoints);
				trigger = true;
			}
			else
			{
				trigger = false;
			}
		}
		else if (type4)
		{
			int redAmount = 0;
			int yellowAmount = 0;
			int greenAmount = 0;
			int blueAmount = 0;

			Color finalColor = Color.white;
			
			for (int i = 0; i < seeds.Length; i++)
			{
				Seed seed = seeds[i];

				if (seed.owner == "Red")
				{
					redAmount++;
					if (redAmount > yellowAmount && redAmount > greenAmount && redAmount > blueAmount)
					{
						finalColor = seed.GetComponent<SpriteRenderer>().color;
					}
				}
				if (seed.owner == "Yellow")
				{
					yellowAmount++;
					if (yellowAmount > redAmount && yellowAmount > greenAmount && yellowAmount > blueAmount)
					{
						finalColor = seed.GetComponent<SpriteRenderer>().color;
					}
				}
				if (seed.owner == "Green")
				{
					greenAmount++;
					if (greenAmount > yellowAmount && greenAmount > redAmount && greenAmount > blueAmount)
					{
						finalColor = seed.GetComponent<SpriteRenderer>().color;
					}
				}
				if (seed.owner == "Blue")
				{
					blueAmount++;
					if (blueAmount > yellowAmount && blueAmount > greenAmount && blueAmount > redAmount)
					{
						finalColor = seed.GetComponent<SpriteRenderer>().color;
					}
				}
			}
			
			image.color = finalColor;
		}
	}

	public void MainMenu()
	{
		SceneManager.LoadScene(0);
	}

	public static void Win(Color color)
	{
		WinManager winManager = FindObjectOfType<WinManager>();

		winManager.image.color = color;
		winManager.anim.SetBool("Finished", true);
	}
	public static void Win(Color color, int coins)
	{
		WinManager winManager = FindObjectOfType<WinManager>();

		PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + coins);

		winManager.image.color = color;
		winManager.anim.SetBool("Finished", true);
	}
}
