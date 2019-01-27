using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TilesManager : MonoBehaviour
{
    public int player0Tiles;
    public int player1Tiles;
    public int player2Tiles;
    public int player3Tiles;
    public int dirtyTiles;
    public int totalTiles;

    public Text player0Score;
    public Text player1Score;
    public Text player2Score;
    public Text player3Score;
    void Start()
    {
        player0Tiles = 0;
        player1Tiles = 0;
        player2Tiles = 0;
        player3Tiles = 0;
        totalTiles = GameObject.FindGameObjectsWithTag("Tile").Length;
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
               dirtyTiles--;
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
                dirtyTiles++;
                break;
        }

       
        player0Score.text = (player0Tiles * 100 / totalTiles).ToString("0\\%");
        player1Score.text = (player1Tiles * 100 / totalTiles).ToString("0\\%");
        if (player2Tiles > 0)
            player2Score.text = (player2Tiles * 100 / totalTiles).ToString("0\\%");
        if (player3Tiles > 0)
            player3Score.text = (player3Tiles * 100 / totalTiles).ToString("0\\%");


        return;
    }
}
