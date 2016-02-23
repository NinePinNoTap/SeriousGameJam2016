using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BatteryMonitor : MonoBehaviour
{
	[Header("Components")]
	public RectTransform rectTransform;
	public Text batteryText;

	[Header("Battery Levels")]
	public int curBattery = 50;
	public float minBattery = 0;
	public float maxBattery = 100;

	[Header("Calculations")]
	public float width = 0;
	public float height = 0;
	public float maxWidth = 0;
	public float minWidth = 0;

	// Use this for initialization
	void Start ()
	{
		// Access component
		rectTransform.GetComponent<RectTransform>();

		// Get starting height
		height = rectTransform.sizeDelta.y;

		// Get max width
		maxWidth = rectTransform.sizeDelta.x;
	}
	
	// Update is called once per frame
	void Update ()
	{
		HandleBatteryBar();

		HandleBatteryLevel();
	}

	public void SetBatteryLevel(int level)
	{
		curBattery = level;
	}

	private void HandleBatteryBar()
	{
		// Calculate width based on value
		float range = maxWidth - minWidth;
		float percent = (curBattery - minWidth) / range;
		
		// Calculate width on battery image
		width = Mathf.Lerp(minWidth, maxWidth, percent);
		
		// Apply
		rectTransform.sizeDelta = new Vector2(width, height);
	}

	private void HandleBatteryLevel()
	{
		// Clamp battery
        curBattery = (int)Mathf.Clamp(curBattery, minBattery, maxBattery);

		// Update text
		batteryText.text = curBattery.ToString() + "%";

		if(curBattery < 20)
		{
			batteryText.color = Color.red;
		}
		else
		{
			batteryText.color = Color.white;
		}
	}
}

