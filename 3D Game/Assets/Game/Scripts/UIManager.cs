using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text ammoText;
    [SerializeField]
    private GameObject coin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateAmmo(int count)
    {
        ammoText.text = "Ammo: " + count;
    }

    public void ShowCoin()
    {
        coin.SetActive(true);
    }

    public void HideCoin()
    {
        coin.SetActive(false);
    }
}
