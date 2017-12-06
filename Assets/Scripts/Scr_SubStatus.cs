using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_SubStatus : MonoBehaviour {
	public string[] TagList;
	public enum ModType {Null, Base, Extension, Barrel, Handle, Magazine, Ammunition, Shield, Battery};

	[Header("Connection Parts")]
	public ModType vPartType = ModType.Null;
	public ModType vRequiredPart = ModType.Null;

	public string CorrectionPartType = "";
}
