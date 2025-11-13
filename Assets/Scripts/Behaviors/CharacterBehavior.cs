using UnityEngine;
using UnityEngine.Splines;
using System.Collections;

public class CharacterBehavior : MonoBehaviour
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

        // Convert local spline position to world space
        Vector3 localPos = _walkingPath.Spline.EvaluatePosition(_t);
        Vector3 worldPos = _walkingPath.transform.TransformPoint(localPos);
        transform.position = worldPos;

        // Convert tangent direction to world space
        Vector3 localTangent = _walkingPath.Spline.EvaluateTangent(_t);
        Vector3 worldTangent = _walkingPath.transform.TransformDirection(localTangent);

        if (worldTangent != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(worldTangent);
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
