using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosaoController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            BombManController bombMan =
           collision.GetComponent<BombManController>();
            bombMan.Morrer();
        }
        if (collision.tag == "Obstaculo")
        {
            Destroy(collision.gameObject);
            //Destroy(gameObject);
        }
        if (collision.tag == "Bomba")
        {
            BombaController bomba = collision.GetComponent<BombaController>
           ();
            bomba.Explodir();
        }
    }
}