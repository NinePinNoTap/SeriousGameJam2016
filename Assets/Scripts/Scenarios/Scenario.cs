using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections.Generic;

public class Scenario
{
	public enum actions
	{
		TurnOff = 1,
		TurnOn,
		InFaraday,
		OutFaraday,
		AirplaneModeOn,
	    AirplaneModeOff,
	    SimCardRemoved,
	    SimCardDamaged,
	    SimCardInserted,
	    SdCardRemoved,
	    SdCardDamaged,
	    SdCardInserted,
	    ChargingOn,
	    ChargingOff,
	    ContactPhoneProvider,
	    ReportPhoneState,
	    BackRemoved,
	    BackPutOn,
	    BatteryRemoved,
	    BatteryPutIn
	};

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

		History = new List<actions>();
	}

	public void TurnOffPhone()
	{
		if(IsOn)
		{
			History.Add(actions.TurnOff);
			IsOn = false;
		}
	}

	public void TurnOnPhone()
	{
		if(!IsOn)
		{
			History.Add(actions.TurnOn);
			IsOn = true;
			IsPhoneLocked = true;
		}
	}

	public IEnumerable<string> TestAllRules()
	{
		return Rules.Where(x => !x.Test(this)).Select(x => x.Description);
	}

    public IEnumerable<string> CheckLosses()
    {
        return ScenarioStore.LossRules.Where(x => !x.Test(this)).Select(x => x.Description);
    }


    public void SetHistoricalAction(Scenario.actions action)
	{
		History.Add(action);
	}
}
