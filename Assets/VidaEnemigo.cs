using UnityEngine;

public class VidaEnemigo : MonoBehaviour
{
    public int vidaMaxima = 3;
    private int vidaActual;

    void Start()
    {
        vidaActual = vidaMaxima;
    }

    public void RecibirDanio(int cantidad)
    {
        vidaActual -= cantidad;

        // Si la vida llega a 0, el enemigo muere
        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        // ¡NUEVO! Le sumamos oro al jugador al eliminar un enemigo
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SumarOro(20); // Te da 20 de oro por cada español derrotado
        }

        Destroy(gameObject);
    }
}