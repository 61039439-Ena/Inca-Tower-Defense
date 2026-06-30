using UnityEngine;

public class Proyectil : MonoBehaviour
{
    private Transform objetivo;
    public float velocidad = 7f;
    public int danio = 1; // Cuánta vida le restará al enemigo

    // La torre usará esta función para decirle al proyectil a quién perseguir
    public void BuscarObjetivo(Transform _objetivo)
    {
        objetivo = _objetivo;
    }

    void Update()
    {
        // Si el enemigo murió antes de que el proyectil llegara, destruimos el proyectil
        if (objetivo == null)
        {
            Destroy(gameObject);
            return;
        }

        // Moverse en dirección al enemigo
        Vector3 direccion = objetivo.position - transform.position;
        float distanciaEstaFrame = velocidad * Time.deltaTime;

        // Si estamos lo suficientemente cerca, impactamos
        if (direccion.magnitude <= distanciaEstaFrame)
        {
            ImpactarObjetivo();
            return;
        }

        transform.Translate(direccion.normalized * distanciaEstaFrame, Space.World);
    }

    void ImpactarObjetivo()
    {
        // Intentamos obtener el script de vida del enemigo
        VidaEnemigo vida = objetivo.GetComponent<VidaEnemigo>();
        if (vida != null)
        {
            vida.RecibirDanio(danio);
        }

        // Destruimos el proyectil tras el impacto
        Destroy(gameObject);
    }
}