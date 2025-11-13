using UnityEngine;

public class InteractibleObjects : MonoBehaviour
{
    private GameObject lastHitObject;
    private Material lastMaterial;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject != lastHitObject)
            {
                if (lastMaterial != null)
                {
                    lastMaterial.SetInt("_IsOutlined", 0);
                }

                Renderer rend = hitObject.GetComponent<Renderer>();
                if (rend != null)
                {
                    lastMaterial = rend.material; 
                    lastMaterial.SetInt("_IsOutlined", 1);
                    Debug.Log("Outline activé sur : " + hitObject.name);
                }

                lastHitObject = hitObject;
            }
        }
        else
        {
            if (lastMaterial != null)
            {
                lastMaterial.SetInt("_IsOutlined", 0);
                lastMaterial = null;
                lastHitObject = null;
            }
        }
    }
}
