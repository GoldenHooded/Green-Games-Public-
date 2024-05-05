using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
	public int amountOfRounds = 6;
	public int currentRound = 0;
	
	public int redWins = 0;
	public int yellowWins = 0;
	public int greenWins = 0;
	public int blueWins = 0;
	
	[SerializeField] private GameObject[] playerSides;
	[SerializeField] private Color[] playerColors;
	[SerializeField] private GameObject item;
	private GameObject itemInst;
	[SerializeField] private Transform canvas;
	
	[SerializeField] private Sprite[] glassSprites;
	[SerializeField] private Sprite[] paperSprites;
	[SerializeField] private Sprite[] plasticSprites;
	
	private bool canAnswer;
	private int correctAnswer;
	
	
	public void Start()
	{
		Invoke("Round", 4f);
	}
	
	private void Round()
	{
		currentRound++;
		canAnswer = true;
		
		correctAnswer = Random.Range(0, 3);
		itemInst = Instantiate(item, canvas);
		
		if (correctAnswer == 0)
			itemInst.GetComponentInChildren<Image>().sprite = glassSprites[Random.Range(0, glassSprites.Length)];
		else if (correctAnswer == 1)
			itemInst.GetComponentInChildren<Image>().sprite = plasticSprites[Random.Range(0, plasticSprites.Length)];
		else if (correctAnswer == 2)
			itemInst.GetComponentInChildren<Image>().sprite = paperSprites[Random.Range(0, paperSprites.Length)];
	}
	
	public void Answer(string AColor)
	{
		int answer = int.Parse(AColor[..^3]);
		string color = AColor[1..];
		
		if (canAnswer)
		{
			if (answer == correctAnswer)
			{
				Color winColor = Color.white;
				if (color == "Red")
				{
					itemInst.GetComponent<Item>().destination = playerSides[0].transform.position;
					winColor = playerColors[0];
				}
				else if (color == "Yel")
				{
					itemInst.GetComponent<Item>().destination = playerSides[1].transform.position;
					winColor = playerColors[1];
				}
				else if (color == "Gre")
				{
					itemInst.GetComponent<Item>().destination = playerSides[2].transform.position;
					winColor = playerColors[2];
				}
				else if (color == "Blu")
				{
					itemInst.GetComponent<Item>().destination = playerSides[3].transform.position;
					winColor = playerColors[3];
				}
				itemInst.GetComponent<Item>().targetScale = Vector3.zero;
				
				canAnswer = false;
				if (currentRound != amountOfRounds) Invoke("Round", 2f);
				else WinManager.Win(winColor, 10);
			}
		}
	}
}
