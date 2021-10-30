using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System;


public class Settings : MonoBehaviour {

	public delegate void Mute();
	public static event Mute OnMute;

    [SerializeField] public Text app_version;
	[SerializeField] public Text meu_id;
    [SerializeField] private DataManager dataManager;

	[SerializeField] private Toggle bgm, sfx;


	/*[SerializeField]*/ public bool logOut = false;
	/*[SerializeField] */public bool deleteUser = false;

    public string filePath;
    //public string meu_id;

    [SerializeField]
    public GameObject popUp_Box;
    
    public void AskUser(string msg)
    {
		FindObjectOfType<AudioManager>().Play("click_btn");
		popUp_Box.SetActive(true);
        popUp_Box.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = msg;
    }

    public void OK_clicked(){

		FindObjectOfType<AudioManager>().Play("click_btn");

		if (deleteUser)
		{
			if (File.Exists(filePath))
			{
				File.Delete(filePath);
				dataManager.LimpaDados();
				Debug.Log("<Moobing> File deleted!");
				LoadingScreen.LoadScene("intro");
				ResetBools();
			}
			else
			{

				Debug.Log("<Moobing>File não existe...");
			}
		}
		else if(logOut)
		{
			LogoutFnc();
			ResetBools();
		}
    }

	/// <summary>
	/// Função temporária, para controlar as bools. True para setar a logOut, e false para setar a deleteUser.
	/// </summary>
	/// <param name="boolType"></param>
	public void SetBool(bool boolType)
	{
		if(boolType == true)
		{
			logOut = true;
		}
		if (boolType == false)
		{
			deleteUser = true;
		}
	}
	public void ResetBools()
	{
		logOut = false;
		deleteUser = false;
	}

	public void LogoutFnc()
    {
        dataManager.LimpaDados();
		LoadingScreen.LoadScene("intro");
	}

    // Use this for initialization
    void Start () {
		//Functions.Load();
		bgm = GameObject.Find("bgm_mute").GetComponent<Toggle>();
		sfx = GameObject.Find("sfx_mute").GetComponent<Toggle>();

		if (DataModel.muteBgm)
			bgm.isOn = false;
		else
			bgm.isOn = true;

		if (DataModel.muteSfx)
			sfx.isOn = false;
		else
			sfx.isOn = true;


		FindObjectOfType<AudioManager>().Stop("nature_bgm");
		FindObjectOfType<AudioManager>().Stop("wind_bgm");
		app_version.text = "WowlApp version: " + Application.version;
		filePath = Application.persistentDataPath + "/" + DataModel.projectName + ".dat";
	}

	public void Bgm(Toggle mute)
	{
		if (mute.isOn)
			DataModel.muteBgm = false;
		else
			DataModel.muteBgm = true;
		OnMute?.Invoke();
	}

	public void Sfx(Toggle mute)
	{
		if (mute.isOn)
			DataModel.muteSfx = false;
		else
			DataModel.muteSfx = true;
		OnMute?.Invoke();
	}

	public void VoltaItens()
    {
		dataManager.Save(false);
		FindObjectOfType<AudioManager>().Play("click_btn");
		LoadingScreen.LoadScene(DataModel.previusScene);
    }
}
