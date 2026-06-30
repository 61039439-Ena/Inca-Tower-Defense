using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de niveles

public class MenuPrincipal : MonoBehaviour
{
    public void IniciarJuego()
    {
        // Pon aquí entre comillas el nombre EXACTO de tu escena del juego
        // Por ejemplo, si tu mapa se llama "SampleScene", pon ese.
        SceneManager.LoadScene("Nivel1"); 
    }
}