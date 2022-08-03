using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombaController : MonoBehaviour
{
    public BombManController bombMan;
    public GameObject prefabExplosao;
    public float tempoExplosao;
    public int powerUp = 3;
    bool explodiu = false;
    float contador;
    Collider2D collider;
    SpriteRenderer renderer;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
        renderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (explodiu == false) {
            contador += Time.deltaTime;
            renderer.color = new Color(1, 1 - (contador/tempoExplosao), 1 - (contador/tempoExplosao));
        }
        if (contador >= tempoExplosao && explodiu == false) {
            Explodir();
        }
        if (explodiu == true) {
            if (!audioSource.isPlaying)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            collider.isTrigger = false;
        }
    }

    public void SpawnExplosion(Vector3 direction) {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction * powerUp, 
            powerUp, LayerMask.GetMask("Default"));
        Debug.DrawRay(transform.position, direction * powerUp, Color.red, 3);
        if (hit.collider != null)
        {
            for (float i = 0.5f; i < powerUp; i += 1)
            {
                if (i > hit.distance)
                {
                    return;
                }
                Vector2 newPos = GetDirecaoExplosao(direction, transform.position.x, transform.position.y, i);
                Instantiate(prefabExplosao, newPos, Quaternion.identity);
            }
        }
        else {
            for (float i = 0.5f; i < powerUp; i += 1)
            {
                Vector2 newPos = GetDirecaoExplosao(direction,transform.position.x,transform.position.y,i);
                Instantiate(prefabExplosao, newPos, Quaternion.identity);
            }
        }
    }

    Vector2 GetDirecaoExplosao(Vector2 direction, float posX, float posY, float incremento) {
        Vector2 newPos = Vector2.zero;
        if (direction.x > 0)
        {
            newPos = new Vector2(posX + incremento + 0.5f, posY);
        }
        else if (direction.x < 0)
        {
            newPos = new Vector2(posX - incremento - 0.5f, posY);
        }
        else if (direction.y > 0)
        {
            newPos = new Vector2(posX, posY + incremento + 0.5f);
        }
        else if (direction.y < 0)
        {
            newPos = new Vector2(posX, posY - incremento - 0.5f);
        }
        return newPos;
    }

    public void Explodir() {
        Instantiate(prefabExplosao, transform.position, Quaternion.identity);
        SpawnExplosion(Vector2.up);
        SpawnExplosion(Vector2.down);
        SpawnExplosion(Vector2.left);
        SpawnExplosion(Vector2.right);
        bombMan.contaBomba--;
        renderer.enabled = false;
        collider.enabled = false;
        explodiu = true;
        audioSource.Play();
    }

}
