using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
	private MeshRenderer _padMeshRenderer;
	private Material _jumpPadDisbled;
	private Material _jumpPadEnabled;

	private float _oldJumpHeight;
	[SerializeField] private float _newJumpHeight = 10;

	private void Awake()
	{
		_padMeshRenderer = transform.Find("Pad").GetComponent<MeshRenderer>();
		_jumpPadDisbled = GameAssets.Instance.m_JumpPadDisabled;
		_jumpPadEnabled = GameAssets.Instance.m_JumpPadEnabled;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			_padMeshRenderer.material = _jumpPadEnabled;
			if (other.TryGetComponent<MPNormalMovement>(out MPNormalMovement player))
			{
				_oldJumpHeight = player.JumpHeight;
				player.JumpHeight = _newJumpHeight;
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			_padMeshRenderer.material = _jumpPadDisbled;
			if (other.TryGetComponent<MPNormalMovement>(out MPNormalMovement player))
			{
				player.JumpHeight = _oldJumpHeight;
			}
		}
	}
}
