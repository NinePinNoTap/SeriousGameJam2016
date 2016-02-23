using System.Linq;
using System.Collections.Generic;

public static class ScenarioStore
{
	// power low, phone locked,  wifi, Context, rules
	// true, true, true, "A suspect has been arrested for a series of recent attacks. Whilst searching his home you find this phone on a kitchen countertop, alongside a pile of SD cards"
	public static readonly Scenario[] Data = 
	{
		new Scenario(true, true, true, 
				"A suspect has been arrested for a series of recent attacks. Whilst searching his home you find this phone on a kitchen countertop, alongside a pile of SD cards",
				new List<Rule> {
				// is charging
				new Rule( scene => IsAfter(scene, Scenario.actions.ChargingOn, Scenario.actions.ChargingOff),
                    "Phone out of battery - Potential data loss from power cycle"),
				//	Faraday cage
				new Rule( scene => IsAfter(scene, Scenario.actions.InFaraday, Scenario.actions.OutFaraday),
                    "No Faraday Cage fitted - Potential data loss from remote access"),
				//	Contact phone provider
				new Rule( scene => scene.History.Contains(Scenario.actions.ContactPhoneProvider), 
					"The phone provider should have been contacted")
				})
	};

    // If these are false, they lose!
	public static readonly List<Rule> LossRules = new List<Rule> 
		{
            new Rule( scene => !scene.History.Contains(Scenario.actions.SimCardRemoved),
                 "Sim Removed - See \"Appendix A - Volatile Data Collection\" of the ACPO Good Practice Guide for Digital Evidence"),
			//•    If the SIM card is removed/damaged.
		     new Rule( scene => !scene.History.Contains(Scenario.actions.SimCardDamaged),
                 "Sim Destroyed - See \"Appendix A - Volatile Data Collection\" of the ACPO Good Practice Guide for Digital Evidence"),
			//x    If the SD card is removed/damaged.
            new Rule( scene => !scene.History.Contains(Scenario.actions.SdCardRemoved),
                 "SD Removed - See \"Appendix A - Volatile Data Collection\" of the ACPO Good Practice Guide for Digital Evidence"),
			//•    If the SIM card is removed/damaged.
		     new Rule( scene => !scene.History.Contains(Scenario.actions.SdCardDamaged),
                 "SD Destroyed - See \"Appendix A - Volatile Data Collection\" of the ACPO Good Practice Guide for Digital Evidence"),
			//•    If Faraday cage not fitted/airplane mode not activated.
			new Rule( scene => IsAfter(scene, Scenario.actions.InFaraday, Scenario.actions.OutFaraday) ,
                    "No Faraday Cage fitted or Aeroplane Mode activated - Potential data loss from remote access"),
			//x    If the phone is turned off.
			new Rule( scene => !(scene.History.Any(x => x == Scenario.actions.TurnOff) || scene.History.Any(x => x == Scenario.actions.TurnOn)), 
                 "Phone Power State Changed - See \"Appendix C - Mobile Phones\" of the ACPO Good Practice Guide for Digital Evidence"),
			//?    Failure to report correct state of phone.
			new Rule( scene => scene.History.Any(x => x == Scenario.actions.ReportPhoneState), 
                 "CSP not contacted - See \"Appendix D - Investigating Different Types of Crime\" of the ACPO Good Practice Guide for Digital Evidence")
		};

//
//"Phone Allowed to Lock - Potential data loss"
//"Phone out of battery - Potential data loss from power cycle"



	public static IEnumerable<string> CheckLosses(Scenario scene)
	{
		return LossRules.Where( x => x.Test(scene) ).Select( x => x.Description );
	}


	public static bool IsAfter(Scenario scene, Scenario.actions last, Scenario.actions before)
	{
		if(scene.History.Contains(before))
		{
			return scene.History.Contains(last);
		}

        return scene.History.FindLastIndex(x => x == before) < scene.History.FindLastIndex(x => x == last);
	}
} 