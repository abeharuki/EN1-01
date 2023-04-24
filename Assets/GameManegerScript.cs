using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;

public class GameManegerScript : MonoBehaviour
{
    public GameObject playerPrefab;
    int[,] map;
    GameObject[,] field;



    Vector2Int GetPlayerIndex()
    {
        for (int y = 0; y < map.GetLength(0); y++)
        {

            for (int x = 0; x < map.GetLength(1); x++)
            {

                
                if (field[y, x] != null && field[y, x].tag == "Player") { return new Vector2Int(x, y); }

            }

        }
        return new Vector2Int(-1, -1);
    }



    bool MoveNumber(string tag,Vector2Int moveFrom,Vector2Int moveTo)
   {
        if (moveTo.y < 0 || moveTo.y >= map.GetLength(0)) { return false; }
        if (moveTo.x < 0 || moveTo.x >= map.GetLength(1)) { return false; }

        if (field[moveTo.y, moveTo.x] != null && field[moveTo.y, moveTo.x].tag == "Box")
        {
            Vector2Int velocity = moveTo - moveFrom;
            bool success = MoveNumber(tag, moveTo, moveTo + velocity);

            if (!success) { return false; }
        }

        field[moveFrom.y, moveFrom.x].transform.position = 
            new Vector3(moveTo.x, field.GetLength(0) - moveTo.y, 0);
        field[moveTo.y, moveTo.x] = field[moveFrom.y, moveFrom.x];
        field[moveFrom.y, moveFrom.x] = null;
        return true;


    }

  
    // Start is called before the first frame update
    void Start()
    {


        map = new int[,]
        {{0,0,0,0,0},
         {0,0,1,0,0},
         {0,0,0,0,0},

        };

        field = new GameObject[
            map.GetLength(0), 
            map.GetLength(1)
            ];

       

        for (int y = 0;y< map.GetLength(0); y++){

            for(int x= 0; x < map.GetLength(1); x++)
            {
               
                if (map[y,x] == 1)
                {
                    field[y,x] = Instantiate(
                    playerPrefab,
                    new Vector3(x, map.GetLength(0)-y, 0),
                    Quaternion.identity
                    );
                }
            }
            
        }
      
      

    }

   
    
    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
           
            Vector2Int playerIndex = GetPlayerIndex();

            MoveNumber("Player", playerIndex, playerIndex + new Vector2Int (1,0));

        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            Vector2Int playerIndex = GetPlayerIndex();

            MoveNumber("Player", playerIndex, playerIndex - new Vector2Int(1, 0));
            
        }
    }
    
}
