using UnityEngine;
using TMPro; // Necesario para controlar TextMeshPro

public class ControladorUI : MonoBehaviour
{
    [Header("Componentes de Texto")]
    public TextMeshProUGUI textoOro;
    public TextMeshProUGUI textoVidas;
    public TextMeshProUGUI textoOleadas; // ¡NUEVA! Casilla para el texto de rondas

    private GeneradorEnemigos generador;

    void Start()
    {
        // Buscamos automáticamente el componente del generador que está en la escena
        generador = Object.FindFirstObjectByType<GeneradorEnemigos>();
    }

    void Update()
    {
        // Actualizamos el oro y las vidas leyendo el GameManager
        if (GameManager.Instance != null)
        {
            if (textoOro != null) textoOro.text = "Oro: " + GameManager.Instance.oro;
            if (textoVidas != null) textoVidas.text = "Vidas: " + GameManager.Instance.vidas;
        }

        // ¡EL TRUCO! Actualizamos el texto de las rondas en tiempo real
        if (generador != null && textoOleadas != null)
        {
            textoOleadas.text = "Oleada: " + generador.oleadaActual + " / " + generador.totalOleadas;
        }
    }
}