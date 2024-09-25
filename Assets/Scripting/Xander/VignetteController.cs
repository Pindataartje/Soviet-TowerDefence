using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class VignetteController : MonoBehaviour
{
    public Volume globalVolume;
    public Camera mainCamera;
    public float maxIntensity = 0.5f;
    public float maxSmoothness = 0.8f;
    public Color closeColor = Color.red;
    public Color originalColor = Color.black;
    public float detectionRadius = 10f;

    private Vignette vignette;
    private float originalIntensity;
    private float originalSmoothness;

    void Start()
    {
        globalVolume.profile.TryGet(out vignette);
        originalIntensity = vignette.intensity.value;
        originalSmoothness = vignette.smoothness.value;
        vignette.color.value = originalColor;
    }

    void Update()
    {
        Collider[] borders = Physics.OverlapSphere(mainCamera.transform.position, detectionRadius);

        bool nearBorder = false;
        foreach (Collider border in borders)
        {
            if (border.CompareTag("Border"))
            {
                nearBorder = true;
                float distance = Vector3.Distance(mainCamera.transform.position, border.ClosestPoint(mainCamera.transform.position));
                float proximity = 1f - Mathf.Clamp01(distance / detectionRadius);

                vignette.intensity.value = Mathf.Lerp(originalIntensity, maxIntensity, proximity);
                vignette.smoothness.value = Mathf.Lerp(originalSmoothness, maxSmoothness, proximity);
                vignette.color.value = Color.Lerp(originalColor, closeColor, proximity);
                break;
            }
        }

        if (!nearBorder)
        {
            vignette.intensity.value = originalIntensity;
            vignette.smoothness.value = originalSmoothness;
            vignette.color.value = originalColor;
        }
    }
}
