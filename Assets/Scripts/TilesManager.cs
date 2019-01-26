using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesManager : MonoBehaviour
{
    public int player0Tiles;
    public int player1Tiles;
    public int player2Tiles;
    public int player3Tiles;
    public int player4Tiles;

    void Start()
    {
        player0Tiles = GameObject.FindGameObjectsWithTag("Tile").Length;
        player1Tiles = 0;
        player2Tiles = 0;
        player3Tiles = 0;
        player4Tiles = 0;
    }

    void Update()  {}

    public void UpdateTilesCOunt(int oldcolour, int newcolour)
    {
        switch (oldcolour)
        {
            case 0:
                player0Tiles--;
                break;
            case 1:
                player1Tiles--;
                break;

            case 2:
                player2Tiles--;
                break;

            case 3:
                player3Tiles--;
                break;

            case 4:
                player4Tiles--;
                break;
        }

        switch (newcolour)
        {
            case 0:
                player0Tiles++;
                break;

            case 1:
                player1Tiles++;
                break;

            case 2:
                player2Tiles++;
                break;

            case 3:
                player3Tiles++;
                break;

            case 4:
                player4Tiles++;
                break;
        }
    }
}
