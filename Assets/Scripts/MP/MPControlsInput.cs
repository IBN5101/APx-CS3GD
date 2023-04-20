using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MPControlsInput : MonoBehaviour
{
	private PlayerInput playerInput;

	[Header("Normal Input Values")]
	public Vector2 move;
	public Vector2 look;
	public bool jump;
	[Header("Special Input Values")]
	public Vector3 dash;

	[Header("Movement Settings")]
	public bool analogMovement;

	[Header("Mouse Cursor Settings")]
	public bool cursorLocked = true;
	public bool cursorInputForLook = true;

	private void Awake()
	{
		playerInput = GetComponent<PlayerInput>();
	}

	private void Start()
	{
		SetCursorState(cursorLocked);
	}

	public void OnMove(InputValue value)
	{
		MoveInput(value.Get<Vector2>());
	}

	public void OnLook(InputValue value)
	{
		if (cursorInputForLook)
		{
			LookInput(value.Get<Vector2>());
		}
	}

	public void OnJump(InputValue value)
	{
		JumpInput(value.isPressed);
	}

	public void OnDash(InputValue value)
	{
		dash = value.Get<Vector3>();
	}

	public void OnClick(InputValue value)
	{
		//if (playerInput.currentActionMap.name == "Normal")
		//{
		//    playerInput.SwitchCurrentActionMap("Special");
		//}
		//else
		//{
		//    playerInput.SwitchCurrentActionMap("Normal");
		//}
	}

	public void OnExitGame(InputValue value)
	{
		// NOTE: THIS IS A BAD IDEA
		Application.Quit();
	}

	#region Update values
	private void MoveInput(Vector2 newMoveDirection)
	{
		move = newMoveDirection;
	}

	private void LookInput(Vector2 newLookDirection)
	{
		look = newLookDirection;
	}

	private void JumpInput(bool newJumpState)
	{
		jump = newJumpState;
	}
	#endregion

	#region Cursor
	private void OnApplicationFocus(bool hasFocus)
	{
		SetCursorState(cursorLocked);
	}

	private void SetCursorState(bool newState)
	{
		Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
	}
	#endregion
}
