using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float speed = 3.0f;
    [SerializeField]
    private int powerupID; // 1 = Triple Shot, 2 = Speed

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // If the powerup leaves the screen then destroy it.
        if (transform.position.y < -6.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Destroy(this.gameObject);
            
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                if (powerupID == 1)
                {
                    player.ActivateTripleShot();
                }
                else if (powerupID == 2)
                {
                    player.ActivateSpeed(2.5f, 3.5f);
                }
                else if (powerupID == 3)
                {
                    player.ActivateShield();
                }
            }
        }
    }
}
