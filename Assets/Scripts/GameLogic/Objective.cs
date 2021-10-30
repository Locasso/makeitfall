using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
	public delegate void PassLevel();
	public static event PassLevel OnLevelPassed;

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.layer == 8)
		{
			Debug.Log("Win");
			other.transform.position = this.gameObject.transform.position;
			other.GetComponent<Rigidbody>().isKinematic = true;
			OnLevelPassed?.Invoke();
		}
	}
}
