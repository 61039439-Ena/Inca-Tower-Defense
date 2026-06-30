using UnityEngine;

public class SelectorConstruccion : MonoBehaviour
{
    [Header("Configuración de Torres")]
    public GameObject[] prefabsDeTorres; // Lista de torres disponibles
    public int[] costosDeTorres;         // Costo de cada torre en el mismo orden
    public LayerMask capaPlataformas;

    private int indiceTorreSeleccionada = 0; // Por defecto empieza con la primera torre (Tecla 1)
    private Camera camaraPrincipal;

    void Start()
    {
        camaraPrincipal = Camera.main;
    }

    void Update()
    {
        // 1. SELECCIÓN DE TORRE CON EL TECLADO
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            indiceTorreSeleccionada = 0;
            Debug.Log("¡Has seleccionado: Torre Común!");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // Solo si tenemos configurada la segunda torre
            if (prefabsDeTorres.Length > 1)
            {
                indiceTorreSeleccionada = 1;
                Debug.Log("¡Has seleccionado: Hondero Inca!");
            }
        }

        // 2. CONSTRUCCIÓN CON CLIC IZQUIERDO
        if (Input.GetMouseButtonDown(0))
        {
            // Validamos que el índice sea correcto dentro de la lista
            if (prefabsDeTorres == null || prefabsDeTorres.Length == 0 || indiceTorreSeleccionada >= prefabsDeTorres.Length) return;

            Ray rayo = camaraPrincipal.ScreenPointToRay(Input.mousePosition);
            RaycastHit golpe;

            if (Physics.Raycast(rayo, out golpe, Mathf.Infinity, capaPlataformas))
            {
                if (golpe.collider.CompareTag("Plataforma"))
                {
                    int costoActual = costosDeTorres[indiceTorreSeleccionada];

                    if (GameManager.Instance != null && GameManager.Instance.oro >= costoActual)
                    {
                        GameManager.Instance.oro -= costoActual;

                        Vector3 posicionConstruccion = golpe.transform.position + new Vector3(0, 0.5f, 0);
                        
                        // Construimos la torre que esté actualmente seleccionada
                        Instantiate(prefabsDeTorres[indiceTorreSeleccionada], posicionConstruccion, Quaternion.identity);

                        golpe.collider.enabled = false; 
                    }
                    else
                    {
                        Debug.Log("¡No tienes suficiente Oro para esta torre!");
                    }
                }
            }
        }
    }
}