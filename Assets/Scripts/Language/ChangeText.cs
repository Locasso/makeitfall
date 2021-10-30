using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeText : MonoBehaviour
{
	[SerializeField] private LanguageManager languageManager;
	[SerializeField] private Text objText;
	[SerializeField] private int txtId;

	[SerializeField] private bool isTutorial;
	private void Awake()
	{
		languageManager = null;
		languageManager = FindObjectOfType<LanguageManager>();
	}

	void Start()
    {
		//languageManager = null;
		//languageManager = FindObjectOfType<LanguageManager>();

		if (!isTutorial) 
		TextChanger(objText, txtId);
    }

	public void TextChanger(Text text, int id)
	{
		text.text = languageManager.LanguageCollection.language[DataModel.idLanguage].textos[id].texto;
	}


	public void TutorialStringChanger(string[] text, int idStarter)
	{
		for (int i = 0; i <= text.Length - 1; i++)
		{
			text[i] = languageManager.LanguageCollection.language[DataModel.idLanguage].textos[idStarter].texto;
			idStarter++;
		}
	}
	//public string AddLine(string oldString, string newLine)
	//{
	//	oldString += "\n" + newLine;
	//	return oldString;
	//}
}
