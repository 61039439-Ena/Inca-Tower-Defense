using UnityEngine;
using System.Collections;

public class SpawnerEnemigos : MonoBehaviour
{
    [Header("Configuración del Spawner")]
    public GameObject enemigoPrefab;     // Aquí arrastraremos la plantilla del enemigo
    public float tiempoEntreEnemigos = 2f; // Cada cuántos segundos sale un enemigo

    void Start()
    {
        // Iniciamos una Corrutina (un temporizador) para generar enemigos
        StartCoroutine(GenerarEnemigos());
    }

    IEnumerator GenerarEnemigos()
    {
        // Este ciclo se repetirá para siempre durante la partida
        while (true)
        {
            // Clonar el prefab del enemigo en la posición de este Spawner
            Instantiate(enemigoPrefab, transform.position, Quaternion.identity);

            // Esperar el tiempo configurado antes de volver a clonar otro
            yield return new WaitForSeconds(tiempoEntreEnemigos);
        }
    }
}