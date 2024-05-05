using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerJoystick : MonoBehaviour
{
	[SerializeField] private MultiplayerJoystick multiplayerJoystick;
	[SerializeField] private float speed;
	[SerializeField] private GameObject plusOne;
	[SerializeField] private GameObject minusOne;
	
	[SerializeField] private bool waterShield;
	[SerializeField] private bool hasWaterShield;
	[SerializeField] private GameObject waterShieldObj;
	[SerializeField] private Vector3 targetSize;
	
	[SerializeField] private string colorName;
	
	public int points;
	
	private Rigidbody2D rb;
	
	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		StartButton startButton = FindObjectOfType<StartButton>();
		if (startButton)
		{
			if (!startButton.upLeft && multiplayerJoystick.side == JoystickSide.UpLeft) 
			{
				Disable();
			}
			if (!startButton.upRight && multiplayerJoystick.side == JoystickSide.UpRight) 
			{
				Disable();
			}
			if (!startButton.downLeft && multiplayerJoystick.side == JoystickSide.DownLeft) 
			{
				Disable();
			}
			if (!startButton.downRight && multiplayerJoystick.side == JoystickSide.DownRight) 
			{
				Disable();
			}
		}
	}
	
	private void Disable()
	{
		multiplayerJoystick.joystickBackground.gameObject.SetActive(false);
		multiplayerJoystick.gameObject.SetActive(false);
		gameObject.SetActive(false);
	}
	
	private void Update() 
	{
		if (waterShield)
		{
			waterShieldObj.transform.localScale = Vector3.Lerp(waterShieldObj.transform.localScale, targetSize, Time.deltaTime * 5);
		}
	}
	
	private void FixedUpdate() 
	{
		rb.velocity = multiplayerJoystick.inputVector * speed;
	}
	
	private void OnCollisionEnter2D(Collision2D other) 
	{
		if (other.gameObject.CompareTag("Garbage"))
		{
			PlusOne();
			Destroy(other.gameObject);
		}
		else if (other.gameObject.CompareTag("Tree"))
		{
			MinusOne();
			Destroy(other.gameObject);
		}
		else if (other.gameObject.CompareTag("Lake"))
		{
			hasWaterShield = true;
			targetSize = Vector3.one * 2.636746f;
		}
		else if (other.gameObject.CompareTag("Fire"))
		{
			if (hasWaterShield)
			{
				targetSize = Vector3.zero;
				hasWaterShield = false;
				PlusOne();
				Destroy(other.gameObject);
			}
		}
	}
	
	private void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.gameObject.CompareTag("Seed"))
		{
			other.gameObject.GetComponent<Seed>().owner = colorName;
			other.gameObject.GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color;
		}
	}
	
	private void PlusOne()
	{
		points++;
		Instantiate(plusOne, transform.position, Quaternion.identity);
	}
	
	public void MinusOne()
	{
		points--;
		Instantiate(minusOne, transform.position, Quaternion.identity);
	}
}
