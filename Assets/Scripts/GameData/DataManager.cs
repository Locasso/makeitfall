using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Security.Cryptography;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

[SerializeField]
public class DataManager: MonoBehaviour{

	public delegate void ReadJson();
	public static event ReadJson OnJsonLoaded;

	public delegate void PlayerDataLoaded();
	public static event	PlayerDataLoaded OnPlayerDataLoaded;

	public delegate void VersionCheckEnd();
	public static event	VersionCheckEnd OnVersionCheckEnd;

	public float myTimer = 0;
    public bool StartCount = false;
    public int MeuTimer = 0;
    public string meu_id;
    public string meuArquivo;
    public string splitString;

    [SerializeField]
    private GameObject loading;
    public bool activateLoadingOnStart;
    [SerializeField]
    public GameObject popupErro;


	[SerializeField] private GameObject attPanel;
	[SerializeField] private GameObject reconectBtn;
	[SerializeField] private Text reconectTxt;

	[SerializeField] private bool synchronization;
	[SerializeField] private Texture2D[] textures;

    [SerializeField] public static string versionGot;

	public void ChangeTimer()
    {
        if (StartCount)
        {
            StartCount = false;
        }
        else
        {
            myTimer = 0;
            StartCount = true;
        }
    }

    // Use this for initialization
    public void Start ()
	{
		if (loading == null) loading = GameObject.FindWithTag("loading");
		if (activateLoadingOnStart && !loading.activeSelf) loading.SetActive(true);

		if (attPanel.IsNull()) { }
		else
			attPanel.SetActive(true);

		if (reconectBtn.IsNull()) { }
		else
			reconectBtn.SetActive(false);
	}

	// Update is called once per frame
	public void Update () {

        if (StartCount)
        {
            myTimer += Time.deltaTime;
            MeuTimer = Convert.ToInt32(myTimer);

            //Debug.Log("Timer: " + MeuTimer);
        }

    }

	 public void Save(bool sync)
    {
        BinaryFormatter bf = new BinaryFormatter();

        print("VAMOS SALVAR OS DADOS >" + Application.persistentDataPath + "/" + DataModel.projectName + "_" + ".dat");

		using (FileStream file = File.Open(Application.persistentDataPath + "/" + DataModel.projectName + "_" + ".dat", FileMode.OpenOrCreate))
		{
			PlayerData data = new PlayerData();
			//data.matricula = DataModel.matricula;			
			data.score = DataModel.score;
			data.pathToFiles = DataModel.pathToFiles;
			data.previusScene = DataModel.previusScene;
			data.orderSlots = DataModel.orderSlots;
			data.muteBgm = DataModel.muteBgm;
			data.muteSfx = DataModel.muteSfx;
			data.starsList = DataModel.starsList;
			data.starsSum = DataModel.starsSum;
			data.porcentageList = DataModel.porcentageList;

			Debug.Log("<moobing> Save Data");
			bf.Serialize(file, data);
		}
    }

    public void LimpaDados()
    {
		DataModel.Reset();
    }

	public void BackToScene(string cena)
	{
		FindObjectOfType<AudioManager>().Play("click_btn");
		//Save(synchronization);
		LoadingScreen.LoadScene(cena);
		DataModel.previusScene = SceneManager.GetActiveScene().name;
	}

	public void DoSound()
	{
		if (!DataModel.startedMute)
			FindObjectOfType<AudioManager>().Play("click_btn");
	}

    public void Load()
    {
		Debug.Log("++ LOAD >"+ Application.persistentDataPath + "/" + DataModel.projectName + ".dat");

        if (File.Exists(Application.persistentDataPath + "/" + DataModel.projectName + "_" + ".dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			using (FileStream file = File.Open(Application.persistentDataPath + "/" + DataModel.projectName + ".dat", FileMode.OpenOrCreate))
			{
				PlayerData data = (PlayerData)bf.Deserialize(file);
				DataModel.score = data.score;		
				DataModel.pathToFiles = data.pathToFiles;
				DataModel.previusScene = data.previusScene;	
				DataModel.orderSlots = data.orderSlots;
				DataModel.muteBgm = data.muteBgm;
				DataModel.muteSfx = data.muteSfx;
				DataModel.starsList = data.starsList;
				DataModel.starsSum = data.starsSum;
				DataModel.porcentageList = data.porcentageList;
		
			}
			OnPlayerDataLoaded?.Invoke();
		}
		else
		{
			//Save(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name, synchronization);
			Debug.Log("Não tem arquivo salvo para dar Load");
			OnPlayerDataLoaded?.Invoke();
		}
		
	}



[System.Serializable]
    class PlayerData
    {
		public string userInfo;

		public int score;

		public string previusScene;
		public string pathToFiles;

		public List<int> orderSlots;

		public List<int> starsList;
		public int starsSum;

		public List<int> porcentageList;

		public bool muteBgm;
		public bool muteSfx;
    }
    

    public void ShowMsg(string msg){

        #if UNITY_EDITOR
            Debug.Log("Attention ->"+ msg);
            popupErro.SetActive(true);
            popupErro.transform.GetChild(3).GetChild(0).gameObject.GetComponent<Text>().text = msg;
        #else
            popupErro.SetActive(true);
            popupErro.transform.GetChild(3).GetChild(0).gameObject.GetComponent<Text>().text = msg;
        #endif     
    }
}