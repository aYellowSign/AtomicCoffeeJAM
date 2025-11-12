using UnityEngine;
using System.Collections;

public class QuitGame : MonoBehaviour {


	public void ExitGame () {
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit ();
		#endif
	}
}
