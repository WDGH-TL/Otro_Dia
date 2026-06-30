using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct SegmentoRuta
{
    public Transform puntoInicio;
    public Transform puntoFin;
}

public class Enemy : MonoBehaviour
{
    private bool hasEnemy = false;

    [Header("Configuracin de Rutas")]
    public SegmentoRuta[] segmentos;
    public float velocidad = 2f;
    public float tiempoPorSegmento = 5f;

    private int indiceActual = 0;
    private bool moviendose = false;

    public Animator anim;

    [Header("Configuración de Sprites")]
    public SpriteRenderer spriteRenderer;
    public Sprite spriteNormal;
    public Sprite spriteEspecial;

    void Start()
    {
        anim = GetComponent<Animator>();

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (segmentos.Length > 0)
        {
            IniciarSegmento(0);
        }
    }

    void Update()
    {
        if (moviendose && segmentos.Length > 0 && indiceActual < segmentos.Length)
        {
            Transform destinoActual = segmentos[indiceActual].puntoFin;
            if (destinoActual != null)
            {
                transform.position = Vector2.MoveTowards(transform.position, destinoActual.position, velocidad * Time.deltaTime);
            }
        }

        if (anim != null && anim.enabled)
        {
            if (velocidad < 0)
            {
                anim.SetBool("Moviendo", true);
            }
            else
            {
                anim.SetBool("Moviendo", false);
            }
        }
    }

    private void IniciarSegmento(int indice)
    {
        indiceActual = indice;

        if (indiceActual < segmentos.Length)
        {
            if (spriteRenderer != null)
            {
                if (indiceActual == 2 || indiceActual == 3)
                {
                    if (anim != null) anim.enabled = false;
                    spriteRenderer.sprite = spriteEspecial;
                }
                else
                {
                    if (anim != null) anim.enabled = true;
                    spriteRenderer.sprite = spriteNormal;
                }
            }

            Transform inicioActual = segmentos[indiceActual].puntoInicio;
            if (inicioActual != null)
            {
                transform.position = inicioActual.position;
            }

            moviendose = true;
            StartCoroutine(EnemyTimer());
        }
        else
        {
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
        yield return new WaitForSeconds(tiempoPorSegmento);

        moviendose = false;

        IniciarSegmento(indiceActual + 1);
    }
}