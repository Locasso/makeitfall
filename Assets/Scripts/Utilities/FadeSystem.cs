using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeSystem : Singleton<FadeSystem>
{
	public delegate void FadeEnded();
	public static event FadeEnded OnFadeEnded;

	public static Animator animator;
	
	private void Start()
	{
		animator = this.gameObject.GetComponent<Animator>();
	}

	public static void FadeToLevel()
	{
		if (animator.IsNull()) { }
		else
		animator.SetTrigger("Fade");
	}

	public void OnFadeComplete()
	{
		//if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 0)
		//{
		//	Debug.Log("Do nothing");
		//}
		//else
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != DataModel.sceneToLoad || DataModel.testMode != true)
		{
			LoadingScreen.SceneToLoad(DataModel.sceneToLoad);

			//if(DataModel.sceneToLoad == "Mapa 1")
			//{
			//	Debug.Log("FOI O ONFADECOMPLETE DO MAP LOGIC");
			//	OnFadeEnded?.Invoke();
			//}
			
			//Debug.Log("Diferente");
		}
		else
		{
			animator.SetTrigger("FadeOut");
			//Debug.Log("Igual");
		}

	}
}
