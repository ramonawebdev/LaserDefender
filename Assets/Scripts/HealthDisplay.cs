using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour {


    Text healthText;
    

    // Use this for initialization
    void Start()
    {
        healthText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Player player = FindObjectOfType<Player>();
        if (player)
        {
            healthText.text = player.GetHealth().ToString();
        }
        else
        {
            int health = 0;
            healthText.text = health.ToString();
        }
    }
}
