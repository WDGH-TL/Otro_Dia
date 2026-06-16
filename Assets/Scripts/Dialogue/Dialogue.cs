using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public Textos[] todasLasPlantillas;

    [Header("Referencias UI")]
    public TextMeshProUGUI nombre;
    public TextMeshProUGUI textoNarracion;
    
    public GameObject[] botonesUI;

    public GameObject btnNext;

    private Textos plantillaActual;

    void Start()
    {
        MostrarTexto(todasLasPlantillas[0]);
    }

    public void MostrarTexto(Textos nuevaPlantilla)
    {
        plantillaActual = nuevaPlantilla;
        nombre.text = plantillaActual.nombre;
        textoNarracion.text = plantillaActual.textoNarrativo;
       // Debug.Log("plantilla: " + plantillaActual.opciones);

        if (plantillaActual.esFinal || plantillaActual.opciones.Length == 0)
        {
            foreach (GameObject boton in botonesUI)
            {
                boton.SetActive(false);
            }
            return;
        }

        for (int i = 0; i < botonesUI.Length; i++)
        {

            if (botonesUI[i] != null)
            {
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
                    botonesUI[i].gameObject.SetActive(false);
                }
            }
        }
    }

    public void ControlDeBotones(int indiceBoton)
    {
       
        if (indiceBoton >= 0 && indiceBoton < plantillaActual.opciones.Length)
        {
            btnNext.gameObject.SetActive(false);
            int indiceSiguiente = plantillaActual.opciones[indiceBoton].indiceDestino;

            if (indiceSiguiente >= 0 && indiceSiguiente < todasLasPlantillas.Length)
            {
                MostrarTexto(todasLasPlantillas[indiceSiguiente]);
            }
           
        }
        else {
            int indiceNext = plantillaActual.indiceDestinoNext;
            btnNext.gameObject.SetActive(true);
            MostrarTexto(todasLasPlantillas[indiceNext]);
        }
    }
}