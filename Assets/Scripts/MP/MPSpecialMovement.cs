using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class MPSpecialMovement : MonoBehaviour
{
	[Header("Settings")]

	[Tooltip("Duration of dashing action")]
	public float DashingTime = 0.5f;
	[Tooltip("Dashing speed of the character in m/s")]
	public float DashingSpeed = 10.0f;
	[Tooltip("Duration of cooldown period")]
	public float CooldownTime = 1.0f;
	[Tooltip("Dashing cooldown if the player is Grounded")]
	public bool CooldownRequireGrounded = true;
	[Space(10)]

	private MPControlsInput _input;
	private MPNormalMovement _normalMovement;
	private CharacterController _controller;

	private Vector3 _currentDashingDirection;
	private bool _dashingCompleted = false;
	private bool _cooldownCompleted = false;

	// (IBN) Should have used a proper State Machine ...
	public enum State
	{
		IDLE,
		PREP,
		DASH,
		COOLDOWN,
	}
	[SerializeField]
	[ReadOnlyInspector]
	private State _currentState;

	// Event
	public event EventHandler<bool> OnDashingIdle;

	private void Start()
	{
		_input = GetComponent<MPControlsInput>();
		_normalMovement = GetComponent<MPNormalMovement>();
		_controller = GetComponent<CharacterController>();

		_currentState = State.IDLE;

		_input.OnActionAFired += input_OnActionAFired;
	}

	private void Update()
	{
		switch (_currentState)
		{
			case State.DASH:
				Vector3 dashingDirection = transform.TransformDirection(_currentDashingDirection);
				_controller.Move(dashingDirection * DashingSpeed * Time.deltaTime);

				if (_dashingCompleted)
					SwitchState(State.COOLDOWN);
				break;
			case State.COOLDOWN:
				bool groundedCheck = true;
				if (CooldownRequireGrounded)
					groundedCheck = _normalMovement.Grounded;
				if (groundedCheck && _cooldownCompleted)
					SwitchState(State.IDLE);
				break;

			default:
				break;
		}
	}

	private void input_OnActionAFired(object sender, System.EventArgs e)
	{
		switch (_currentState)
		{
			case State.IDLE:
				SwitchState(State.PREP);
				break;
			case State.PREP:
				if (_input.dash == Vector3.zero)
				{
					SwitchState(State.COOLDOWN);
				}
				else
				{
					_currentDashingDirection = _input.dash;
					SwitchState(State.DASH);
				}	
				break;
			default:
				break;
		}
	}

	private void SwitchState(State newState)
	{
		switch (newState)
		{
			case State.IDLE:
				OnIdleStart();
				break;
			case State.PREP:
				OnPrepStart();
				break;
			case State.DASH:
				OnDashStart();
				break;
			case State.COOLDOWN:
				OnCooldownStart();
				break;
		}
		_currentState = newState;
		//Debug.Log("MPSpecialMovement, new State: " + _state);
	}

	private void OnIdleStart()
	{
		OnDashingIdle?.Invoke(this, true);
	}

	private void OnPrepStart()
	{
		LevelController.Instance.ChangeTimeScale(0.1f);
		LevelController.Instance.ToggleDashingVolume(true);
		_input.ChangeActionMap(MPControlsInput.ActionMapName.SPECIAL);
	}

	private void OnDashStart()
	{
		LevelController.Instance.ChangeTimeScale(1.0f);
		_normalMovement.ToggleNormalMovement(false);

		_dashingCompleted = false;
		StartCoroutine(DashingTimer());
	}

	private void OnCooldownStart()
	{
		OnDashingIdle?.Invoke(this, false);

		LevelController.Instance.ChangeTimeScale(1.0f);
		LevelController.Instance.ToggleDashingVolume(false);
		_normalMovement.ToggleNormalMovement(true);
		_input.ChangeActionMap(MPControlsInput.ActionMapName.NORMAL);

		_cooldownCompleted = false;
		StartCoroutine(CooldownTimer());
	}

	private IEnumerator DashingTimer()
	{
		yield return new WaitForSeconds(DashingTime);
		_dashingCompleted = true;
	}

	private IEnumerator CooldownTimer()
	{
		yield return new WaitForSeconds(CooldownTime);
		_cooldownCompleted = true;
	}

	public void ResetSpecialMovement()
	{
		SwitchState(State.COOLDOWN);
	}
}
