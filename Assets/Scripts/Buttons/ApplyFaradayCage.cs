using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ApplyFaradayCage : MonoBehaviour
{
	[Header("Components")]
	public Text buttonText;
	public SwipeToRotate phoneSwipe;
	public GameObject faradayCage;
	public Transform cameraTransform;

	//Sprite for active button
	public Sprite activeSprite;
	public Sprite deactiveSprite;
	
	//Gameobject to redirect towards this button
	public Button thisButton;

	[Header("Flags")]
	public bool isActivated;

	[Header("Button Text")]
	public string textActive = "Take out of cage";
	public string textInactive  = "Put in Faraday Cage";

	void Start()
	{
		isActivated = false;
		buttonText.text = textInactive;
		faradayCage.SetActive(false);
		phoneSwipe.Activate();
	}

	public void Toggle()
	{
		// Toggle flag
		isActivated = !isActivated;

		// Set text
		if(isActivated)
		{
			ScenarioHandler.Instance.currentScenario.SetHistoricalAction(Scenario.actions.InFaraday);
			Activate();
		}
		else
		{
			ScenarioHandler.Instance.currentScenario.SetHistoricalAction(Scenario.actions.OutFaraday);
			Deactivate();
		}
	}

	private void Activate()
	{
		thisButton.image.sprite = activeSprite;
		faradayCage.SetActive(true);
		phoneSwipe.Deactivate();
	}

	private void Deactivate()
	{
		thisButton.image.sprite = deactiveSprite;
		faradayCage.SetActive(false);
		phoneSwipe.Activate();
	}
}
