using UnityEngine;
using System.Collections;

//============================================
// HELPER FUNCTIONS FOR CONTROLLING THE PHONE
//============================================

public class PhoneController : MonoBehaviour
{
	[Header("Phone States")]
	public GameObject canvasPhoneOn;
	public GameObject canvasPhoneOff;

	[Header("Airplane Mode")]
	public ToggleImage imageAirplaneMode;

	[Header("Battery")]
	public BatteryMonitor batteryMonitor;
	public ToggleImage imageLowBattery;
	public bool IsCharging = false;

	[Header("Lockscreen")]
	public ToggleImage lockScreen;

	[Header("Audio Details")]
	public AudioController audioController1;
	public AudioClip PhoneOnClip;
	public AudioClip PhoneOffClip;
	
	public void TurnPhoneOn()
	{
		canvasPhoneOn.SetActive(true);
		canvasPhoneOff.SetActive(false);
		//AudioController.PlaySound(PhoneOnClip);
	}

	public void TurnPhoneOff()
	{
		canvasPhoneOn.SetActive(false);
		canvasPhoneOff.SetActive(true);
		imageLowBattery.Toggle(false);
	}

	public void TurnAirplaneModeOn()
	{
		imageAirplaneMode.Toggle(true);
	}

	public void TurnAirplaneModeOff()
	{
		imageAirplaneMode.Toggle(false);
	}

	public void ChangeBatteryLevel(int value)
	{
		batteryMonitor.SetBatteryLevel(value);
	}

	public void ShowLockScreen()
	{
		lockScreen.Toggle(true);
	}

	public void ShowHomeScreen()
	{
		lockScreen.Toggle(false);
	}

	public void ShowNoBattery()
	{
		TurnPhoneOff();
		imageLowBattery.Toggle(true);
	}
}
