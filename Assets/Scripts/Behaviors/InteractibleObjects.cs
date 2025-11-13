using UnityEngine;

public class InteractibleObjects : MonoBehaviour
{
    private GameObject lastHitObject;
    private Material lastMaterial;

    [Header("Outline Settings")]
    public float outlineThickness = 1.0f; 

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
                    lastMaterial.SetFloat("_Outline_Thickness", 0f);
                }

                Renderer rend = hitObject.GetComponent<Renderer>();
                if (rend != null)
                {
                    lastMaterial = rend.material;
                    lastMaterial.SetInt("_IsOutlined", 1);
                    lastMaterial.SetFloat("_Outline_Thickness", outlineThickness); 
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
                lastMaterial.SetFloat("_Outline_Thickness", 0f); 
                lastMaterial = null;
                lastHitObject = null;
            }
        }
    }
}
