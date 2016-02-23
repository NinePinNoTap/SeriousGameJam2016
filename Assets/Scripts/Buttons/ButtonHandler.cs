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

        Debug.Log("Found : " + objs.Length);

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
        TrySetActive("phoneBack", false);
        LeaveAlone();
    }

    public void AttachBatteryCase()
    {
        TrySetActive("Battery Cover", true);
        TrySetActive("phoneBack", true);
        LeaveAlone();
    }

    //==============================================
    // SIM CARD
    //==============================================

    public void RemoveSim()
    {
        TrySetActive("simCard", false);
        LeaveAlone();
    }

    public void DestroySim()
    {
        TryDestroy("simCard");
        LeaveAlone();
    }

    //==============================================
    // SD CARD
    //==============================================

    public void RemoveSD()
    {
        TrySetActive("memoryCard", false);
        LeaveAlone();
    }

    public void DestroySD()
    {
        TryDestroy("memoryCard");
        LeaveAlone();
    }

    //==============================================
    // BATTERY PACK
    //==============================================

    public void RemoveBattery()
    {
        TrySetActive("batteryCard", false);
        LeaveAlone();
    }

    public void DestroyBattery()
    {
        TryDestroy("batteryCard");
        LeaveAlone();
    }

    //==============================================
    // POWER BUTTON
    //==============================================

    public void TurnOnPhone()
    {
        phoneController.TurnPhoneOn();
    }

    public void TurnOffPhone()
    {
        phoneController.TurnPhoneOff();
    }

    //==============================================
    // AEROPLANE MODE
    //==============================================

    public void TurnOnAeroplaneMode()
    {
        phoneController.TurnAirplaneModeOn();
    }

    public void TurnOffAeroplaneMode()
    {
        phoneController.TurnAirplaneModeOff();
    }

    //==============================================
    // CHARGE SLOT
    //==============================================

    public void PutPhoneOnCharge()
    {
        phoneController.ChangeBatteryLevel(100);
    }

    public void RemoveChargeFromPhone()
    {
        // What do we want to do here?
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