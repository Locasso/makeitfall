using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class LanguageManager : MonoBehaviour
{

	public static LanguageManager instance;

	[SerializeField] private LanguageCollection languageCollection;
	[SerializeField] private Language language;
	[SerializeField] private string languageJson;

	public LanguageCollection LanguageCollection { get => languageCollection; set => languageCollection = value; }

	void Awake()
    {
		LoadLanguageJson();
    }

	void Start()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	void LoadLanguageJson()
	{
		LanguageCollection = JsonConvert.DeserializeObject<LanguageCollection>(LoadResourceTextfile("Language"));
		Debug.Log("CONTADOR DE LÍNGUAS: " + LanguageCollection.language.Count);
	}

	public static string LoadResourceTextfile(string path)
	{

		string filePath = "Json/" + path;

		TextAsset targetFile = Resources.Load<TextAsset>(filePath);

		Debug.Log(targetFile.text);
		return targetFile.text;
	}
}
