using UnityEngine;

public class showBtn : MonoBehaviour
{
    
    public GameObject botonUI;


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            botonUI.SetActive(true);
        }

    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            botonUI.SetActive(false);
        }

    }
}
