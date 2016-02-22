/// <summary>
/// Handles the display of the scenarios
/// </summary>
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScenarioHandlerScript : MonoBehaviour
{
	
	//Text gameobject fields
	public Text suspectScenario;
	
	//String involved with the suspects scenario
	string suspectString;
	
	//Bool variables for phone
	bool phonePower;
	bool phoneLock;
	bool phoneInternet;

	void Start ()
	{
		//Assemble lists, very basic could be better
		string[] suspectStringCollection = { "Murder with Arrest", "Missing Child", "Hacking Suspicion" };
		
		//Set to a random item
		suspectString = suspectStringCollection.RandomItem();
		
		//Set textbox to equal string
		suspectScenario.text = suspectString;
	}

	void Update ()
	{
		//FSM
		if (suspectString == "Murder with Arrest")
		{
			//Doaction();
		}
		else if (suspectString == "Missing Child")
		{
			//Doaction();
		}
		else if (suspectString == "Hacking Suspicion")
		{
			//Doaction();
		}
		
	}
}

//Extension class to pick a random string from an array
public static class ArrayExtensions
{
	// This is an extension method. RandomItem() will now exist on all arrays.
	public static T RandomItem<T>(this T[] array)
	{
		return array[Random.Range(0, array.Length)];
	}
}