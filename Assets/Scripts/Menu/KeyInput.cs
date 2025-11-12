using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class KeyInput : MonoBehaviour {

	public EventSystem eventSystem;
	public GameObject selectedObject;

	private bool buttonSelected;

	void Start () {
	}

	void Update ()
	{
		if (Input.GetAxisRaw ("Vertical") != 0 && buttonSelected == false)
		{
			eventSystem.SetSelectedGameObject (selectedObject);
			buttonSelected = true;
		}
		if (Input.GetAxisRaw ("Horizontal") != 0 && buttonSelected == false)
		{
			eventSystem.SetSelectedGameObject (selectedObject);
			buttonSelected = true;
		}
	}

	private void OnDisable()
		{
		buttonSelected = false;
	}
}
