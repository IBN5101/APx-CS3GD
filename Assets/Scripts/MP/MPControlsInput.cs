using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// (IBN) Made with refernce to Unity's StarterAssets [ThirdPersonController.cs]
public class MPControlsInput : MonoBehaviour
{
	private PlayerInput _playerInput;

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

	public enum ActionMapName
	{
		NORMAL,
		SPECIAL,
		PAUSED,
	}

	// Events
	public event EventHandler OnActionAFired;

	private void Awake()
	{
		_playerInput = GetComponent<PlayerInput>();
	}

	private void Start()
	{
		SetCursorState(cursorLocked);

		GameController.Instance.OnGamePause += GameController_OnGamePause;
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
		DashInput(value.Get<Vector3>());
	}

	public void OnActionA(InputValue value)
	{
		OnActionAFired?.Invoke(this, EventArgs.Empty);
	}

	public void OnEscape(InputValue value)
	{
		GameController.Instance.PauseToggle();
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

	private void DashInput(Vector3 newDashDirection)
	{
		dash = newDashDirection;
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

	private void SetCursorAll(bool locked)
	{
		cursorLocked = locked;
		cursorInputForLook = locked;
		Cursor.lockState = cursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
	}
	#endregion

	public void ChangeActionMap(ActionMapName newActionMap)
	{
		switch (newActionMap)
		{
			case ActionMapName.NORMAL:
				_playerInput.SwitchCurrentActionMap("Normal");
				break;
			case ActionMapName.SPECIAL:
				_playerInput.SwitchCurrentActionMap("Special");
				break;
			case ActionMapName.PAUSED:
				_playerInput.SwitchCurrentActionMap("Paused");
				break;
		}
	}
	private void GameController_OnGamePause(object sender, bool paused)
	{
		if (paused)
		{
			ChangeActionMap(ActionMapName.PAUSED);
			SetCursorAll(false);
		}
		else
		{
			ChangeActionMap(ActionMapName.NORMAL);
			SetCursorAll(true);
		}

	}

}
