using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberFormatter : MonoBehaviour
{
	//ExampleResult of using this method. Input 123456789123 (123 Billion) - Result: 123.45B

	//We make an enum that contains our suffixes.
	//These can be changed or more suffixes can be added.
	public enum suffixes
	{
		p, // p is a placeholder if the value is under 1 thousand
		K, // Thousand
		M, // Million
		B, // Billion
		T, // Trillion
		Q, //Quadrillion
		Qt,//Quintillion
		Sx//Sextillion

	}

	//Formats numbers in Millions, Billions, etc.
	public string numberFormat(long money)
	{
		int decimals = 2; //How many decimals to round to
		string r = money.ToString(); //Get a default return value

		foreach (suffixes suffix in Enum.GetValues(typeof(suffixes))) //For each value in the suffixes enum
		{
			var currentVal = 1 * Math.Pow(10, (int)suffix * 3); //Assign the amount of digits to the base 10
			var suff = Enum.GetName(typeof(suffixes), (int)suffix); //Get the suffix value
			if ((int)suffix == 0) //If the suffix is the p placeholder
				suff = String.Empty; //set it to an empty string

			if (money >= currentVal)
				r = Math.Round((money / currentVal), decimals, MidpointRounding.ToEven).ToString() + suff; //Set the return value to a rounded value with suffix
			else
				return r; //If the value wont go anymore then return
		}
		return r; // Default Return
	}
}
