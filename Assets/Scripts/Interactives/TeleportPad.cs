using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPad : MonoBehaviour
{
	[Header("Settings")]

	[Tooltip("Teleport destination")]
	[SerializeField] private Transform _teleportDestination;
	[Tooltip("Time required to start teleport")]
	public float TeleportTime = 2.0f;

	private Coroutine _teleportCoroutine;

	private void Start()
	{
		LevelController.Instance.OnLevelReset += LevelController_OnLevelReset;
	}

	private void LevelController_OnLevelReset(object sender, System.EventArgs e)
	{
		StopTeleporting();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			_teleportCoroutine = StartCoroutine(Teleporting());
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			StopTeleporting();
		}
	}

	private IEnumerator Teleporting()
	{
		LevelController.Instance.ToggleTeleportVolume(true);

		yield return new WaitForSeconds(TeleportTime);
		LevelController.Instance.ForceTeleportPlayer(_teleportDestination.position);

		LevelController.Instance.ToggleTeleportVolume(false);
	}

	private void StopTeleporting()
	{
		if (_teleportCoroutine != null)
		{
			LevelController.Instance.ToggleTeleportVolume(false);

			StopCoroutine(_teleportCoroutine);
			_teleportCoroutine = null;
		}
	}
}
