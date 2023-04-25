using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class MPSoundEffects : MonoBehaviour
{
	[Header("Settings")]
	[Range(0, 1)] public float AudioVolume = 0.25f;

	[Header("Audio clips")]
	public AudioClip LandingAudioClip;
	public AudioClip[] FootstepAudioClips;
	public AudioClip DashingAudioClip;
	public AudioClip DeathAudioClip;

	private MPSpecialMovement _specialMovement;

	private void Awake()
	{
		_specialMovement = GetComponent<MPSpecialMovement>();
	}

	private void Start()
	{
		_specialMovement.OnDashingDash += SpecialMovement_OnDashingDash;
		LevelController.Instance.OnLevelReset += LevelController_OnLevelReset;
	}

	private void OnFootstep(AnimationEvent animationEvent)
	{
		if (animationEvent.animatorClipInfo.weight > 0.5f)
		{
			if (FootstepAudioClips.Length > 0)
			{
				var index = Random.Range(0, FootstepAudioClips.Length);
				AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.position, AudioVolume);
			}
		}
	}

	private void OnLand(AnimationEvent animationEvent)
	{
		if (animationEvent.animatorClipInfo.weight > 0.5f)
		{
			AudioSource.PlayClipAtPoint(LandingAudioClip, transform.position, AudioVolume);
		}
	}

	private void SpecialMovement_OnDashingDash(object sender, System.EventArgs e)
	{
		AudioSource.PlayClipAtPoint(DashingAudioClip, transform.position, AudioVolume);
	}

	private void LevelController_OnLevelReset(object sender, System.EventArgs e)
	{
		AudioSource.PlayClipAtPoint(DeathAudioClip, transform.position, AudioVolume);
	}
}
