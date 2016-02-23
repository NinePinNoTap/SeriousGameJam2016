using UnityEngine;
using System.Collections;

public enum MouseButton { MouseLeft = 0, MouseRight = 1 };

public class ClickToInteract : MonoBehaviour
{
	[Header("Components")]
	public Camera mainCamera;
	public InteractionMenuHandler menuHandler;

	[Header("Interaction")]
	public Vector3 mousePos;
	public MouseButton buttonInput;

	void Start ()
	{
		if(!mainCamera)
		{
			mainCamera = Camera.main;
		}

		if(!menuHandler)
		{
			menuHandler = GetComponent<InteractionMenuHandler>();
		}
	}

	void Update ()
	{
		RaycastHit hit;

		// Look for a mouse click
		if(Input.GetMouseButtonDown((int)buttonInput))
		{
			// Get the position of the mouse
			mousePos = Input.mousePosition;

			// Convert the mouse to a raycast
			Ray ray = Camera.main.ScreenPointToRay(mousePos);

			// Raycast and see what we hit
			if (Physics.Raycast(ray, out hit))
			{
				Debug.Log (hit.transform.name);
                menuHandler.HandleClick(hit.transform.gameObject);
			}
			else
			{
				// Send a no click
				menuHandler.HandleClick(null);
			}
		}
	}
}
