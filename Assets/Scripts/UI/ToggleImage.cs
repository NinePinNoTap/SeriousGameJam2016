using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToggleImage : MonoBehaviour
{
	private Image objImage;
	public Sprite mainSprite;
	public Sprite altSprite;

	public bool isToggled;

	void Start()
	{
		objImage = GetComponent<Image>();
	}

	void Update ()
	{
		if(isToggled)
		{
			objImage.sprite = altSprite;
		}
		else
		{
			objImage.sprite = mainSprite;
		}
	}

	public void Toggle(bool flag)
	{
		isToggled = flag;
		if(isToggled)
		{
			objImage.sprite = altSprite;
		}
		else
		{
			objImage.sprite = mainSprite;
		}
	}
}
