using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections.Generic;

public class TroubleBehavior : MonoBehaviour
{
    public Volume _globalVolume;
    private ChromaticAberration _chromatic;
    public List<GameObject> _objectsToTrue = new List<GameObject>();
    public List<GameObject> _objectsToFalse = new List<GameObject>();

    private void Start()
    {
        _globalVolume.profile = Instantiate(_globalVolume.profile);

        if (!_globalVolume.profile.TryGet(out _chromatic))
        {
            Debug.LogWarning("Aucun effet Chromatic Aberration trouv� dans le Volume !");
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("Clic d�tect� !");

        //Active tous les objets de la liste
        if (_objectsToTrue.Count > 0)
        {
            foreach (GameObject obj in _objectsToTrue)
            {
                obj.SetActive(true);
            }
        }

        //Désactive tous les objets de la liste
        if (_objectsToFalse.Count > 0)
        {
            foreach (GameObject obj in _objectsToFalse)
            {
                obj.SetActive(false);
            }
        }

        if (_chromatic != null)
        {
            // Active ou d�sactive selon l��tat actuel
            bool isActive = _chromatic.active;
            _chromatic.active = !isActive;

            
            if (_chromatic.active)
                _chromatic.intensity.value = 1.0f;
            else
                _chromatic.intensity.value = 0.0f; 

            Debug.Log($"Chromatic Aberration activ� : {_chromatic.active}");
        }
    }
}


