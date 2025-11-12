using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TroubleBehavior : MonoBehaviour
{
    public Volume _globalVolume;
    private ChromaticAberration _chromatic;

    private void Start()
    {
        _globalVolume.profile = Instantiate(_globalVolume.profile);

        if (!_globalVolume.profile.TryGet(out _chromatic))
        {
            Debug.LogWarning("Aucun effet Chromatic Aberration trouvé dans le Volume !");
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("Clic détecté !");

        if (_chromatic != null)
        {
            // Active ou désactive selon l’état actuel
            bool isActive = _chromatic.active;
            _chromatic.active = !isActive;

            
            if (_chromatic.active)
                _chromatic.intensity.value = 1.0f;
            else
                _chromatic.intensity.value = 0.0f; 

            Debug.Log($"Chromatic Aberration activé : {_chromatic.active}");
        }
    }
}


