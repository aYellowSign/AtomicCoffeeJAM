using UnityEngine;
using UnityEngine.Splines;
using System.Collections; // Nécessaire pour les coroutines

public class GeorgeBehavior : MonoBehaviour
{
    public SplineContainer _walkingPath;
    public float _movementSpeed = 5f;      
    public float _normalSpeed = 5f;        
    public float _waitDuration = 2f;       
    public float _t;

    void Update()
    {
        if (_walkingPath == null) return;

        _t += _movementSpeed * Time.deltaTime / _walkingPath.Spline.GetLength();
        if (_t > 1f) _t = 0f;

     
        Vector3 pos = _walkingPath.Spline.EvaluatePosition(_t);
        transform.position = pos;

        Vector3 tangent = _walkingPath.Spline.EvaluateTangent(_t);
        if (tangent != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(tangent);
    }

    private void OnTriggerEnter(Collider other)
    {
        int collisionLayer = other.gameObject.layer;
        if (collisionLayer == LayerMask.NameToLayer("CheckPoint"))
        {
            Debug.Log("Collision avec un danger !");
            _movementSpeed = 0f;

            
            StartCoroutine(RestoreSpeedAfterDelay());
        }
    }

    private IEnumerator RestoreSpeedAfterDelay()
    {
        
        yield return new WaitForSeconds(_waitDuration);

        
        _movementSpeed = _normalSpeed;
        Debug.Log("Vitesse restaurée !");
    }
}

