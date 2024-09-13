using UnityEngine;
using TMPro;

public class ButtonGlowAnimator : MonoBehaviour
{
    public TextMeshProUGUI buttonText; // Reference to the TextMeshProUGUI component of the button

    private float timeOffset;
    private float timeInner;
    private float timeOuter;
    private float timePower;

    private float offset;
    private float inner;
    private float outer;
    private float power;

    private float offsetMin = -1f;
    private float offsetMax = 0.14f;
    private float innerMin = 0.1f;
    private float innerMax = 0.3f;
    private float outerMin = 0.1f;
    private float outerMax = 0.25f;
    private float powerMin = 0.3f;
    private float powerMax = 0.7f;

    private float speed = 0.2f; // Slow and consistent speed

    void Start()
    {
        // Initialize time variables with a random start to desynchronize the patterns
        timeOffset = Random.value * 10f;
        timeInner = Random.value * 10f;
        timeOuter = Random.value * 10f;
        timePower = Random.value * 10f;
    }

    void Update()
    {
        // Increment time at a consistent speed
        timeOffset += Time.deltaTime * speed;
        timeInner += Time.deltaTime * speed;
        timeOuter += Time.deltaTime * speed;
        timePower += Time.deltaTime * speed;

        // Smoothly oscillate between the min and max values, with unique patterns for each
        offset = Mathf.Lerp(offsetMin, offsetMax, Mathf.PingPong(timeOffset, 1));
        inner = Mathf.Lerp(innerMin, innerMax, Mathf.PingPong(timeInner, 1));
        outer = Mathf.Lerp(outerMin, outerMax, Mathf.PingPong(timeOuter, 1));
        power = Mathf.Lerp(powerMin, powerMax, Mathf.PingPong(timePower, 1));

        // Apply the glow settings to the button text
        SetGlowEffect(offset, inner, outer, power);
    }

    void SetGlowEffect(float offset, float inner, float outer, float power)
    {
        // Apply the glow settings to the TextMeshPro material properties
        buttonText.materialForRendering.SetFloat(ShaderUtilities.ID_GlowOffset, offset);
        buttonText.materialForRendering.SetFloat(ShaderUtilities.ID_GlowInner, inner);
        buttonText.materialForRendering.SetFloat(ShaderUtilities.ID_GlowOuter, outer);
        buttonText.materialForRendering.SetFloat(ShaderUtilities.ID_GlowPower, power);
    }
}