using UnityEngine;

public class ActivarParticulasDisparo : MonoBehaviour
{
    [Header("Efectos de Partículas")]
    public ParticleSystem[] efectosDisparo; // Array de dos efectos de partículas (uno por cañón)

    /// <summary>
    /// Activa los efectos de partículas de los cañones.
    /// </summary>
    public void ActivarEfectoDisparo()
    {
        foreach (ParticleSystem efecto in efectosDisparo)
        {
            if (efecto != null)
            {
                efecto.Play();
            }
        }
    }
}

