﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.hasCoin = true;

                // Show the coin in the inventory.
                UIManager uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
                uiManager.ShowCoin();

                Destroy(this.gameObject);
            }
        }
    }
}
