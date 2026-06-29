using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public Textos[] todasLasPlantillas;

    [Header("Referencias UI")]
    public TextMeshProUGUI nombre;
    public TextMeshProUGUI textoNarracion;

    public Image imgPersonaje;

    public GameObject[] botonesUI;

    public GameObject btnNext;
    public GameObject btnChangeScene;

    private Textos plantillaActual;

    void Start()
    {
        btnChangeScene.gameObject.SetActive(false);
        btnNext.gameObject.SetActive(false);
        MostrarTexto(todasLasPlantillas[0]);
    }

    public void MostrarTexto(Textos nuevaPlantilla)
    {
        plantillaActual = nuevaPlantilla;
        nombre.text = plantillaActual.nombre;
        textoNarracion.text = plantillaActual.textoNarrativo;

        if (imgPersonaje != null)
        {
            
            if (plantillaActual.characterSprite != null)
            {
                imgPersonaje.sprite = plantillaActual.characterSprite;
                imgPersonaje.gameObject.SetActive(true);
            }
            else
            {
                imgPersonaje.gameObject.SetActive(false);
            }
        }

        if (plantillaActual.esFinal || plantillaActual.opciones.Length == 0)
        {
            foreach (GameObject boton in botonesUI)
            {
                boton.SetActive(false);
            }
        }
        else
        {
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

                         Button botonComponente = botonesUI[i].GetComponent<Button>();
                        if (botonComponente != null)
                        {
                            botonComponente.onClick.RemoveAllListeners(); 
                            int indiceCapturado = i; 
                            botonComponente.onClick.AddListener(() => ControlDeBotones(indiceCapturado));
                        }
                    }
                    else
                    {
                        botonesUI[i].gameObject.SetActive(false);
                    }
                }
            }
        }

        if (!plantillaActual.hasDestinoNext)
        {
            if (plantillaActual.isDecision)
            {
                btnNext.gameObject.SetActive(false);
                btnChangeScene.gameObject.SetActive(false);
            }
            else
            {
                btnNext.gameObject.SetActive(false);
                btnChangeScene.gameObject.SetActive(true);
            }
        }
        else
        {
            btnNext.gameObject.SetActive(true);
            btnChangeScene.gameObject.SetActive(false);
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
        else
        {
            if (plantillaActual.hasDestinoNext)
            {
                int indiceNext = plantillaActual.indiceDestinoNext;
                if (indiceNext >= 0 && indiceNext < todasLasPlantillas.Length)
                {
                    MostrarTexto(todasLasPlantillas[indiceNext]);
                }
            }
        }
    }
}