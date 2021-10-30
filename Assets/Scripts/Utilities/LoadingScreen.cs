using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
	[SerializeField] private static string sceneToLoad;
	[SerializeField] private GameObject loadingWheel, loadingDone;

    void Start()
    {
		loadingDone.gameObject.SetActive(false);
		StartCoroutine(AsynchronousLoad(sceneToLoad));
	}

	public static void LoadScene(string scene)
	{
		FadeSystem.FadeToLevel();
		DataModel.sceneToLoad = scene;
	}

	public static void SceneToLoad(string sceneName)
	{
		SceneManager.LoadScene("loading_scene");
		sceneToLoad = sceneName;
		DataModel.sceneToLoad = sceneName;
	}

	public IEnumerator AsynchronousLoad(string scene)
	{
		yield return null;

		AsyncOperation ao = SceneManager.LoadSceneAsync(scene);
		ao.allowSceneActivation = false;

		while (!ao.isDone)
		{
			//float progress = Mathf.Clamp01(ao.progress / 0.9f);
			//progressBar.fillAmount = ao.progress * Time.time;
			//textPorcentage.text = Mathf.Round((progress * 100)).ToString() + "%";
			//textPorcentage.text = string.Format("{0:0.00}", tempString);
			// Loading completed
			if (ao.progress == 0.9f)
			{
				loadingWheel.gameObject.SetActive(false);
				loadingDone.gameObject.SetActive(true);
				//Debug.Log("Press a key to start");
				//if (Input.GetMouseButtonDown(0))
				ao.allowSceneActivation = true;
			}

			yield return null;
		}
	}
}
