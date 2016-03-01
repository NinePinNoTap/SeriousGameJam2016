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
    private static ScenarioHandler _instance;
    public static ScenarioHandler Instance {get;set;}
//    {
//    	get {
//    		if(_instance == null)
//    		{
//    			_instance = this;
//    		}
//	        return _instance;
//    	} 
//    	set { _instance = value; }
//   	}

	void Awake()
	{
		// ensure we are a singleton
		if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;

        // ensure this is run before Start() is for other objects
		currentScenario = ScenarioStore.Data.RandomItem();
		context.text = currentScenario.Context;
   	}


    void Update()
    {
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


