using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Atributos del Jugador")]
    public int vidas = 10;
    public int oro = 100;

    [Header("UI de Fin de Juego")]
    public GameObject panelGameOver; 

    [Header("Audio del Juego")]
    public AudioSource musicaFondo;       
    public AudioClip sonidoVictoria;      
    public AudioClip sonidoDerrota;       // ¡NUEVA! Casilla para el audio de perder

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void RestarVida(int cantidad)
    {
        vidas -= cantidad; 

        if (vidas <= 0)
        {
            vidas = 0;
            DispararGameOver();
        }
    }

    void DispararGameOver()
    {
        Time.timeScale = 0f;
        if (panelGameOver != null)
        {
            panelGameOver.SetActive(true);
        }

        // ¡EL TRUCO DE DERROTA! 
        // 1. Apagamos la música alegre de fondo
        if (musicaFondo != null)
        {
            musicaFondo.Stop();
        }

        // 2. Reproducemos el sonido triste de perder
        if (musicaFondo != null && sonidoDerrota != null)
        {
            musicaFondo.PlayOneShot(sonidoDerrota);
        }
    }

    public void ReproducirMusicaVictoria()
    {
        if (musicaFondo != null)
        {
            musicaFondo.Stop();
        }

        if (musicaFondo != null && sonidoVictoria != null)
        {
            musicaFondo.PlayOneShot(sonidoVictoria);
        }
    }

    public void ReiniciarPartida()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AvanzarSiguienteNivel()
    {
        // Despausamos el tiempo del juego para el nuevo nivel
        Time.timeScale = 1f; 

        // Obtenemos el número (índice) del nivel en el que estamos jugando ahora mismo
        int nivelActual = SceneManager.GetActiveScene().buildIndex;

        // Si el siguiente número de nivel existe en nuestra lista de Build Settings...
        if (nivelActual + 1 < SceneManager.sceneCountInBuildSettings)
        {
            // ¡Cargamos el siguiente nivel! (Por ejemplo, del Nivel 1 pasa al Nivel 2)
            SceneManager.LoadScene(nivelActual + 1);
        }
        else
        {
            // Si ya no hay más niveles (es decir, ganamos el Nivel 4), regresamos al menú
            SceneManager.LoadScene("MenuPrincipal");
        }
    }

    public void SumarOro(int cantidad)
    {
        oro += cantidad;
    }
}