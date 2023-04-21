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
		GameController.Instance.OnLevelReset += GameController_OnLevelReset;
	}

	private void GameController_OnLevelReset(object sender, System.EventArgs e)
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
		GameController.Instance.ToggleTeleportVolume(true);

		yield return new WaitForSeconds(TeleportTime);
		GameController.Instance.ForceTeleportPlayer(_teleportDestination.position);

		GameController.Instance.ToggleTeleportVolume(false);
	}

	private void StopTeleporting()
	{
		if (_teleportCoroutine != null)
		{
			GameController.Instance.ToggleTeleportVolume(false);

			StopCoroutine(_teleportCoroutine);
			_teleportCoroutine = null;
		}
	}
}
