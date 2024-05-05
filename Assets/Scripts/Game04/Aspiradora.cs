using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aspiradora : MonoBehaviour
{
	[SerializeField] private Transform cloudParent;

	private Rigidbody2D[] childs;

	[SerializeField] private float m;
	[SerializeField] private float b;

	public int points;

	private void Start()
	{
		childs = cloudParent.GetComponentsInChildren<Rigidbody2D>();
	}

	public void Click()
	{
		for (int i = 0; i < childs.Length; i++)
		{
			if (childs[i] != null)
			{
				Rigidbody2D child = childs[i];

				float dist = Vector2.Distance(transform.position, child.position);
				Vector2 dir = transform.position - child.transform.position;

				//child.velocity = child.velocity / 2;
				child.AddForce((Vector3)dir * (m / dist), ForceMode2D.Impulse);

				float oneSize = Mathf.Clamp(dist * b, 0f, 0.13f);

				if (oneSize < 1)
				{
					child.transform.localScale = new Vector3(oneSize, oneSize, oneSize);
				}
			}
		}
	}

	private static float k = 0.13f;
	private void Update()
	{
		for (int i = 0; i < childs.Length; i++)
		{
			if (childs[i] != null)
			{
				Rigidbody2D child = childs[i];


				float dist = Vector2.Distance(transform.position, child.position);

				float oneSize = Mathf.Clamp(dist * b, 0f, k);

				if (oneSize < k)
				{
					oneSize = Mathf.Clamp((dist - k / 2) * (2 * b), 0f, k);
					child.transform.localScale = new Vector3(oneSize, oneSize, oneSize);
				}

				if (Vector3.Distance(child.transform.position, transform.position) < 0.1f)
				{
					Destroy(child.gameObject);
					points++;
				}
			}
		}
	}
}
