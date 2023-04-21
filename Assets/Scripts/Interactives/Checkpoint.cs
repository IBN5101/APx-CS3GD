using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
	private MeshRenderer _capsule1MeshRenderer;
	private MeshRenderer _capsule2MeshRenderer;
	private Material _checkpointDisbled;
	private Material _checkpointEnabled;

	private bool _checkpointActivated = false;

	private void Awake()
	{
		_capsule1MeshRenderer = transform.Find("Capsule1").GetComponent<MeshRenderer>();
		_capsule2MeshRenderer = transform.Find("Capsule2").GetComponent<MeshRenderer>();
		_checkpointDisbled = GameAssets.Instance.m_checkpointDisabled;
		_checkpointEnabled = GameAssets.Instance.m_checkpointEnabled;
	}

	private void Start()
	{
		DisableCheckpoint();
		LevelController.Instance.OnCheckpointDisable += LevelController_OnCheckpointDisable;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			if (!_checkpointActivated)
			{
				LevelController.Instance.DisableAllCheckpoints();
				LevelController.Instance.SetRespawnLocation(transform.position);
				EnableCheckpoint();
			}
		}
	}

	private void LevelController_OnCheckpointDisable(object sender, System.EventArgs e)
	{
		DisableCheckpoint();
	}

	public void EnableCheckpoint()
	{
		_checkpointActivated = true;

		_capsule1MeshRenderer.material = _checkpointEnabled;
		_capsule2MeshRenderer.material = _checkpointEnabled;
	}

	public void DisableCheckpoint()
	{
		_checkpointActivated = false;

		_capsule1MeshRenderer.material = _checkpointDisbled;
		_capsule2MeshRenderer.material = _checkpointDisbled;
	}
}
