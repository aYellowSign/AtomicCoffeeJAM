using UnityEngine;
using UnityEngine.Splines;

public class CharacterBehavior : MonoBehaviour
{
    public SplineContainer _walkingPath;
    public float _movementSpeed = 5f;
    public float _normalSpeed = 5f;
    public float _t;

    public bool _isBlocked = false; 

    void Update()
    {
        if (_walkingPath == null) return;

        if (_isBlocked)
            return;

        
        _t += _movementSpeed * Time.deltaTime / _walkingPath.Spline.GetLength();
        if (_t > 1f) _t = 0f;

        Vector3 localPos = _walkingPath.Spline.EvaluatePosition(_t);
        Vector3 worldPos = _walkingPath.transform.TransformPoint(localPos);
        transform.position = worldPos;

        Vector3 localTangent = _walkingPath.Spline.EvaluateTangent(_t);
        Vector3 worldTangent = _walkingPath.transform.TransformDirection(localTangent);
        if (worldTangent != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(worldTangent);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("CheckPoint"))
        {
            Debug.Log("Checkpoint atteint — le joueur s’arrête !");
            _isBlocked = true;
        }
    }

    
    public void UnblockMovement()
    {
        Debug.Log("Interaction effectuée — le joueur reprend son mouvement !");
        _isBlocked = false;
    }
}
