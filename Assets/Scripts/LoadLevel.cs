using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour {

	public void ChangeLevel (int sceneToChangeTo) {
	//using scene index in place of scene name
	//public void ChangeLevel (string sceneToChangeTo) {
		Application.LoadLevel (sceneToChangeTo);

	}
}
