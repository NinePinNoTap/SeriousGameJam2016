using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MinMaxWindow : MonoBehaviour
{
	[Header("Components")]
	public Text textToHide;							// Text to be hidden when minimised
	public Image backgroundImage;					// Image for the background

	[Header("Properties")]
	public bool isShowing = true;					// Flag for whether to show or hide window
	public string textStorage = "";					// Storage for the text when hidden
	public Sprite minimisedSprite;					// Sprite for when minimised
	public Sprite maximisedSprite;

	private Vector2 minimisedSize;
	private Vector2 maximisedSize;

	// Use this for initialization
	void Start ()
	{
		// Store current size;
		maximisedSize = backgroundImage.rectTransform.sizeDelta;

		// Calculate minimised size
		minimisedSize = new Vector2(73,75);

		// Store the current text
		textStorage = textToHide.text;

		MaximiseWindow();
	}

	void Update()
	{
	}

	public void Toggle()
	{
		// Check new state
		if(isShowing)
		{
			MinimiseWindow();
		}
		else
		{
			MaximiseWindow();
		}
	}
	
	private void MinimiseWindow()
	{	
		// Disable flag
		isShowing = false;

		// Reset text
		textToHide.text = " ";

		// Set sprites
		backgroundImage.sprite = minimisedSprite;

		// Update sizes
		backgroundImage.rectTransform.sizeDelta = minimisedSize;

		// Change image type
		backgroundImage.type = Image.Type.Simple;
	}
	
	private void MaximiseWindow()
	{
		// Enable flag
		isShowing = true;

		// Reset text
		textToHide.text = textStorage;

		// Set sprites
		backgroundImage.sprite = maximisedSprite;

		// Update sizes
		backgroundImage.rectTransform.sizeDelta = maximisedSize;

		// Change image type
		backgroundImage.type = Image.Type.Sliced;
	}
}
