// <summary>
/// Handles the display of the scenarios
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.Linq;

public class ScenarioHandler : MonoBehaviour 
{
    public Scenario currentScenario;
    [SerializeField] Text context;

    // Use this for initialization
    void Start () 
    {
		currentScenario = ScenarioStore.Data.RandomItem();
    }

    void Update()
    {
    	context.text = currentScenario.Context;
    }
    
    public IList<string> CheckVictory()
    {
		IList<string> errors = currentScenario.TestAllRules().ToList();
		foreach(var error in currentScenario.CheckLosses())
		{
			errors.Add(error);
		}
    	return errors;
    }


}

//Extension class to pick a random string from an array
public static class ArrayExtensions
{
    // This is an extension method. RandomItem() will now exist on all arrays.
    public static T RandomItem<T>(this T[] array)
    {
        return array[UnityEngine.Random.Range(0, array.Length)];
    }
}


