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
                
                if (lastHitObject != null && lastMaterial != null)
                {
                    lastMaterial.SetFloat("_Outline_Thickness", 0f);
                }

               
                Renderer rend = hitObject.GetComponent<Renderer>();
                if (rend != null)
                {
                    lastMaterial = rend.material; 
                    lastMaterial.SetFloat("_Outline_Thickness", 0.05f);
                    Debug.Log("Outline");
                }

                lastHitObject = hitObject;
            }
        }
        else
        {
            
            if (lastHitObject != null && lastMaterial != null)
            {
                lastMaterial.SetFloat("_Outline_Thickness", 0f);
                lastHitObject = null;
                lastMaterial = null;
            }
        }
    }
}
