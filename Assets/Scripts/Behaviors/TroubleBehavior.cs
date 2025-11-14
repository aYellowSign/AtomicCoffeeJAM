using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections.Generic;

public class TroubleBehavior : MonoBehaviour
{
    [Header("Effets visuels")]
    public Volume _globalVolume;
    private ChromaticAberration _chromatic;

    [Header("Références du joueur")]
    public GameObject _player;
    private CharacterBehavior _characterBehavior;

    [Header("Paramètres d’interaction")]
    private Collider _collider;
    public float _radius = 2f;
    public bool unblocksMovement;

    [Header("Objets à activer / désactiver")]
    public List<GameObject> _objectsToTrue = new List<GameObject>();
    public List<GameObject> _objectsToFalse = new List<GameObject>();

    [Header("Dialogue associé")]
    public DialogueData dialogue;

    private void Start()
    {
        if (_globalVolume == null)
        {
            GameObject globalVolumeObj = GameObject.FindGameObjectWithTag("GlobalVolume");
            if (globalVolumeObj != null)
                _globalVolume = globalVolumeObj.GetComponent<Volume>();
        }

        _collider = GetComponent<Collider>();

        _player = GameObject.FindGameObjectWithTag("Player");
        if (_player == null)
        {
            Debug.LogError("Aucun GameObject avec le tag 'Player' n’a été trouvé !");
            return;
        }

        _characterBehavior = _player.GetComponent<CharacterBehavior>();
        if (_characterBehavior == null)
        {
            Debug.LogError(" Le joueur n’a pas de script 'CharacterBehavior' attaché !");
        }

        if (_globalVolume != null && !_globalVolume.profile.TryGet(out _chromatic))
        {
            Debug.LogWarning(" Aucun effet 'Chromatic Aberration' trouvé dans le Volume !");
        }
    }

    private void OnMouseDown()
    {
        if (_player == null || _characterBehavior == null)
        {
            Debug.LogError("Impossible d’interagir : référence au joueur manquante.");
            return;
        }

        if(DialogueManager.Instance.isInDialogue)
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray, 100f);
        if (hits.Length == 0) return;

        GameObject interactableHit = null;

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("Interactable")||hit.collider.CompareTag("Player"))
            {
                interactableHit = hit.collider.gameObject;
                break;
            }
        }

        if (interactableHit == null)
        {
            return;
        }

        if (interactableHit != gameObject)
            return;

        float distance = Vector3.Distance(transform.position, _player.transform.position);
        if (distance > _radius)
        {
            Debug.Log("Trop loin pour interagir.");
            return;
        }

        Debug.Log("Interaction réussie avec un objet TroubleBehavior !");

        foreach (GameObject obj in _objectsToTrue)
            if (obj != null) obj.SetActive(true);

        foreach (GameObject obj in _objectsToFalse)
            if (obj != null) obj.SetActive(false);

        if (dialogue != null)
            DialogueManager.Instance.LaunchDialogue(dialogue);

        if (_chromatic != null)
        {
            _chromatic.active = !_chromatic.active;
            _chromatic.intensity.value = _chromatic.active ? 1.0f : 0.0f;
            Debug.Log($"Chromatic Aberration activé : {_chromatic.active}");
        }

        if (unblocksMovement)
        {
            _characterBehavior.UnblockMovement();
        }
    }
}
