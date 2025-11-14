using UnityEngine;

public class SpriteToQuad : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Material quadMaterial;

    void Update()
    {
        if (spriteRenderer.sprite != null)
        {
            quadMaterial.mainTexture = spriteRenderer.sprite.texture;
        }
    }
}
