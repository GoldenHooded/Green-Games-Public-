using UnityEngine;
using UnityEngine.UI;

public enum JoystickSide
{
	UpLeft,
	DownLeft,
	DownRight,
	UpRight
}

public class MultiplayerJoystick : MonoBehaviour
{
	public Image joystickBackground; // Imagen de fondo del joystick
	public Image joystickHandle; // Imagen del joystick

	public JoystickSide side; // Lado del joystick

	[SerializeField] private Vector2 offset;

	[SerializeField] private Vector2 initialPosition; // Posición inicial del joystick
	public Vector2 inputVector; // Vector de entrada para el movimiento del joystick

	private void Start()
	{
		initialPosition = joystickBackground.rectTransform.localPosition;
	}

	private void Update()
	{
		// Actualizar el joystick solo si hay toques en la pantalla
		if (Input.touchCount > 0 && StartScreen.Started())
		{
			// Almacena si se encontró un toque en el lado del joystick
			bool foundTouch = false;

			foreach (Touch touch in Input.touches)
			{
				// Verifica si el toque actual está en el lado del joystick
				if (IsTouchOnSide(touch.position))
				{
					foundTouch = true;

					Vector2 touchPosition = touch.position + offset;
					Vector2 position;
					RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBackground.rectTransform, touchPosition, null, out position);
					position.x /= joystickBackground.rectTransform.sizeDelta.x;
					position.y /= joystickBackground.rectTransform.sizeDelta.y;

					inputVector = new Vector2(position.x * 2 + 1, position.y * 2 - 1);
					inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

					// Mueve el joystickHandle
					UpdateJoystickTransform();

					// Solo procesa un toque por frame
					break;
				}
			}

			// Si no se encontró un toque en el lado del joystick, establece el inputVector a cero
			if (!foundTouch)
			{
				inputVector = Vector2.zero;
				UpdateJoystickTransform();
			}
		}
		else
		{
			inputVector = Vector2.zero;
			UpdateJoystickTransform();
		}
	}

	private void UpdateJoystickTransform()
	{
		joystickHandle.rectTransform.anchoredPosition = new Vector2(inputVector.x * (joystickBackground.rectTransform.sizeDelta.x / 3), inputVector.y * (joystickBackground.rectTransform.sizeDelta.y / 3));
	}

	// Verifica si el toque está en el lado del joystick
	private bool IsTouchOnSide(Vector2 touchPosition)
	{
		switch (side)
		{
			case JoystickSide.UpLeft:
				return touchPosition.x < Screen.width / 2 && touchPosition.y > Screen.height / 2;
			case JoystickSide.DownLeft:
				return touchPosition.x < Screen.width / 2 && touchPosition.y < Screen.height / 2;
			case JoystickSide.DownRight:
				return touchPosition.x > Screen.width / 2 && touchPosition.y < Screen.height / 2;
			case JoystickSide.UpRight:
				return touchPosition.x > Screen.width / 2 && touchPosition.y > Screen.height / 2;
			default:
				return false;
		}
	}
}
