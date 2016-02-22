using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections.Generic;

public class Scenario
{
	public enum actions
	{
		TURN_OFF,
		TURN_ON,
		IN_FARADAY,
		OUT_FARADAY,
		AIRPLANE_MODE_ON,
		AIRPLANE_MODE_OFF,
		SIM_CARD_REMOVED,
		SIM_CARD_DAMAGED,
		SIM_CARD_INSERTED,
		SD_CARD_REMOVED,
		SD_CARD_DAMAGED,
		SD_CARD_INSERTED,
		CHARGING_ON,
		CHARGING_OFF,
		CONTACT_PHONE_PROVIDER,
	}
	public List<actions> History { get; private set; }

	public bool IsBatteryLow { get; private set; }
	public bool IsPhoneLocked { get; private set; }
	public bool IsWifi { get; private set; }
	public string Context { get; private set; }
	public List<Rule> Rules { get; private set; }
	public bool IsOn { get; private set; }

	public Scenario(bool isBatteryLow, bool isPhoneLocked, bool isWifi, string context, List<Rule> rules)
	{
		IsBatteryLow = isBatteryLow;
		IsPhoneLocked = isPhoneLocked;
		IsWifi = isWifi;
		Context = context;
		Rules = rules;
	}

	public void Serialise(string inputData)
	{
		string[] data = inputData.Split("|".ToCharArray());	

		IsBatteryLow = data[0] == "1";
		IsPhoneLocked = data[1] == "1";
		IsWifi = data[2] == "1";
		Context = data[3];
	}

	public void TurnOffPhone()
	{
		if(IsOn)
		{
			History.Add(actions.TURN_OFF);
			IsOn = false;
		}
	}

	public void TurnOnPhone()
	{
		if(!IsOn)
		{
			History.Add(actions.TURN_ON);
			IsOn = true;
			IsPhoneLocked = true;
		}
	}

	public IEnumerable<string> TestAllRules()
	{
		return Rules.Where(x => x.Test(this)).Select(x => x.Description);
	}
}
