using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPAbsolute : MonoBehaviour
{
	public MPAnimation Animation;
	public MPControlsInput ControlsInput;
	public MPNormalMovement NormalMovement;
	public MPSpecialMovement SpecialMovement;

	private void Start()
	{
		Animation = GetComponent<MPAnimation>();
		ControlsInput = GetComponent<MPControlsInput>();
		NormalMovement = GetComponent<MPNormalMovement>();
		SpecialMovement = GetComponent<MPSpecialMovement>();
	}

	public void ResetAllMovements()
	{
		NormalMovement.ResetNormalMovement();
		SpecialMovement.ResetSpecialMovement();
	}
}
