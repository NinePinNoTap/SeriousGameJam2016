using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ContactPhoneProvider : MonoBehaviour
{
	[Header("Components")]
	public Text buttonText;

	[Header("Flags")]
	public bool isActivated;

	void Start()
	{
		isActivated = false;
		buttonText.text = "Contact CSP";
	}

	public void Toggle()
	{
		// Toggle flag
		isActivated = !isActivated;

		// Set text
		if(isActivated)
		{
			Activate();
		}
		else
		{
			Deactivate();
		}
	}

	private void Activate()
	{
	}

	private void Deactivate()
	{
	}
}
