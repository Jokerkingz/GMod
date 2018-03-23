using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scr_SceneManager : MonoBehaviour {
	
	public string firstSceneName;
	private static Scr_SceneManager _instance;
	public static Scr_SceneManager Instance
	{
		get{
		if (_instance ==null)
		{
			_instance = GameObject.FindObjectOfType<Scr_SceneManager>();
		}
		return _instance;
	}
	}

	void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
	}
	void Start () {
		LoadFirstScene(firstSceneName);
	}

	void Update () {	
		
	}

	public void LoadFirstScene(string firstSceneName)
	{
		SceneManager.LoadSceneAsync(firstSceneName, LoadSceneMode.Additive);	
	}

	public void LoadNext(string sceneName)
	{
		if (!SceneManager.GetSceneByName(sceneName).isLoaded)
		{
				SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
		}
	}

	public void UnloadPrevious (string sceneName)
	{
		if (SceneManager.GetSceneByName(sceneName).isLoaded)
		{
			SceneManager.UnloadSceneAsync(sceneName);
		}
	}

	public void OnSceneFinishedLoading( Scene sceneName, LoadSceneMode mode)
	{
		Debug.Log("Scene Done Loading");
	}

}
