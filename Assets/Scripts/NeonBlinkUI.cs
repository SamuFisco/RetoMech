using UnityEngine;
using UnityEngine.UI;

public class NeonBlinkUI : MonoBehaviour
{
    public Image neonImage;  // Asigna el Image UI
    public float blinkDuration = 0.5f; // Tiempo entre parpadeos

    void Start()
    {
        StartBlinking();
    }

    void StartBlinking()
    {
        LeanTween.alpha(neonImage.rectTransform, 0f, blinkDuration).setLoopPingPong().setEase(LeanTweenType.easeInOutSine);
    }
}
