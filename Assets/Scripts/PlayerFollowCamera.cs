using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowCamera : MonoBehaviour
{
	[SerializeField] private Transform[] players;
	[SerializeField] private float speed = 5f;
	[SerializeField] private float margin = 3f;
	[SerializeField] private float maxMargin = 3f;
	[SerializeField] private float minMargin = 10f;

	void Update()
	{
		List<Vector2> playersList = new List<Vector2>();

		for (int i = 0; i < players.Length; i++)
		{
			playersList.Add(players[i].position);
		}

		Vector2 midPoint;
		CalculateMidPoint(playersList, out midPoint);

		// Convertir el punto medio al espacio de la pantalla de la cámara
		Vector3 midpointViewportPoint = Camera.main.WorldToViewportPoint(midPoint);

		MaxDistantPoints(playersList, out float maxDistance);
		
		// Calcular el tamaño del rectángulo que rodea los puntos, agregando el margen
		float cameraSize = Mathf.Max(
			Mathf.Abs(midpointViewportPoint.x - 0.5f) * 2 * (Screen.width / Screen.height),
			Mathf.Abs(midpointViewportPoint.y - 0.5f) * 2) + Mathf.Clamp(margin * maxDistance, minMargin, maxMargin);

		// Ajustar el tamaño de la cámara
		Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, cameraSize, speed * Time.deltaTime);

		// Calcular la posición de la cámara
		Vector3 targetPos = (Vector3)midPoint + Vector3.forward * -10;
		transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);
	}

	void CalculateMidPoint(List<Vector2> points, out Vector2 midPoint)
	{
		if (points.Count == 0)
		{
			Debug.LogError("Se necesita al menos un punto para calcular el punto medio.");
			midPoint = Vector2.zero;
			return;
		}

		Vector2 sum = Vector2.zero;
		foreach (Vector2 point in points)
		{
			sum += point;
		}

		midPoint = sum / points.Count;
	}
	
	public void MaxDistantPoints(List<Vector2> points, out Vector2 pointA, out Vector2 pointB)
	{
		if (points == null || points.Count < 2)
		{
			Debug.LogError("Se requieren al menos dos puntos para calcular la distancia máxima.");
			pointA = Vector2.zero;
			pointB = Vector2.zero;
			return;
		}

		float maxDistance = 0f;
		pointA = Vector2.zero;
		pointB = Vector2.zero;

		// Comparar cada par de puntos para encontrar la máxima distancia
		for (int i = 0; i < points.Count; i++)
		{
			for (int j = i + 1; j < points.Count; j++)
			{
				float distance = Vector2.Distance(points[i], points[j]);
				if (distance > maxDistance)
				{
					maxDistance = distance;
					pointA = points[i];
					pointB = points[j];
				}
			}
		}
	}
	
	public void MaxDistantPoints(List<Vector2> points, out float maxDistance)
	{
		Vector2 pointA;
		Vector2 pointB;
		maxDistance = 0f;
		
		if (points == null || points.Count < 2)
		{
			Debug.LogError("Se requieren al menos dos puntos para calcular la distancia máxima.");
			pointA = Vector2.zero;
			pointB = Vector2.zero;
			return;
		}
		
		pointA = Vector2.zero;
		pointB = Vector2.zero;

		// Comparar cada par de puntos para encontrar la máxima distancia
		for (int i = 0; i < points.Count; i++)
		{
			for (int j = i + 1; j < points.Count; j++)
			{
				float distance = Vector2.Distance(points[i], points[j]);
				if (distance > maxDistance)
				{
					maxDistance = distance;
					pointA = points[i];
					pointB = points[j];
				}
			}
		}
	}
}