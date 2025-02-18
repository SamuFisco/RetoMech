using UnityEngine;

public class ActivarParticulasDisparo : MonoBehaviour
{
    [Header("Efectos de Part�culas")]
    public ParticleSystem[] efectosDisparo; // Array de dos efectos de part�culas (uno por ca��n)

    /// <summary>
    /// Activa los efectos de part�culas de los ca�ones.
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

