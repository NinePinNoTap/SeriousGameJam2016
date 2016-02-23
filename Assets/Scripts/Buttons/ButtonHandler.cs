using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * CHANCES ARE THIS CLASS IS A REALLY BAD IMPLEMENTATION
 * 
 */

public class ButtonHandler : MonoBehaviour
{
    public string componentTag = "PhoneComponent";
    public PhoneController phoneController;

    private Dictionary<string, GameObject> objDictionary;

    void Start()
    {
        // Not the best but add all game objects into a dictionary
        GameObject[] objs = GameObject.FindGameObjectsWithTag(componentTag);

//        Debug.Log("Found : " + objs.Length);

        objDictionary = new Dictionary<string, GameObject>();
        foreach(GameObject obj in objs)
        {
            objDictionary.Add(obj.name, obj);
        }
    }

    // Calls a function
    public void Call(string functionName)
    {
        Invoke(functionName, 0.0f);
    }

    public void LeaveAlone()
    {
        // Delete the menu if its there
        GameObject obj = GameObject.Find("Interaction Menu");
        if(obj)
        {
            Destroy(obj);
        }
    }

    //==============================================
    // PHONE BACK
    //==============================================

    public void RemoveBatteryCase()
    {
		if(TryIsActive("phoneBack") == 1)
		{
			ScenarioHandler.Instance.currentScenario.SetHistoricalAction(Scenario.actions.BackRemoved);
		}
        TrySetActive("phoneBack", false);
        LeaveAlone();
    }

    public void AttachBatteryCase()
    {
		if(TryIsActive("phoneBack") == 0)
		{
			ScenarioHandler.Instance.currentScenario.SetHistoricalAction(Scenario.actions.BackPutOn);
		}
        TrySetActive("Battery Cover", true);
        TrySetActive("phoneBack", true);
        LeaveAlone();
    }

    //==============================================
    // SIM CARD
    //==============================================

    public void RemoveSim()
    {
		if(TryIsActive("simCard") == 1)
		{
			ScenarioHandler.Instance.currentScenario.SetHistoricalAction(Scenario.actions.SimCardRemoved);
		}
        TrySetActive("simCard", false);
        LeaveAlone();
    }

    public void DestroySim()
    {
		ScenarioHandler.Instance.currentScenario.SetHistoricalAction(Scenario.actions.SimCardDamaged);
        TryDestroy("simCard");
        LeaveAlone();
    }

    //==============================================
    // SD CARD
    //==============================================

    public void RemoveSD()
    {
		if(TryIsActive("memoryCard") == 1)
		{
			ScenarioHandler.Instance.currentScenario.SetHistoricalAction(Scenario.actions.SdCardRemoved);
		}        
		TrySetActive("memoryCard", false);
        LeaveAlone();
    }

    public void DestroySD()
    {
		ScenarioHandler.Instance.currentScenario.SetHistoricalAction(Scenario.actions.SdCardDamaged);
        TryDestroy("memoryCard");
        LeaveAlone();
    }

    //==============================================
    // BATTERY PACK
    //==============================================

    public void RemoveBattery()
    {
        TrySetActive("batteryCard", false);
		TurnOffPhone();
        LeaveAlone();
    }

    public void DestroyBattery()
    {
        TryDestroy("batteryCard");
		TurnOffPhone();
        LeaveAlone();
    }

    //==============================================
    // POWER BUTTON
    //==============================================

    public void TurnOnPhone()
    {
		if(phoneController.canvasPhoneOff.activeInHierarchy)
		{
			ScenarioHandler.Instance.currentScenario.SetHistoricalAction(Scenario.actions.TurnOn);
		}
        phoneController.TurnPhoneOn();
    }

    public void TurnOffPhone()
    {
		if(phoneController.canvasPhoneOn.activeInHierarchy)
		{
			ScenarioHandler.Instance.currentScenario.SetHistoricalAction(Scenario.actions.TurnOff);
		}
        phoneController.TurnPhoneOff();
    }

    //==============================================
    // AEROPLANE MODE
    //==============================================

    public void TurnOnAeroplaneMode()
    {
		if(!phoneController.imageAirplaneMode.isToggled)
		{
			ScenarioHandler.Instance.currentScenario.SetHistoricalAction(Scenario.actions.AirplaneModeOn);
		}
        phoneController.TurnAirplaneModeOn();
    }

    public void TurnOffAeroplaneMode()
    {
		if(phoneController.imageAirplaneMode.isToggled)
		{
			ScenarioHandler.Instance.currentScenario.SetHistoricalAction(Scenario.actions.AirplaneModeOff);
		}

        phoneController.TurnAirplaneModeOff();
    }

    //==============================================
    // CHARGE SLOT
    //==============================================

    public void PutPhoneOnCharge()
    {
		if(!phoneController.IsCharging)
		{
			phoneController.IsCharging = true;
			ScenarioHandler.Instance.currentScenario.SetHistoricalAction(Scenario.actions.ChargingOn);
		}

        phoneController.ChangeBatteryLevel(100);
    }

    public void RemoveChargeFromPhone()
    {
		if(phoneController.IsCharging)
		{
			phoneController.IsCharging = false;
			ScenarioHandler.Instance.currentScenario.SetHistoricalAction(Scenario.actions.ChargingOff);
		}
    }

    //==============================================
    // FUNCTIONALITY
    //==============================================

    public void TrySetActive(string name, bool flag)
    {
        if(objDictionary.ContainsKey(name))
        {
            GameObject obj;
            objDictionary.TryGetValue(name, out obj);
            obj.SetActive(flag);
        }
    }

    public int TryIsActive(string name)
    {
		if(objDictionary.ContainsKey(name))
        {
            GameObject obj;
            objDictionary.TryGetValue(name, out obj);
			return obj.activeSelf ? 1 : 0;
        }    
        return -1;
    }

    public void TryDestroy(string name)
    {
        if(objDictionary.ContainsKey(name))
        {
            GameObject obj;
            objDictionary.TryGetValue(name, out obj);
            Destroy(obj);
        }
    }
}