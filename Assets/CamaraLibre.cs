using UnityEngine;

public class CamaraLibre : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 15f;
    public float velocidadVertical = 10f; // Velocidad para subir y bajar

    [Header("Rotación (Mouse)")]
    public float sensibilidadMouse = 2f;
    
    private float rotacionX = 0f;
    private float rotacionY = 0f;

    void Start()
    {
        Vector3 rotacionInicial = transform.localEulerAngles;
        rotacionX = rotacionInicial.y;
        rotacionY = rotacionInicial.x;
    }

    void Update()
    {
        // 1. MOVIMIENTO HORIZONTAL (Teclas W, A, S, D)
        float moverH = Input.GetAxis("Horizontal"); 
        float moverV = Input.GetAxis("Vertical");   

        Vector3 haciaAdelante = transform.forward;
        Vector3 haciaLados = transform.right;

        haciaAdelante.y = 0;
        haciaLados.y = 0;
        
        haciaAdelante.Normalize();
        haciaLados.Normalize();

        Vector3 direccionFinal = (haciaAdelante * moverV) + (haciaLados * moverH);
        transform.position += direccionFinal * velocidad * Time.deltaTime;


        // 2. NUEVO: MOVIMIENTO VERTICAL (Teclas E para Subir, Q para Bajar)
        float movimientoVertical = 0f;

        if (Input.GetKey(KeyCode.E))
        {
            movimientoVertical = 1f; // Sube
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            movimientoVertical = -1f; // Baja
        }

        // Aplicamos el movimiento directamente en el eje Y (hacia arriba o abajo en el mundo)
        transform.position += Vector3.up * movimientoVertical * velocidadVertical * Time.deltaTime;


        // 3. ROTACIÓN (Clic Derecho presionado)
        if (Input.GetMouseButton(1))
        {
            rotacionX += Input.GetAxis("Mouse X") * sensibilidadMouse;
            rotacionY -= Input.GetAxis("Mouse Y") * sensibilidadMouse;

            rotacionY = Mathf.Clamp(rotacionY, 10f, 85f);

            transform.localRotation = Quaternion.Euler(rotacionY, rotacionX, 0f);
        }
    }
}