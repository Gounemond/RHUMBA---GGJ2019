using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public int oldColour;
    public int newColour;
    public bool enter = true;
    public string playerTag = "Player0";

    public TilesManager tilesManagerScript;


    void Start()
    {      
        oldColour = 0; //grigio iniziale
    }

    void Update(){}

    private void OnTriggerEnter(Collider playerCollider)
    {
        Debug.Log("triggrred");
        playerTag = playerCollider.tag;
        switch (playerTag)
        {
            case "Player0":
                newColour = 0;
                break;
            case "Player1":
                newColour = 1;
                break;
            case "Player2":
                newColour = 2;
                break;
            case "Player3":
                newColour = 3;
                break;
            case "Player4":
                newColour = 4;
                break;
        }
        if (newColour!= oldColour)
            tilesManagerScript.UpdateTilesCOunt(oldColour, newColour);

        oldColour = newColour;
    }

}
