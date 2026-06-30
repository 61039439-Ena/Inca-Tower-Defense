using UnityEngine;

public class GeneradorEnemigos : MonoBehaviour
{
    [Header("Configuración de Enemigos")]
    public GameObject[] tiposDeEnemigos; 
    public Transform puntoSalida;         

    [Header("Configuración de Oleadas")]
    public int oleadaActual = 1;
    public int totalOleadas = 3; 
    public int enemigosPorOleada = 5; 
    public float tiempoEntreEnemigos = 2.5f;
    public float tiempoEntreOleadas = 8f; 

    [Header("UI de Victoria")]
    public GameObject panelVictoria;

    private int enemigosSpawnadosEnEstaOleada = 0;
    private float temporizadorEnemigos;
    private float temporizadorOleadas;
    private bool esperandoSiguienteOleada = false;
    private bool juegoTerminado = false;

    void Start()
    {
        temporizadorOleadas = tiempoEntreOleadas;
    }

    void Update()
    {
        if (juegoTerminado) return;

        if (enemigosSpawnadosEnEstaOleada < enemigosPorOleada)
        {
            temporizadorEnemigos += Time.deltaTime;
            if (temporizadorEnemigos >= tiempoEntreEnemigos)
            {
                SpawnearEnemigoAleatorio();
                temporizadorEnemigos = 0f;
            }
        }
        else if (!esperandoSiguienteOleada)
        {
            esperandoSiguienteOleada = true;
            temporizadorOleadas = 0f;
            Debug.Log($"¡Oleada {oleadaActual} completada! Preparando la siguiente...");
        }

        if (esperandoSiguienteOleada)
        {
            temporizadorOleadas += Time.deltaTime;
            if (temporizadorOleadas >= tiempoEntreOleadas)
            {
                AvanzarOleada();
            }
        }
    }

    void SpawnearEnemigoAleatorio()
    {
        if (tiposDeEnemigos.Length > 0 && puntoSalida != null)
        {
            int indiceAleatorio = Random.Range(0, tiposDeEnemigos.Length);
            Instantiate(tiposDeEnemigos[indiceAleatorio], puntoSalida.position, puntoSalida.rotation);
            enemigosSpawnadosEnEstaOleada++;
        }
    }

    void AvanzarOleada()
    {
        if (oleadaActual < totalOleadas)
        {
            oleadaActual++;
            enemigosSpawnadosEnEstaOleada = 0;
            enemigosPorOleada += 4; 
            esperandoSiguienteOleada = false;
            Debug.Log($"¡Inicia la Oleada {oleadaActual}!");
        }
        else
        {
            GanarJuego();
        }
    }

    void GanarJuego()
    {
        juegoTerminado = true;
        Time.timeScale = 0f; 

        if (panelVictoria != null)
        {
            panelVictoria.SetActive(true);
        }

        // ¡AQUÍ ESTÁ EL CAMBIO! Le avisamos al GameManager que active la música de triunfo
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ReproducirMusicaVictoria();
        }
    }
}