using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
	public float percent = 0;
	
	[HideInInspector] public string sceneName;
	
	[SerializeField] private Image backgroundImg;
	[SerializeField] private Image barImg;
	[SerializeField] private TMPro.TMP_Text percentTxt;
	[SerializeField] private GameObject canvasNotToDest;
	[SerializeField] private Animator logoAnim;
	
	[SerializeField] private RectTransform barRectTransform;
	private float iBarSizeDeltaX;
	private float iBarAnchoredPosX;
	[SerializeField] private RectTransform fBarRectTransform;
	private float fBarSizeDeltaX;
	private float fBarAnchoredPosX;
	
	private void Start() 
	{
		iBarSizeDeltaX = barRectTransform.sizeDelta.x;
		iBarAnchoredPosX = barRectTransform.anchoredPosition.x;
		
		fBarSizeDeltaX = fBarRectTransform.sizeDelta.x;
		fBarAnchoredPosX = 0;
	}
	
	private void Update() 
	{
		//relación lineal f(x) = m * x + n
		float BarSizeDeltaX = (fBarSizeDeltaX - iBarSizeDeltaX) / 100 * percent + iBarSizeDeltaX;
		float BarSizeDeltaY = barRectTransform.sizeDelta.y;
		barRectTransform.sizeDelta = new Vector2(BarSizeDeltaX, BarSizeDeltaY);
		
		float BarAnchoredPosX = (fBarAnchoredPosX - iBarAnchoredPosX) / 100 * percent + iBarAnchoredPosX;
		float BarAnchoredPosY = barRectTransform.anchoredPosition.y;
		barRectTransform.anchoredPosition = new Vector2(BarAnchoredPosX, BarAnchoredPosY);
		
		percentTxt.text = ((int)percent).ToString() + "%";
		
		backgroundImg.color = new Color(backgroundImg.color.r, backgroundImg.color.g, backgroundImg.color.b, barImg.color.a);
	}
	
	public void LoadScene()
	{
		FindObjectOfType<LoadManager>().asyncLoadMenuScene.allowSceneActivation = true;
		logoAnim.SetTrigger("End");
		DontDestroyOnLoad(canvasNotToDest);
	}
	
	public void End()
	{
		Destroy(canvasNotToDest);
	}
}
