using UnityEngine;
using System.Collections;
using TMPro;

public class QTE : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public Transform objetoMovil; // El GameObject vacío que se moverá
    public Transform puntoA;
    public Transform puntoB;
    public float tiempoDeViaje = 2f; // Cuánto tiempo tardará en llegar al Punto B

    [Header("Configuración de QTE")]
    public TextMeshProUGUI UI_QTE;
    public KeyCode teclaRequerida1 = KeyCode.F;
    public KeyCode teclaRequerida2 = KeyCode.G;
    public float tiempoLimite = 3f;
    public int pulsacionesNecesarias = 3;

    private int pulsacionesActuales = 0;
    public bool isQTE1_success = false;
    public bool isQTE2_success = false;

    void Start()
    {
        // Limpiamos el texto al inicio para que no diga nada mientras se mueve
        UI_QTE.text = "";

        // Iniciamos la secuencia: primero se mueve, luego activa el QTE
        StartCoroutine(MoverYActivarQTE());
    }

    private IEnumerator MoverYActivarQTE()
    {
        // Validamos que los puntos estén asignados para evitar errores
        if (objetoMovil != null && puntoA != null && puntoB != null)
        {
            float tiempoPasado = 0f;
            objetoMovil.position = puntoA.position; // Posicionamos el objeto en el inicio

            // Ciclo de movimiento basado en el tiempo
            while (tiempoPasado < tiempoDeViaje)
            {
                tiempoPasado += Time.deltaTime;
                float porcentajeCompletado = tiempoPasado / tiempoDeViaje;

                // Lerp interpola la posición entre A y B de forma fluida
                objetoMovil.position = Vector3.Lerp(puntoA.position, puntoB.position, porcentajeCompletado);

                yield return null;
            }

            // Aseguramos que termine exactamente en la posición del punto B
            objetoMovil.position = puntoB.position;
        }

        // --- Terminó el movimiento, ahora empezamos el QTE ---

        // Usamos .ToString() para que el texto se adapte automáticamente si cambias la tecla en el Inspector
        UI_QTE.text = "Presiona " + teclaRequerida1.ToString();
        StartCoroutine(RutinaQTE(teclaRequerida1));
    }

    public IEnumerator RutinaQTE(KeyCode tecla)
    {
        float tiempoRestante = tiempoLimite;
        pulsacionesActuales = 0;

        while (tiempoRestante > 0)
        {
            if (Input.GetKeyDown(tecla))
            {
                pulsacionesActuales++;

                if (pulsacionesActuales >= pulsacionesNecesarias)
                {
                    ExitoQTE();
                    yield break;
                }
            }

            tiempoRestante -= Time.deltaTime;
            yield return null;
        }

        FalloQTE();
    }

    private void ExitoQTE()
    {
        if (!isQTE1_success)
        {
            isQTE1_success = true;
            UI_QTE.text = "Presiona " + teclaRequerida2.ToString();
            StartCoroutine(RutinaQTE(teclaRequerida2));
        }
        else
        {
            isQTE2_success = true;
            UI_QTE.text = "Good job";
        }
    }

    private void FalloQTE()
    {
        UI_QTE.text = "You lose";
    }
}