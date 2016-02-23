using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;

public class RestartButton : MonoBehaviour
{
	public void Restart()
	{
		Destroy(ScenarioHandler.Instance);
		Application.LoadLevel(Application.loadedLevel);
	}
}
