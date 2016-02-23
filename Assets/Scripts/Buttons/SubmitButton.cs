using UnityEngine;
using System.Collections.Generic;

public class SubmitButton : MonoBehaviour
{
	private ScenarioHandler handler;
	void Start()
	{
		handler = GameObject.Find("WorldController").GetComponent<ScenarioHandler>();
	}

	public void Submit()
	{
		IList<string> errors = handler.CheckVictory();

		foreach(string error in errors)
		{
			Debug.Log(error);
		}
	}
}
