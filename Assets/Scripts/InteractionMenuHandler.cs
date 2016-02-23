using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class InteractionMenuHandler : MonoBehaviour
{
	[Header("Components")]
    public GameObject canvas;					// Access to canvas
    public GameObject buttonHandler;
    private Text menuTitle;						// Access to title text
    private Text menuInformation;				// Access to information text

    [Header("Objects")]
    public GameObject menuPrefab;               // Object to instantiate
    public GameObject buttonPrefab;               // Object to instantiate
    private GameObject menuObj;					// Created object
    private GameObject interactedObject;			// Name of the interacted item

    [Header("Menu Properties")]
    public float buttonHeight;                  // How high buttons are
	
    public void HandleClick(GameObject clickedObj)
	{
        // Make sure we have an object to process
        if(!clickedObj)
		{
            // Destroy the menu as we have clicked off
			//DestroySelf();
			return;
		}

        // Make sure the clicked object is something we can interact with
        if(!clickedObj.GetComponent<InteractableComponent>())
        {
            return;
        }

        // Check if we need to remake the menu
        if(interactedObject && !interactedObject.Equals(clickedObj))
		{
            // Attempt to destroy the menu
            DestroySelf();
        }

        // Store the item name
        interactedObject = clickedObj;

		// Check if we have a menu created
		if(menuObj)
		{
			// Do an action
			// E.g. Take off cover, put phone into aeroplane mode
			// Needs some processing
            // MAYBE WONT NEED THIS BIT NOW
		}
		else
		{
			// We don't have a menu showing so create one
			BuildMenu();
		}
	}

	private void BuildMenu()
	{
        // Create the object
        CreateMenuObject();

        // Set up text
        SetUpMenuText();

        // Set up buttons
        SetUpMenuButtons();
    }

    private void CreateMenuObject()
    {
        // Create the menu
        menuObj = GameObject.Instantiate(menuPrefab);
        menuObj.transform.name = "Interaction Menu";
        menuObj.transform.SetParent(canvas.transform);
        menuObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-200, 100);
        menuObj.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
    }

    private void SetUpMenuText()
    {
        // Get access to the menu data
        InteractableComponent menuData = interactedObject.GetComponent<InteractableComponent>();
        menuTitle = menuObj.transform.FindChild("TitleText").GetComponent<Text>();
        menuInformation = menuObj.transform.FindChild("InformationText").GetComponent<Text>();

        // Set the text
        menuTitle.text = menuData.menuTitle;
        menuInformation.text = menuData.menuDescription;
    }

    private void SetUpMenuButtons()
    {
        InteractableComponent menuData = interactedObject.GetComponent<InteractableComponent>();

        // Set up the buttons
        int NoOfButtons = menuData.menuButtons.Count;
        for(int i = 0; i < NoOfButtons; i++)
        {
            // Create a button
            GameObject obj = GameObject.Instantiate(buttonPrefab);

            // Set names
            obj.name = "Menu Button : " + (i+1);
            obj.transform.GetChild(0).GetComponent<Text>().text = menuData.menuButtons[i].buttonText;

            // Set parents
            obj.transform.SetParent(canvas.transform);
            obj.transform.SetParent(menuObj.transform);

            // Set transforms
            obj.transform.localScale = new Vector3(1,1,1);
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 20 + (i*buttonHeight));

            // For some reason delegate needs this
            string functionName = menuData.menuButtons[i].functionName;

            // Set up onClick function
            obj.GetComponent<Button>().onClick.AddListener(delegate()
            { 
                buttonHandler.GetComponent<ButtonHandler>().Call(functionName);
            });
        }
    }

	private void DestroySelf()
	{
		if(!menuObj)
			return;

		Destroy(menuObj);
		menuObj = null;
	}
}