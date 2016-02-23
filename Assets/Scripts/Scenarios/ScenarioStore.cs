using System.Linq;
using System.Collections.Generic;

public static class ScenarioStore
{
	// power low, phone locked,  wifi, Context, rules
	// true, true, true, "A suspect has been arrested for a series of recent attacks. Whilst searching his home you find this phone on a kitchen countertop, alongside a pile of SD cards"
	public static readonly Scenario[] Data = 
	{
		new Scenario(false, true, true, 
			"Whilst inspecting a domestic disturbance you notice a large amount of narcotics stored within a suspect’s office space. Alongside this evidence is a phone.",
			new List<Rule> {
				FaradayRule,
				ContactProviderRule
			}),
		new Scenario(true, true, true, 
			"A suspect has been arrested for a series of recent attacks. Whilst searching his home you find this phone on a kitchen countertop, alongside a pile of, presumably spare, SD cards.",
			new List<Rule> {
				ChargingRule,
				FaradayRule,
				ContactProviderRule
			}),
		new Scenario(false, false, true, 
			"You are called to search a suspect’s home for connections with potential terrorist activity. The suspect is found on his home computer, speaking with an unknown accomplice. When you subduing him he reaches into his pocket and retrieves this phone.",
			new List<Rule> {
				FaradayRule,
				KeepUnlockedRule
			}),	
		new Scenario(true, false, true, 
			"Whilst walking your beat you notice a drug exchange taking place between a collection of youths. They notice you approaching and scatter. You pursue the suspect who seemed to be dealing and, in the chase, he starts making a call on his phone. You catch and subdue him, wrestling the phone from his hand in the process.",
			new List<Rule> {
				ChargingRule,
				KeepUnlockedRule
				}),	
		new Scenario(true, false, false, 
			"A call has been received about a series of car alarms all activating within a small area. You are the first responder and see that the cars have clearly been vandalised (smashed windows). There’s no sign of any suspects but this phone is found on the floor next to one of the cars.",
			new List<Rule> {
				ChargingRule,
				KeepUnlockedRule
			}),
		new Scenario(false, false, false, 
			"Whilst completing a raid on the home of a suspected people-trafficker, you find a collection of phones stacked together within a drawer. This is one of them.",
			new List<Rule> {
				KeepUnlockedRule
			}),
		new Scenario(false, true, false, 
			"Whilst walking your beat you notice a woman (a known prostitute) attempting to proposition a pair of young males. As you approach, you notice her toss her phone into a nearby bush. You complete the arrest and collect the phone.",
			new List<Rule> {
				FaradayRule,
				ContactProviderRule
				}),
		new Scenario(true, false, false, 
			"A local child-pornography ring has been tracked back to a home within your beat. You are the first responder on the scene. As you approach you see a suspect standing outside who, upon noticing you, breaks into a run, his phone dropping out of his pocket as he escapes.",
			new List<Rule> {
				ChargingRule,
				KeepUnlockedRule
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
			new Rule( scene => IsAfter(scene, Scenario.actions.InFaraday, Scenario.actions.OutFaraday)  ||
							IsAfter(scene, Scenario.actions.AirplaneModeOn, Scenario.actions.AirplaneModeOff),
                    "No Faraday Cage fitted or Aeroplane Mode activated - Potential data loss from remote access"),
			//x    If the phone is turned off.
			new Rule( scene => !(scene.History.Any(x => x == Scenario.actions.TurnOff) || scene.History.Any(x => x == Scenario.actions.TurnOn)), 
                 "Phone Power State Changed - See \"Appendix C - Mobile Phones\" of the ACPO Good Practice Guide for Digital Evidence"),
			//?    Failure to report correct state of phone.
//			new Rule( scene => scene.History.Any(x => x == Scenario.actions.ReportPhoneState), 
//                 "CSP not contacted - See \"Appendix D - Investigating Different Types of Crime\" of the ACPO Good Practice Guide for Digital Evidence")
		};


//
//"Phone Allowed to Lock - Potential data loss"
//"Phone out of battery - Potential data loss from power cycle"
	private static Rule FaradayRule
	{
		get {
			return new Rule( scene => IsAfter(scene, Scenario.actions.InFaraday, Scenario.actions.OutFaraday),
	                    "No Faraday Cage fitted - Potential data loss from remote access");

		}	 
	}

	private static Rule ContactProviderRule
	{
		get {
			return new Rule( scene => scene.History.Contains(Scenario.actions.ContactPhoneProvider), 
				"CSP not contacted - See \"Appendix D - Investigating Different Types of Crime\" of the ACPO Good Practice Guide for Digital Evidence");

		}	 
	}

	private static Rule ChargingRule
	{
		get {
			return new Rule( scene => IsAfter(scene, Scenario.actions.ChargingOn, Scenario.actions.ChargingOff),
	                    "Phone out of battery - Potential data loss from power cycle");

		}	 
	}

	private static Rule KeepUnlockedRule
	{
		get {
//			return new Rule( scene => false,
//				"Phone Allowed to Lock - Potential data loss");
			return new Rule( scene => true,	"");

		}	 
	}

	public static bool IsAfter(Scenario scene, Scenario.actions last, Scenario.actions before)
	{
		if(!scene.History.Contains(before))
		{
			return scene.History.Contains(last);
		}

        return scene.History.FindLastIndex(x => x == before) < scene.History.FindLastIndex(x => x == last);
	}
} 