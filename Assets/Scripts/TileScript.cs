using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public int oldColour;
    public int newColour;
    public bool enter = true;
    public int? playerTag = 4;

    public TilesManager tilesManagerScript;


    void Start()
    {      
        oldColour = newColour = 4; //grigio iniziale

    }

    void Update(){}

    private void OnTriggerEnter(Collider playerCollider)
    {   
        playerTag = playerCollider.GetComponent<PlayerController>()?.playerId;
        switch (playerTag)
        {
            case 0:
                newColour = 0;
                break;
            case 1:
                newColour = 1;
                break;
            case 2:
                newColour = 2;
                break;
            case 3:
                newColour = 3;
                break;
            default:
                newColour = 4;
                break;
        }
        if (newColour!= oldColour) { 
            tilesManagerScript.UpdateTilesCOunt(oldColour, newColour);
            oldColour = newColour;
        }
    }

}
