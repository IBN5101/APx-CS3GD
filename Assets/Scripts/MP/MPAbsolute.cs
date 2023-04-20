using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPAbsolute : MonoBehaviour
{
	public MPAnimation mpAnimation;
	public MPControlsInput mpControlsInput;
	public MPNormalMovement mpNormalMovement;
	public MPSpecialMovement mpSpecialMovement;

	private void Start()
	{
		mpAnimation = GetComponent<MPAnimation>();
		mpControlsInput = GetComponent<MPControlsInput>();
		mpNormalMovement = GetComponent<MPNormalMovement>();
		mpSpecialMovement = GetComponent<MPSpecialMovement>();
	}
}
