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

    [Header("Configuración de Sonido")]
    public AudioSource fuenteAudio; // El componente que reproduce el sonido
    public AudioClip sonidoCaminar;  // El archivo de sonido de los pasos

    [Header("Configuración de Aspecto")]
    public Sprite spriteNuevaZona; // El nuevo sprite para cuando cambie de zona
    [Tooltip("El número de segmento a partir del cual cambiará el sprite (Empezando desde 0)")]
    public int segmentoParaCambiarSprite = 2; // Ejemplo: cambia al empezar el segmento 2

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
            anim.SetBool("Moviendo", false);

            // Si el enemigo no se mueve, detenemos el sonido
            if (fuenteAudio != null && fuenteAudio.isPlaying)
            {
                fuenteAudio.Stop();
            }
        }
        else
        {
            anim.SetBool("Moviendo", true);

            // Si se está moviendo y el sonido NO está sonando, lo reproducimos en bucle
            if (fuenteAudio != null && sonidoCaminar != null && !fuenteAudio.isPlaying)
            {
                fuenteAudio.clip = sonidoCaminar;
                fuenteAudio.loop = true; // Hace que el sonido se repita constantemente
                fuenteAudio.Play();
            }
        }
    }

    private void IniciarSegmento(int indice)
    {
        indiceActual = indice;

        // NUEVO: Comprobamos si ya alcanzamos o superamos el segmento indicado para cambiar el sprite
        if (indiceActual >= segmentoParaCambiarSprite)
        {
            CambiarAspectoNuevaZona();
        }

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

            // Detenemos el sonido si el objeto se desactiva
            if (fuenteAudio != null && fuenteAudio.isPlaying)
            {
                fuenteAudio.Stop();
            }
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

    // Cambia el aspecto del sprite usando el SpriteRenderer
    public void CambiarAspectoNuevaZona()
    {
        SpriteRenderer renderizador = GetComponent<SpriteRenderer>();
        if (renderizador != null && spriteNuevaZona != null)
        {
            renderizador.sprite = spriteNuevaZona;
        }
    }
}