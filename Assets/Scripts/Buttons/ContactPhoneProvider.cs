using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ContactPhoneProvider : MonoBehaviour
{
	[Header("Components")]
	public Text buttonText;

	//Sprite for active button
	public Sprite activeSprite;
	public Sprite deactiveSprite;

	//Gameobject to redirect towards this button
	public Button thisButton;

	[Header("Flags")]
	public bool isActivated;

	void Start()
	{
		isActivated = false;
	}

	public void Toggle()
	{
		// Toggle flag
		isActivated = !isActivated;

		// Set text
		if(isActivated)
		{
			ScenarioHandler.Instance.currentScenario.SetHistoricalAction(Scenario.actions.ContactPhoneProvider);
			Activate();
		}
		else
		{
			Deactivate();
		}
	}

	private void Activate()
	{
		thisButton.image.sprite = activeSprite;
	}

	private void Deactivate()
	{
	}
}
