using System.Collections;
using UnityEngine;
using TMPro;

public class ExtraPointMessage : MonoBehaviour
{
    // Componente de UI (TextMeshProUGUI) que mostrará el mensaje.
    public TextMeshProUGUI extraPointText;
    // Duración en segundos que se mostrará el mensaje (2 segundos).
    public float displayDuration = 2f;

    private bool messageDisplayed = false;

    // Se asume que el objeto tiene un Collider configurado como Trigger.
    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que ingresa es el jugador (con etiqueta "Player") y que el mensaje aún no se haya mostrado.
        if (!messageDisplayed && other.CompareTag("Player"))
        {
            messageDisplayed = true;
            if (extraPointText != null)
            {
                extraPointText.text = "EXTRA POINT";
                extraPointText.gameObject.SetActive(true);
                StartCoroutine(HideMessageAfterDelay());
            }
            else
            {
                Debug.LogWarning("No se asignó el componente extraPointText en el Inspector.");
            }
        }
    }

    // Corrutina que espera 2 segundos en tiempo real y luego oculta el mensaje.
    IEnumerator HideMessageAfterDelay()
    {
        yield return new WaitForSecondsRealtime(displayDuration);
        if (extraPointText != null)
        {
            extraPointText.gameObject.SetActive(false);
        }
        Destroy(gameObject);
    }
}
