using UnityEngine;

public class BillboardBehavior : MonoBehaviour
{
    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
    }

    void LateUpdate()
    {
        if (!mainCam) return;
        transform.rotation = Quaternion.LookRotation(transform.position - mainCam.transform.position);
    }
}
