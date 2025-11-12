using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections.Generic;

public class TroubleBehavior : MonoBehaviour
{
    public Volume _globalVolume;
    private ChromaticAberration _chromatic;
    public GameObject _player;
    private Collider _collider;
    public float _radius = 2f;
    public List<GameObject> _objectsToTrue = new List<GameObject>();
    public List<GameObject> _objectsToFalse = new List<GameObject>();

    private void Start()
    {
        _globalVolume.profile = Instantiate(_globalVolume.profile);

        _collider = GetComponent<Collider>();
        _player = GameObject.FindGameObjectWithTag("Player");

        if (!_globalVolume.profile.TryGet(out _chromatic))
        {
            Debug.LogWarning("Aucun effet Chromatic Aberration trouv� dans le Volume !");
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, _player.transform.position);
        if (distance < _radius)
            Debug.Log("Object is in Range");
    }

    private void OnMouseDown()
    {
        float distance = Vector3.Distance(transform.position, _player.transform.position);

        if (distance > _radius)
            return;
        else
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
        
}


