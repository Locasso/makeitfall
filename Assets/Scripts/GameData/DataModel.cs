using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DataModel
{
    static public string projectName = "MakeItFall";

	[Header("Arquivos")]
	static public string pathToFiles = "";

	[Header("Sistema de seleção de area")]
	static public List<int> orderSlots = new List<int>();

	[Header("Sistema de estrela")]
	static public List<int> starsList = new List<int>();
	static public int starsCount = 0;
	static public int starsSum = 0;

	[Header("Sistema de progresso")]
	static public List<int> porcentageList = new List<int>();
	static public int porcentageCount = 0;

	[Header("Sistema de Tutorial")]
	static public int tutorialDone = 0;
	//static public int currentTutorial = 0;

	[Header("Template dos balões")]
	static public float positionBlowned = 0f;
	static public int ballonQuantity = 0;
	static public bool balloonAnswer = false;

	[Header("Dados do usuário")]
	static public int score = 0;

	[Header("Audio")]
	static public bool muteBgm = false;
	static public bool muteSfx = false;
	static public bool startedMute = true;

	[Header("Linguagens")]
	static public int idLanguage = 0; //0 = PT / 1 = EN

	[Header("Utilitários")]
	static public bool fadedIn = false;
	static public string sceneToLoad = "";
	static public string previusScene = "";

	[Header("Modo de Teste")]
	static public bool testMode = false;
	static public bool activityPassTest = false;
	static public bool writeDoc = false;

	static public void Reset()
	{
		projectName = "Moobing";
		orderSlots = new List<int>();

		starsList = new List<int>();
		starsCount = 0;
		starsSum = 0;

		porcentageList = new List<int>();
		porcentageCount = 0;

		tutorialDone = 0;

		positionBlowned = 0f;
		ballonQuantity = 0;
		balloonAnswer = false;

		score = 0;

		muteBgm = false;
		muteSfx = false;
		startedMute = true;

		idLanguage = 0;

		fadedIn = false;
		sceneToLoad = "";
		previusScene = "";

		testMode = false;
		activityPassTest = false;
		writeDoc = false;
	}
}