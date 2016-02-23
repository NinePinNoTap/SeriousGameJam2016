using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;

public class SubmitButton : MonoBehaviour
{
	[SerializeField] Text endText;
	[SerializeField] GameObject endPanel;

	private ScenarioHandler handler;
	void Start()
	{
		handler = GameObject.Find("InteractionController").GetComponent<ScenarioHandler>();
	}

	public void Submit()
	{
		IList<string> errors = handler.CheckVictory();

		endPanel.SetActive(true);
		if(errors.Any())
		{
			endText.text = String.Join("\n\n", errors.ToArray());
		}
		else // they were successful
		{
			endText.text = "Congratulations - You have completed Module 1 of ACPO TRAINER!";
		}
	}
}
