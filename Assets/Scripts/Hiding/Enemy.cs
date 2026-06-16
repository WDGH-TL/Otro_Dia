using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    private bool hasEnemy = false;

    public Transform punto1;
    public Transform punto2;
    public float velocidad = 2f;

    void Start()
    {
        if (punto1 != null)
        {
            transform.position = punto1.position;
        }
        StartCoroutine(EnemyTimer());
    }

    void Update()
    {
       
        if (punto2 != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, punto2.position, velocidad * Time.deltaTime);
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
        yield return new WaitForSeconds(8f);
        gameObject.SetActive(false);

    }
}
