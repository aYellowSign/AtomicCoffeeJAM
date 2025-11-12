using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickToLoadAsync : MonoBehaviour {

	public Image LoadingBar;
	public GameObject loadingScreen;


	private AsyncOperation async;


	public void ClickAsync(int level)
	{
		loadingScreen.SetActive(true);
		StartCoroutine(LoadLevelWithBar(level));
	}


	IEnumerator LoadLevelWithBar (int level)
	{
		async = SceneManager.LoadSceneAsync(level);
		while (!async.isDone)
		{
			LoadingBar.fillAmount = async.progress;
			yield return null;
		}
	}
}
