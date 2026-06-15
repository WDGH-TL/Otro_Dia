using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    private bool hasEnemy = false;

    void Start()
    {
        StartCoroutine(EnemyTimer());
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
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);

    }
}
