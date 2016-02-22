using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Scenario
{
	public bool IsBatteryLow { get; private set; }
	public bool IsPhoneLocked { get; private set; }
	public bool IsWifi { get; private set; }
	public string Context { get; private set; }

	public Scenario(string inputData)
	{
		Serialise(inputData);
	}

	// power low | phone locked | wifi | Context
	// "1|1|1|A suspect has been arrested for a series of recent attacks. Whilst searching his home you find this phone on a kitchen countertop, alongside a pile of SD cards"
	public void Serialise(string inputData)
	{
		string[] data = inputData.Split("|".ToCharArray());	

		IsBatteryLow = data[0] == "1";
		IsPhoneLocked = data[1] == "1";
		IsWifi = data[2] == "1";
		Context = data[3];
	}
}


