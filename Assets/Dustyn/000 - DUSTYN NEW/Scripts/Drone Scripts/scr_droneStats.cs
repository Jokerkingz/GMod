using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_droneStats : MonoBehaviour {
[Header("DRONE STATS - EASY TWEAKING")]
[Header("Entering Options")]
//THE MINIMUM DISTANCE IT CAN BE FROM THE PLAYER
public float minDistancefromPlayer;
//THE MAXIMUM DISTANCE IT CAN BE FROM THE PLAYER
public float maxDistancefromPlayer;
//HOW FAST IT ENTERS AND MOVES TOWARDS THE PLAYER
public float entrySpeed;

[Header("Hover In Place Options")]
//IF CHECKED: MOVEMENT IS MORE PREDICTABLE AND MOVES LEFT TO RIGHT
public bool simplifiedHover;
//HOW FAST THE DRONE RETURNS TO IT'S ANCHOR
public float resetSpeed;
//HOW FAST IT SWITCHES TO IT'S HOVER POINTS
public float hoverSwitchSpeed;
//HOW FAST THE ANCHOR ROTATES
public float hoverRotationalSpeed;
//THE MINIMUM TIME IT TAKES TO REVERSE THE ROTATION DIRECTION
public float minReverseTime;
// THE MAXIMUM TIME IT TAKES TO REVERSE THE ROTATION DIRECTION
public float maxReverseTime;

[Header("Switch Options")]
//THE MINIMUM TIME IT TAKES TO SWITCH POSITIONS
public float minSwitchTime;
// THE MAXIMUM TIME IT TAKES TO SWITCH POSITIONS
public float maxSwitchTime;
//HOW FAST IT MOVES TO SWITCHES TO IT'S SWITCH POINTS
public float switchSpeed;
}
