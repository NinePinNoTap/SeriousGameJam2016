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
		buttonText.text = textActive;
		faradayCage.SetActive(true);
		phoneSwipe.Deactivate();
	}

	private void Deactivate()
	{
		buttonText.text = textInactive;
		faradayCage.SetActive(false);
		phoneSwipe.Activate();
	}
}
