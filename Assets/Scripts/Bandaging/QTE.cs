using UnityEngine;
using System.Collections;
using TMPro;

public class QTE : MonoBehaviour
{
    public SceneChanger sceneChanger;

    public Transform objetoMovil;
    public Transform puntoA;
    public Transform puntoB;
    public float tiempoDeViaje = 2f;

    public TextMeshProUGUI UI_QTE;
    public KeyCode teclaRequerida1 = KeyCode.F;
    public KeyCode teclaRequerida2 = KeyCode.G;

    public bool isQTE1_success = false;
    public bool isQTE2_success = false;

    private int viajeActual = 1;
    private bool estaEnVentanaQTE = false;
    private bool yaPresionoEnEsteViaje = false;

    void Start()
    {
        UI_QTE.text = "";

        StartCoroutine(CicloDeMovimientoContinuo());
    }

    private IEnumerator CicloDeMovimientoContinuo()
    {
        viajeActual = 1;
        estaEnVentanaQTE = false;
        yaPresionoEnEsteViaje = false;

        yield return StartCoroutine(EjecutarViaje());

        viajeActual = 2;
        estaEnVentanaQTE = false;
        yaPresionoEnEsteViaje = false;

        yield return StartCoroutine(EjecutarViaje());

        FinalizarMinijuego();
    }

    private IEnumerator EjecutarViaje()
    {
        float tiempoPasado = 0f;
        objetoMovil.position = puntoA.position;

        while (tiempoPasado < tiempoDeViaje)
        {
            if (isQTE1_success && viajeActual == 1) break;
            if (isQTE2_success && viajeActual == 2) break;

            tiempoPasado += Time.deltaTime;
            float porcentaje = tiempoPasado / tiempoDeViaje;
            objetoMovil.position = Vector3.Lerp(puntoA.position, puntoB.position, porcentaje);

            if (porcentaje >= 0.9f)
            {
                estaEnVentanaQTE = true;
                if (viajeActual == 1 && !yaPresionoEnEsteViaje) UI_QTE.text = "¡Presiona " + teclaRequerida1.ToString() + "!";
                if (viajeActual == 2 && !yaPresionoEnEsteViaje) UI_QTE.text = "¡Presiona " + teclaRequerida2.ToString() + "!";
            }

            yield return null;
        }

        objetoMovil.position = puntoB.position;
    }

    void Update()
    {
        if (viajeActual == 1)
        {
            UI_QTE.text = "¡Presiona " + teclaRequerida1.ToString() + "!";
            if (Input.GetKeyDown(teclaRequerida1) && !yaPresionoEnEsteViaje)
            {
                yaPresionoEnEsteViaje = true;
                if (estaEnVentanaQTE)
                {
                    isQTE1_success = true;
                    UI_QTE.text = "¡Presiona " + teclaRequerida2.ToString() + "!";
                }
                else
                {
                    UI_QTE.text = "¡Presiona " + teclaRequerida2.ToString() + "!";
                }
            }
        }
        else if (viajeActual == 2)
        {
            if (Input.GetKeyDown(teclaRequerida2) && !yaPresionoEnEsteViaje)
            {
                yaPresionoEnEsteViaje = true;
                if (estaEnVentanaQTE)
                {
                    isQTE2_success = true;
                    UI_QTE.text = "¡G ACEPTADA!";
                }
                else
                {
                    UI_QTE.text = "¡Presiona " + teclaRequerida2.ToString() + "!";
                }
            }
        }
    }

    private void FinalizarMinijuego()
    {
        if (isQTE2_success)
        {
            UI_QTE.text = "Good job";
            sceneChanger.BandagindDone();
        }
        else
        {
            UI_QTE.text = "You lose";
            sceneChanger.BandagindDone();
        }
    }
}