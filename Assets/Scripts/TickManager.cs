using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TickManager : MonoBehaviour
{
	public float timeBetweenTick = 1.0f;

	[SerializeField] private Animator[] buttonAnims;
	private int index;
	
	void Start()
	{
		// Iniciar la corrutina cuando comience el juego
		StartCoroutine(TickCoroutine());
	}

	IEnumerator TickCoroutine()
	{
		while (true)
		{
			buttonAnims[index].SetTrigger("Tick");
			
			index++;
			
			if (index >= buttonAnims.Length)
			{
				index = 0;
			}
			
			Invoke("ResetTriggers", 0.1f);
			
			// Esperar el tiempo definido por timeBetweenTick
			yield return new WaitForSeconds(timeBetweenTick);
		}
	}
	
	private void ResetTriggers()
	{
		for (int i = 0; i < buttonAnims.Length; i++)
		{
			buttonAnims[i].ResetTrigger("Tick");
		}
	}
}
