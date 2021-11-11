using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed = 4.0f;

    private Player player;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        if (player == null)
        {
            Debug.LogError("player variable is null.");
        }

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < -6.5f)
        {
            float rx = Random.Range(-9.0f, 9.0f);
            transform.position = new Vector3(rx, 6.5f, 0);
        }
    
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);

            animator.SetTrigger("OnEnemyDestruction");
            speed = 0;
            Destroy(this.gameObject, 2.5f);

            player.AddScore();
        }

        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
                
                animator.SetTrigger("OnEnemyDestruction");
                speed = 0;
                Destroy(this.gameObject, 2.5f);
            }
        }
    }
}
