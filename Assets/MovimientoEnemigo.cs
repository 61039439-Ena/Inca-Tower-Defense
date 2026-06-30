using UnityEngine;

public class MovimientoEnemigo : MonoBehaviour
{
    [Header("Configuración de Ruta")]
    public Transform[] puntosDeControl; // Ya no necesitaremos arrastrar esto a mano
    public float velocidad = 3f;        

    private int indiceActual = 0;       

    void Start()
    {
        // 1. Buscamos el objeto "Ruta" que está en la escena
        GameObject objetoRuta = GameObject.Find("Ruta");

        if (objetoRuta != null)
        {
            // 2. Contamos cuántos hijos (waypoints) tiene dentro
            int cantidadPuntos = objetoRuta.transform.childCount;
            puntosDeControl = new Transform[cantidadPuntos];

            // 3. Los guardamos automáticamente en la lista
            for (int i = 0; i < cantidadPuntos; i++)
            {
                puntosDeControl[i] = objetoRuta.transform.GetChild(i);
            }
        }
        else
        {
            Debug.LogError("¡Ojo! No encontré ningún objeto llamado 'Ruta' en la Hierarchy.");
        }

        // Coloca al enemigo en el primer punto
        if (puntosDeControl != null && puntosDeControl.Length > 0)
        {
            transform.position = puntosDeControl[0].position;
        }
    }

    void Update()
    {
        if (puntosDeControl == null || puntosDeControl.Length == 0) return;

        Transform objetivo = puntosDeControl[indiceActual];
        transform.position = Vector3.MoveTowards(transform.position, objetivo.position, velocidad * Time.deltaTime);

        if (Vector3.Distance(transform.position, objetivo.position) < 0.1f)
        {
            indiceActual++;

            // Si llegamos al final del camino...
            if (indiceActual >= puntosDeControl.Length)
            {
                // ¡NUEVO! Le avisamos al GameManager que reste una vida
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.RestarVida(1);
                }

                // Se destruye el enemigo
                Destroy(gameObject);
            }
        }
    }
}