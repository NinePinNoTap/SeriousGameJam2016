using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections.Generic;

public static class ScenarioStore
{
	// power low, phone locked,  wifi, Context, rules
	// true, true, true, "A suspect has been arrested for a series of recent attacks. Whilst searching his home you find this phone on a kitchen countertop, alongside a pile of SD cards"
	public static Scenario[] Data = 
	{
		new Scenario(true, true, true, 
				"A suspect has been arrested for a series of recent attacks. Whilst searching his home you find this phone on a kitchen countertop, alongside a pile of SD cards",
				new List<Rule> {
				// is charging
				new Rule( scene => IsAfter(scene, Scenario.actions.CHARGING_ON, Scenario.actions.CHARGING_OFF),
					"Do not remove the SD card or allow it to be damaged"),
				//	Faraday cage
				new Rule( scene => IsAfter(scene, Scenario.actions.IN_FARADAY, Scenario.actions.OUT_FARADAY), 
					"The phone must be left in the Faraday cage."),
				//	Contact phone provider
				new Rule( scene => scene.History.Any(x => x == Scenario.actions.CONTACT_PHONE_PROVIDER), 
					"The phone provider should have been contacted")
				})
	};


//	Consistent loss conditions: 
	private static List<Rule> LossRules = new List<Rule> 
		{
			//•    If the SIM card is removed/damaged.
			new Rule( scene => scene.History.Any(x => x == Scenario.actions.SIM_CARD_REMOVED || x == Scenario.actions.SIM_CARD_DAMAGED), 
					"Do not remove the SIM card or allow it to be damaged"),
			//x    If the SD card is removed/damaged.
			new Rule( scene => scene.History.Any(x => x == Scenario.actions.SD_CARD_REMOVED || x == Scenario.actions.SD_CARD_DAMAGED), 
					"Do not remove the SD card or allow it to be damaged"),
			//•    If Faraday cage not fitted/airplane mode not activated.
				new Rule( scene => IsAfter(scene, Scenario.actions.IN_FARADAY, Scenario.actions.OUT_FARADAY) , 
					"The phone should be left in a Faraday cage or be put into Airplane mode."),
			//x    If the phone is turned off.
			new Rule( scene => scene.History.Any(x => x == Scenario.actions.TURN_OFF), "The phone should never be turned off." ),
			//?    Failure to report correct state of phone.
			new Rule( scene => scene.History.Any(x => x == Scenario.actions.REPORT_PHONE_STATE), "You need to report the state of the phone." ),
		};


	public static IEnumerable<string> CheckLosses(Scenario scene)
	{
		return LossRules.Where( x => x.Test(scene) ).Select( x => x.Description );
	}


	public static bool IsAfter(Scenario scene, Scenario.actions last, Scenario.actions before)
	{
		if(scene.History.Any(x => x == before))
		{
			return scene.History.Any(x => x == last);
		}
		else
		{
			return scene.History.FindLastIndex(x => x == before) < scene.History.FindLastIndex(x => x == last);
		}
	}
} 