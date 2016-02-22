using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InteractionMenuHandler : MonoBehaviour
{
	[Header("Components")]
	public GameObject canvas;					// Access to canvas
	private Text menuTitle;						// Access to title text
	private Text menuInformation;				// Access to information text

	[Header("Objects")]
	public GameObject menuPrefab;				// Object to instantiate
	public GameObject menuObj;					// Created object

	[Header("Properties")]
	private string interactedComponentName = "";					// Name of the interacted item
	
	public void HandleClick(string name)
	{
		if(name.Equals(""))
		{
			DestroySelf();
			return;
		}

		// Check if the names differ
		if(!interactedComponentName.Equals(name))
		{
			// Attempt to destroy the menu
			DestroySelf();
			
			// Store the item name
			interactedComponentName = name;
		}

		// Check if we have a menu created
		if(menuObj)
		{
			// Do an action
			// E.g. Take off cover, put phone into aeroplane mode
			// Needs some processing
		}
		else
		{
			// We don't have a menu showing so create one
			BuildMenu();
		}
	}

	public void Interact()
	{
		Debug.Log ("BUTTON CLICK");
	}

	private void BuildMenu()
	{
		// Create the menu
		menuObj = GameObject.Instantiate(menuPrefab);
		menuObj.transform.name = "Interaction Menu";

		// Parent under the canvas
		menuObj.transform.SetParent(canvas.transform);

		menuObj.GetComponent<RectTransform>().position = canvas.GetComponent<RectTransform>().transform.position + new Vector3(256, 0.0f, 0.0f);
		menuTitle = menuObj.transform.FindChild("TitleText").GetComponent<Text>();
		menuInformation = menuObj.transform.FindChild("InformationText").GetComponent<Text>();

		// NEED A HANDLER FOR UPDATING THE TEXT
		// BUT FOR NOW
		menuTitle.text = interactedComponentName;
		menuInformation.text = "You clicked on this component. Interact?";
	}

	private void DestroySelf()
	{
		if(!menuObj)
			return;

		Destroy(menuObj);
		menuObj = null;
	}
}
