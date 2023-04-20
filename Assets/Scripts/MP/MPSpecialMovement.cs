using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class MPSpecialMovement : MonoBehaviour
{
	[SerializeField] private float _dashingTime = 1.0f;
	[SerializeField] private float _dashingSpeed = 5.0f;
	[SerializeField] private float _cooldownTime = 1.0f;

	private MPControlsInput _input;
	private MPNormalMovement _normalMovement;
	private CharacterController _controller;

	private Vector3 _currentDashingDirection;
	private bool _dashingCompleted = false;
	private bool _cooldownCompleted = false;

	// (IBN) Should have used a proper State Machine ...
	private enum State
	{
		IDLE,
		PREP,
		DASH,
		COOLDOWN,
	}
	[SerializeField]
	[ReadOnlyInspector]
	private State _state;

	private void Start()
	{
		_input = GetComponent<MPControlsInput>();
		_normalMovement = GetComponent<MPNormalMovement>();
		_controller = GetComponent<CharacterController>();

		_state = State.IDLE;

		_input.OnActionAFired += input_OnActionAFired;
	}

	private void Update()
	{
		switch (_state)
		{
			case State.DASH:
				Vector3 dashingDirection = transform.TransformDirection(_currentDashingDirection);
				_controller.Move(dashingDirection * _dashingSpeed * Time.deltaTime);

				if (_dashingCompleted)
					SwitchState(State.COOLDOWN);
				break;
			case State.COOLDOWN:
				if (_normalMovement.Grounded && _cooldownCompleted)
					SwitchState(State.IDLE);
				break;

			default:
				break;
		}
	}

	private void input_OnActionAFired(object sender, System.EventArgs e)
	{
		switch (_state)
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
		_state = newState;
		//Debug.Log("MPSpecialMovement, new State: " + _state);
	}

	private void OnPrepStart()
	{
		GameController.Instance.ChangeTimeScale(0.1f);
		_input.SwitchActionMap();
	}

	private void OnDashStart()
	{
		GameController.Instance.ChangeTimeScale(1.0f);
		_normalMovement.ToggleNormalMovement(false);

		_dashingCompleted = false;
		StartCoroutine(DashingTimer());
	}

	private void OnCooldownStart()
	{
		GameController.Instance.ChangeTimeScale(1.0f);
		_normalMovement.ToggleNormalMovement(true);
		_input.SwitchActionMap();

		_cooldownCompleted = false;
		StartCoroutine(CooldownTimer());
	}

	private IEnumerator DashingTimer()
	{
		yield return new WaitForSeconds(_dashingTime);
		_dashingCompleted = true;
	}

	private IEnumerator CooldownTimer()
	{
		yield return new WaitForSeconds(_cooldownTime);
		_cooldownCompleted = true;
	}
}
