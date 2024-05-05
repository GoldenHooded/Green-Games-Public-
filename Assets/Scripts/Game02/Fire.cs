using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
	public float speed = 5f; // Variable para controlar la velocidad

	[SerializeField] private Color treeColor2;
	[SerializeField] private Color treeColor3;
	[SerializeField] private Color treeColor4;
	[SerializeField] private Color treeColor5;
	[SerializeField] private Color treeColor6;
	[SerializeField] private Color treeColor7;


	void Start()
	{
		ChangeVelocity();
	}

	private void ChangeVelocity()
	{
		Rigidbody2D rb = GetComponent<Rigidbody2D>();

		// Genera un ángulo aleatorio en radianes dentro del rango de 0 a 2π
		float angle = Random.Range(0f, 2f * Mathf.PI);

		// Calcula las componentes x e y de la velocidad inicial utilizando funciones trigonométricas
		float velocityX = Mathf.Cos(angle) * speed;
		float velocityY = Mathf.Sin(angle) * speed;

		// Asigna la velocidad inicial al Rigidbody2D
		rb.velocity = new Vector2(velocityX, velocityY);

		Invoke("ChangeVelocity", Random.Range(2, 10));
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Tree2"))
		{
			SpriteRenderer spriteRenderer = other.gameObject.GetComponent<SpriteRenderer>();
			
			if (spriteRenderer.gameObject.name == "Tree")
			{
				spriteRenderer.color = treeColor2;
				spriteRenderer.gameObject.name = "Tree1";
			}
			else if (spriteRenderer.gameObject.name == "Tree1")
			{
				spriteRenderer.color = treeColor3;
				spriteRenderer.gameObject.name = "Tree2";
			}
			else if (spriteRenderer.gameObject.name == "Tree2")
			{
				spriteRenderer.color = treeColor4;
				spriteRenderer.gameObject.name = "Tree3";
			}
			if (spriteRenderer.gameObject.name == "Tree3")
			{
				spriteRenderer.color = treeColor5;
				spriteRenderer.gameObject.name = "Tree4";
			}
			else if (spriteRenderer.gameObject.name == "Tree4")
			{
				spriteRenderer.color = treeColor6;
				spriteRenderer.gameObject.name = "Tree5";
			}
			else if (spriteRenderer.gameObject.name == "Tree5")
			{
				spriteRenderer.color = treeColor7;
				spriteRenderer.gameObject.name = "Tree6";
			}
			else if (spriteRenderer.gameObject.name == "Tree6")
			{
				PlayerControllerJoystick[] playerControllerJoystick = FindObjectsOfType<PlayerControllerJoystick>();

				for (int i = 0; i < playerControllerJoystick.Length; i++)
				{
					playerControllerJoystick[i].MinusOne();
				}

				Destroy(other.gameObject);
			}
		}
	}
}
