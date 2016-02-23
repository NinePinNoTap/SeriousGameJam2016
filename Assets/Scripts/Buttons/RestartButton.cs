using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;

public class RestartButton : MonoBehaviour
{
	public void Restart()
	{
		Debug.Log("1111");
		Application.LoadLevel(Application.loadedLevel);
	}
}
