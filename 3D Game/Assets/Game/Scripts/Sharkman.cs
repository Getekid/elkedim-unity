﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sharkman : MonoBehaviour
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
            if (player && player.hasCoin)
            {
                player.hasCoin = false;
                // Also update the Inventory.
                UIManager uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
                if (uiManager)
                {
                    uiManager.HideCoin();
                }
                player.EnableWeapon();
            }
        }
    }
}
