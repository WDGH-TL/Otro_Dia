using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// Esta estructura agrupa los pares de puntos y la hace visible en el Inspector
[System.Serializable]
public struct SegmentoRuta
{
    public Transform puntoInicio;
    public Transform puntoFin;
}

public class Enemy : MonoBehaviour
{
    private bool hasEnemy = false;

    [Header("Configuración de Rutas")]
    public SegmentoRuta[] segmentos; // Un arreglo con todos tus pares de puntos
    public float velocidad = 2f;
    public float tiempoPorSegmento = 5f; // El tiempo que dura la corrutina

    private int indiceActual = 0;
    private bool moviendose = false;

    public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        // Si configuramos al menos un segmento en el Inspector, iniciamos la secuencia
        if (segmentos.Length > 0)
        {
            IniciarSegmento(0);
        }
    }

    void Update()
    {
        // Solo se mueve si está activo y hay segmentos válidos
        if (moviendose && segmentos.Length > 0 && indiceActual < segmentos.Length)
        {
            Transform destinoActual = segmentos[indiceActual].puntoFin;
            if (destinoActual != null)
            {
                transform.position = Vector2.MoveTowards(transform.position, destinoActual.position, velocidad * Time.deltaTime);
            }
        }

        if (velocidad < 0)
        {
            anim.SetBool("Moviendo", true);
        }
        else
        {
            anim.SetBool("Moviendo", false);
        }
    }

    private void IniciarSegmento(int indice)
    {
        indiceActual = indice;

        // Verificamos si aún nos quedan pares de puntos por recorrer
        if (indiceActual < segmentos.Length)
        {
            Transform inicioActual = segmentos[indiceActual].puntoInicio;
            if (inicioActual != null)
            {
                transform.position = inicioActual.position; // Teletransporta al nuevo punto 1 (o 3, o 5...)
            }

            moviendose = true;
            StartCoroutine(EnemyTimer());
        }
        else
        {
            // Si ya terminó de recorrer todos los pares de puntos, se oculta
            gameObject.SetActive(false);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hasEnemy = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hasEnemy = false;
        }
    }

    public void youLoose()
    {
        if (hasEnemy == true)
        {
            SceneManager.LoadScene("Caught or Lose");
        }
    }

    public IEnumerator EnemyTimer()
    {
        // Esperamos el tiempo definido
        yield return new WaitForSeconds(tiempoPorSegmento);

        moviendose = false;

        // Una vez terminada la corrutina, pasamos al siguiente par de puntos
        IniciarSegmento(indiceActual + 1);
    }
}