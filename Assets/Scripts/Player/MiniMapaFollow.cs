using UnityEngine;

public class MiniMapFollow : MonoBehaviour
{
    public Transform player; //Arrastra el Player aquí en el Inspector
    public Vector3 offset = new Vector3(0, 20, 0); //Altura de la cámara del Mini Mapa

    void LateUpdate()
    {
        if (player != null)
        {
            transform.position = player.position + offset; //Sigue al jugador
        }
    }
}

