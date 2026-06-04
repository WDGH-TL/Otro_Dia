using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public Textos[] todasLasPlantillas;

    [Header("Referencias UI")]
    public TextMeshProUGUI textoNarracion;
    //public Button[] botonesUI; // Arrastra aquí tus botones de la UI

    public GameObject[] botonesUI;

    private Textos plantillaActual;

    void Start()
    {
        MostrarTexto(todasLasPlantillas[0]);
    }

    public void MostrarTexto(Textos nuevaPlantilla)
    {
        plantillaActual = nuevaPlantilla;
        textoNarracion.text = plantillaActual.textoNarrativo;
        Debug.Log("plantilla: " + plantillaActual.opciones);

        // 1. Apagamos botones si es final O si la lista de opciones está vacía (size 0)
        if (plantillaActual.esFinal || plantillaActual.opciones.Length == 0)
        {
            foreach (GameObject boton in botonesUI)
            {
                boton.SetActive(false);
            }
            return;
        }

        // 2. Si no es final y hay opciones, configuramos solo lo que necesitemos
        for (int i = 0; i < botonesUI.Length; i++)
        {

            if (botonesUI[i] != null)
            {
                // ¿El botón existe en la UI y tenemos una opción en el SO para este índice?
                if (i < plantillaActual.opciones.Length)
                {
                    botonesUI[i].gameObject.SetActive(true);
                    TextMeshProUGUI textoHijo = botonesUI[i].GetComponentInChildren<TextMeshProUGUI>();
                    if (textoHijo != null)
                    {
                        textoHijo.text = plantillaActual.opciones[i].textoOpcion;
                    }
                }
                else
                {
                    // No tenemos opción para este botón, lo apagamos
                    botonesUI[i].gameObject.SetActive(false);
                }
            }
        }
    }

    public void ControlDeBotones(int indiceBoton)
    {
       
        if (indiceBoton >= 0 && indiceBoton < plantillaActual.opciones.Length)
        {
            int indiceSiguiente = plantillaActual.opciones[indiceBoton].indiceDestino;

            if (indiceSiguiente >= 0 && indiceSiguiente < todasLasPlantillas.Length)
            {
                MostrarTexto(todasLasPlantillas[indiceSiguiente]);
            }
           
        }
        
    }
}