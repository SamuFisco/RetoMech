using UnityEngine;

public class PersistentParticles : MonoBehaviour
{
    private ParticleSystem ps;

    void Awake()
    {
        ps = GetComponent<ParticleSystem>();

        // Asegurar que las partículas están desactivadas al inicio
        if (ps != null)
        {
            ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }

    public void ActivarParticulas()
    {
        if (ps != null)
        {
            ps.Play(); // Activar partículas
        }
    }
}
