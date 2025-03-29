using UnityEngine;

public class MeshRendererUtility : MonoBehaviour
{
    public void SetMaterialTexture(Texture2D texture)
    {
        if (texture == null)
            return;
        GetComponent<MeshRenderer>().SetMaterialTexture(texture);
    }
    public void MatchScaleToTextureSize(Texture2D texture)
    {
        if (texture == null)
            return;
        transform.localScale = new Vector3((float)texture.width / (float)texture.height, 1, 1);
    }
    public void SetMaterialAlpha(float alpha)
    {
        GetComponent<MeshRenderer>().material.SetFloat("_Alpha", alpha);
    }   
}
