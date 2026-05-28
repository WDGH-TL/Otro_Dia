using UnityEngine;
using System.Collections;
using TMPro;

public class QTE : MonoBehaviour
{
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
            UI_QTE.text = "Presiona G";
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
        UI_QTE.text = "You loose";
       
    }
}
