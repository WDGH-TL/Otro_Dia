using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerLess : MonoBehaviour
{
    public TextMeshProUGUI contador;
    public int minutes;
    public float seconds;
    public float secondsLimit;

   
    public bool tiempoAgotado;
    void Start()
    {
        actualizar();
    }

    void Update()
    {
        if (tiempoAgotado)
        {
            contador.text = "00:00";
            return;
        }

        seconds -= Time.deltaTime;
        if (seconds <= 0)
        {
            if (minutes == 0) {
                timerTrigger();
            }
            else
            {
                seconds = 60;
                minutes -= 1;
            }
           
        }
        actualizar();
    }

    void actualizar()
    {
        if (seconds < 9.5f)
        {
            contador.text = "0" + minutes.ToString() + ":0" + seconds.ToString("f0");
        }
        else
        {
            if (minutes > 60)
            {
                contador.text = minutes.ToString() + ":" + seconds.ToString("f0");
            }
            else
            {
                contador.text = "0" + minutes.ToString() + ":" + seconds.ToString("f0");
            }

        }

    }

    void timerTrigger()
    {
        tiempoAgotado = true;
        // messageFail.SetActive(true);
        SceneManager.LoadScene("Hiding Result One");
    }
}
