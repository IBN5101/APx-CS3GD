using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerribleTrigger : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			LevelController.Instance.LevelReset();
		}
	}
}
