using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public delegate void FallClick();
	public static event FallClick OnFall;

	public delegate void RepeatClick();
	public static event RepeatClick OnRepeat;

	[SerializeField] private GameObject[] levelsPressets;

	[SerializeField] private GameObject passWindow;
	[SerializeField] private GameObject[] rbToManipulate;
	[SerializeField] private Vector3[] objPositions;
	[SerializeField] private Quaternion[] originalRotation;

	float rotationResetSpeed = 1.0f;
	bool passed;
	float min = 0f; //Valor mínimo para o lerp (neste caso, o mínimo quer dizer visão total)
	float max = 0.8f; //Valor máximo para o lerp (neste caso, o máximo quer dizer nenhuma visão)
	float t = 0.1f; //Tempo utilizado para calcular o lerp, startando em 0.1

	void Start()
	{
		passWindow = GameObject.Find("WinCanvas");
		passWindow.SetActive(false);
		rbToManipulate = GameObject.FindGameObjectsWithTag("Fallable");
		objPositions = new Vector3[rbToManipulate.Length];
		originalRotation = new Quaternion[rbToManipulate.Length];


		for (int i = 0; i < rbToManipulate.Length; ++i)
		{
			objPositions[i] = rbToManipulate[i].transform.position;
			originalRotation[i] = rbToManipulate[i].transform.rotation;
			rbToManipulate[i].GetComponent<Rigidbody>().isKinematic = true;
			if (rbToManipulate[i].layer == 9)
			{
				rbToManipulate[i].GetComponent<Rigidbody>().isKinematic = false;
				rbToManipulate[i].GetComponent<Rigidbody>().useGravity = false;
			}
		}

		//foreach (GameObject rbs in rbToManipulate)
		//{
		//	rbs.GetComponent<Rigidbody>().isKinematic = true;
		//}
	}

#region Inscrição e trancamento nos eventos

	void OnEnable()
	{
		Objective.OnLevelPassed += PassLevel;
	}

	void OnDisable()
	{
		Objective.OnLevelPassed -= PassLevel;
	}

#endregion

	public void MakeFall()
	{
		OnFall?.Invoke();
		foreach (GameObject rbs in rbToManipulate)
		{
			rbs.GetComponent<Rigidbody>().isKinematic = false;
			rbs.GetComponent<Rigidbody>().useGravity = true;
			rbs.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
			rbs.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
		}
	}

	public void TurnBack()
	{
		OnRepeat?.Invoke();
		for (int i = 0; i <= rbToManipulate.Length - 1; i++)
		{
			rbToManipulate[i].transform.rotation = Quaternion.Slerp(transform.rotation, 
			originalRotation[i], Time.time * rotationResetSpeed);

			rbToManipulate[i].transform.position = objPositions[i];
			rbToManipulate[i].GetComponent<Rigidbody>().isKinematic = true;
			rbToManipulate[i].GetComponent<Rigidbody>().useGravity = false;
			rbToManipulate[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
			if (rbToManipulate[i].layer == 9)
			{
				rbToManipulate[i].GetComponent<Rigidbody>().isKinematic = false;
			}
		}
		passed = false;
	}
	public void Update()
	{
		if(passed)
		{
			t += 0.6f * Time.deltaTime;
			passWindow.SetActive(true);
			passWindow.GetComponent<Image>().color = new Color(passWindow.GetComponent<Image>().color.r,
			passWindow.GetComponent<Image>().color.g, passWindow.GetComponent<Image>().color.b, Mathf.Lerp(min, max, t));

			for (int i = 0; i <= passWindow.GetComponentsInChildren<Image>().Length -1; i++)
			{
				passWindow.GetComponentsInChildren<Image>()[i].color = new Color(passWindow.GetComponentsInChildren<Image>()[i].color.r,
				passWindow.GetComponentsInChildren<Image>()[i].color.g, passWindow.GetComponentsInChildren<Image>()[i].color.b, Mathf.Lerp(min, max, t));
			}
		}
	}

	public void PassLevel()
	{
		passed = true;
	}
}
