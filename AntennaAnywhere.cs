using UnityEngine;
using HarmonyLib;

public class AntennaAnywhere : Mod{
	public static string full_name = "Antenna Anywhere";
	public static string sh = "[AA] ";

	private Harmony harmony;

	public void Start(){
		harmony = new Harmony("com.kevosoftworks.AntennaAnywhere");
		harmony.PatchAll();

		Debug.Log(sh + full_name + " loaded successfully!");
	}

	public void OnModUnload(){
		harmony.UnpatchAll(harmony.Id);

		Debug.Log(sh + full_name + " unloaded successfully!");
	}
}

[HarmonyPatch(typeof(Reciever_Antenna), "HasValidConnection", MethodType.Getter)]
public class Patch_ValidConnection{
	static bool Prefix(ref Reciever_Antenna __instance){
		// Set restriction booleans to unrestrictive values
		__instance.toCloseToOtherAntenna = false;
		__instance.toCloseToReciever = false;
		__instance.toFarAwayFromReciever = false;
		__instance.onSameAltitudeAsReciever = true;

		// Execute the original getter, in case it is modified further and to check other attributes
		return true;
	}
}