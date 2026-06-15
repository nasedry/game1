using UnityEngine;
using UnityEngine.UI;

public class ScreenBlur : MonoBehaviour
{
    public Material blurMaterial;
    public RawImage outputImage;
    public int downsample = 2;
    public int iterations = 2;
    public float offset = 1f;

    private RenderTexture currentRT;

    public void CaptureAndBlur()
    {
        if (blurMaterial == null || outputImage == null)
        {
            Debug.LogWarning("ScreenBlur: missing references");
            return;
        }

        if (currentRT != null)
        {
            RenderTexture.ReleaseTemporary(currentRT);
            currentRT = null;
        }

        int w = Mathf.Max(1, Screen.width / downsample);
        int h = Mathf.Max(1, Screen.height / downsample);

        RenderTexture rtA = RenderTexture.GetTemporary(w, h, 0, RenderTextureFormat.Default);
        RenderTexture rtB = RenderTexture.GetTemporary(w, h, 0, RenderTextureFormat.Default);

        Camera cam = Camera.main;
        if (cam == null)
        {
            Debug.LogWarning("ScreenBlur: no Camera.main found");
            RenderTexture.ReleaseTemporary(rtA);
            RenderTexture.ReleaseTemporary(rtB);
            return;
        }

        RenderTexture prev = cam.targetTexture;
        cam.targetTexture = rtA;
        cam.Render();
        cam.targetTexture = prev;

        for (int i = 0; i < iterations; i++)
        {
            blurMaterial.SetVector("_Offset", new Vector4(offset, 0, 0, 0));
            Graphics.Blit(rtA, rtB, blurMaterial);
            blurMaterial.SetVector("_Offset", new Vector4(0, offset, 0, 0));
            Graphics.Blit(rtB, rtA, blurMaterial);
        }

        currentRT = rtA;
        outputImage.texture = currentRT;

        RenderTexture.ReleaseTemporary(rtB);
    }

    private void OnDisable()
    {
        if (currentRT != null)
        {
            RenderTexture.ReleaseTemporary(currentRT);
            currentRT = null;
        }
    }

    public void ClearBlur()
    {
        if (outputImage != null)
            outputImage.texture = null;

        if (currentRT != null)
        {
            RenderTexture.ReleaseTemporary(currentRT);
            currentRT = null;
        }
    }
}
