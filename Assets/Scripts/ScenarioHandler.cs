// <summary>
/// Handles the display of the scenarios
/// </summary>
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ScenarioHandler : MonoBehaviour {

    //Text gameobject fields
    public Text suspectScenario;

    public Scenario currentScenario;

    // Use this for initialization
    void Start () {
//        string[] suspectStringCollection = { "Murder with Arrest", "Missing Child", "Hacking Suspicion" };
//        suspectString = suspectStringCollection.RandomItem();
//        suspectScenario.text = suspectString;
		currentScenario = ScenarioStore.Data.RandomItem();
    }
    
    // Update is called once per frame
    void Update () 
    {
 
    
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


