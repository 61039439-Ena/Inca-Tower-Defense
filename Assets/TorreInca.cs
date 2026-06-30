using UnityEngine;
using System.Collections.Generic;

public class TorreInca : MonoBehaviour
{
    [Header("Configuración de Ataque")]
    public float tiempoEntreDisparos = 1f;
    public GameObject proyectilPrefab; // Para arrastrar el prefab de la esfera amarilla
    public Transform puntoDisparo;    // Lugar desde donde saldrá la bala
    private float temporizadorDisparo;

    // Lista para registrar qué enemigos están dentro del rango
    private List<GameObject> enemigosEnRango = new List<GameObject>();
    private GameObject enemigoObjetivo;

    void Update()
    {
        // Limpiar la lista de enemigos que hayan sido destruidos fuera del Trigger
        enemigosEnRango.RemoveAll(item => item == null);

        // Si tenemos un objetivo asignado
        if (enemigoObjetivo != null)
        {
            // Hacer que la torre "mire" (rote) hacia el enemigo en el eje Y
            Vector3 direccion = enemigoObjetivo.transform.position - transform.position;
            direccion.y = 0; // Evita que la torre se incline hacia arriba o abajo
            if (direccion != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direccion);
            }

            // Temporizador para disparar
            temporizadorDisparo += Time.deltaTime;
            if (temporizadorDisparo >= tiempoEntreDisparos)
            {
                Disparar();
                temporizadorDisparo = 0f;
            }
        }
        else
        {
            // Si el objetivo actual se fue o murió, buscar el siguiente de la lista
            AsignarSiguienteObjetivo();
        }
    }

    void Disparar()
    {
        // Este mensaje nos dirá si la torre al menos INTENTA disparar
        Debug.Log("¡La función Disparar() se está ejecutando!");

        Transform origen = puntoDisparo != null ? puntoDisparo : transform;
        GameObject proyectilGO = Instantiate(proyectilPrefab, origen.position, transform.rotation);
        
        Proyectil proyectil = proyectilGO.GetComponent<Proyectil>();
        if (proyectil != null)
        {
            proyectil.BuscarObjetivo(enemigoObjetivo.transform);
        }
    }

    void AsignarSiguienteObjetivo()
    {
        if (enemigosEnRango.Count > 0)
        {
            enemigoObjetivo = enemigosEnRango[0];
        }
        else
        {
            enemigoObjetivo = null;
        }
    }

    // Detecta cuando alguien ENTRA en la esfera invisible
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemigosEnRango.Add(other.gameObject);
        }
    }

    // Detecta cuando alguien SALE de la esfera invisible
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemigosEnRango.Remove(other.gameObject);
            if (enemigoObjetivo == other.gameObject)
            {
                enemigoObjetivo = null;
            }
        }
    }
}