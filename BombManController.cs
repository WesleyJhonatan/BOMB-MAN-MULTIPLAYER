using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombManController : MonoBehaviour
{
    public int numeroPlayer;
    public int qtdBomba;
    public int contaBomba;
    public GameObject bombaPrefab;
    public float speed;
    public float tempoExplosao;
    bool dentroBomba = false;
    Animator animator;
    SpriteRenderer spriteRenderer;
    string horizontal, vertical, fire;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        horizontal = "HorizontalP" + numeroPlayer;
        vertical = "VerticalP" + numeroPlayer;
        fire = "Fire1P" + numeroPlayer;
    }
    // Update is called once per frame
    void Update()
    {
        float xAxis = Input.GetAxis(horizontal);
        float yAxis = Input.GetAxis(vertical);
        transform.Translate(new Vector2(xAxis, yAxis)
        * speed * Time.deltaTime);
        if (xAxis > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (xAxis < 0)
        {
            spriteRenderer.flipX = true;
        }
        if (Mathf.Abs(xAxis) > 0 ||
        Mathf.Abs(yAxis) > 0)
        {
            animator.SetBool("walk", true);
        }
        else
        {

            animator.SetBool("walk", false);
        }
        if (Input.GetButtonDown(fire))
        {
            SpawnBomba();
        }
    }
    public void SpawnBomba()
    {
        if (contaBomba < qtdBomba)
        {
            float posX = Mathf.Round(transform.position.x * 2) / 2;
            float posY = (Mathf.Round(transform.position.y * 2) / 2) + 0.5f;
            if (posX % 1 == 0)
            {
                posX += 0.5f;
            }
            if (posY % 1 == 0)
            {
                posY += 0.5f;
            }
            Vector2 newPos = new Vector2(posX, posY);
            GameObject GO = Instantiate(bombaPrefab, newPos,
           Quaternion.identity);
            BombaController bomba = GO.GetComponent<BombaController>();
            bomba.bombMan = this;
            bomba.tempoExplosao = tempoExplosao;
            contaBomba++;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bomba")
        {
            dentroBomba = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Bomba")
        {
            dentroBomba = false;
        }
    }
    public void Morrer()
    {
        animator.SetTrigger("dead");
        gameObject.layer = 9;
        qtdBomba = 0;
    }
}