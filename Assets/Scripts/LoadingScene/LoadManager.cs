using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
	[SerializeField] private Animator loadingBarAnim;
	[HideInInspector] public AsyncOperation asyncLoadMenuScene;
	public string sceneName = "MenuScene";
	float startTime;
	float percentLoaded;
	
	private void Start()
	{
		Application.targetFrameRate = 60;
		Invoke("SecStart", 0.01f);
	}
	
	private void SecStart()
	{
		StartCoroutine(LoadScene()); //Empezamos una corrutina
	}
	
	public IEnumerator LoadScene()
	{
		Debug.Log("Background load started");
		startTime = Time.time;
		
		//cargar la escena y dejarla en espera "LoadSceneMode.Additive"
		asyncLoadMenuScene = SceneManager.LoadSceneAsync(sceneName); 
		asyncLoadMenuScene.allowSceneActivation = false; //No le permitimos que se cargue instantaneamente
		while (!asyncLoadMenuScene.isDone)
		{
			percentLoaded = asyncLoadMenuScene.progress * 100 / 0.9f;
			yield return null;
		}
	}
	
	bool done = false;
	private void Update() 
	{
		//Si el progreso de carga es mayor que 100, OnLoadScene()
		//No se hace en el start porque este proceso tarda y no se ejecuta en el mismo frame
		if (!done && percentLoaded >= 100)
		{
			OnLoadComplete();
			done = true;
		}
	}
	
	private void OnLoadComplete() //necesario "(AsyncOperation asyncOperation)"
	{
		loadingBarAnim.SetTrigger("Complete");
		Debug.Log("Background load completed in: " + (Time.time - startTime).ToString());
		FindObjectOfType<LoadingBar>().sceneName = sceneName;
	}
}
