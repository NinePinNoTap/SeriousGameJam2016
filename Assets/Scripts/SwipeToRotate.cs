using UnityEngine;
using System.Collections;

public class SwipeToRotate : MonoBehaviour
{
	public float rotationSpeed = 10.0f;			// How fast to spin
	public float lerpSpeed = 1.0f;				// How fast to reduce speed
	
	private Vector3 currentSwipe;	
	private Vector3 averageSwipeSpeed;
	private bool isSwiping;
	private Vector3 targetcurrentSwipeX;

	void Update () 
	{
		// Check for mouse input 
		if (Input.GetMouseButton(0) && isSwiping)
		{
			// Get the current mouse position (inverted in X)
			currentSwipe = new Vector3(-Input.GetAxis ("Mouse X"), Input.GetAxis("Mouse Y"), 0);

			// Calculate the average
			averageSwipeSpeed = Vector3.Lerp(averageSwipeSpeed,currentSwipe,Time.deltaTime * 5);
		}
		else
		{
			// Check for swiping
			if (isSwiping)
			{
				// Update mouse position
				currentSwipe = averageSwipeSpeed;
				isSwiping = false;
			}

			// Reduce the speed of the swipe
			currentSwipe = Vector3.Lerp( currentSwipe, Vector3.zero, Time.deltaTime * lerpSpeed);   
		}

		// Rotate the object using camera transform vectors 
		transform.Rotate( Camera.main.transform.up * currentSwipe.x * rotationSpeed, Space.World );
		transform.Rotate( Camera.main.transform.right * currentSwipe.y * rotationSpeed, Space.World );
	}

	void OnMouseDown() 
	{
		// Flag we are swiping
		isSwiping = true;
	}

	void OnMouseUp()
	{
		// Flag we aren't swiping
		isSwiping = false;
	}
}
