using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInitialColorSetupScript : MonoBehaviour
{
    public int colour;
    public string playerTag;

    void Start()
    {

        playerTag = gameObject.GetComponent<Collider>().tag;

        switch (playerTag)
        {
            case "Player0":
                colour = 0;
                break;
            case "Player1":
                colour = 1;
                break;
            case "Player2":
                colour = 2;
                break;
            case "Player3":
                colour = 3;
                break;
            case "Player4":
                colour = 4;
                break;
        }
    }

    void Update()
    {

    }
}
