using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;




public class GameManegerScript : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject BoxPrefab;
   
    public GameObject clearText;
    public GameObject goalPrefab;

    int[,] map;
    GameObject[,] field;

    bool IsCleard()
    {
        List<Vector2Int> goals = new List<Vector2Int>();
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y, x] == 3)
                {
                    goals.Add(new Vector2Int(x, y));
                }
            }

        }

        for(int i = 0; i < goals.Count; i++)
        {
            GameObject f = field[goals[i].y, goals[i].x];
            if(f == null || f.tag != "Box")
            {
                return false;
            }


        }
       
        Debug.Log("Clear!");
        return true;
    }

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

        Screen.SetResolution(1920, 1080, false);

        map = new int[,]
        {{0,0,0,0,0},
         {0,3,1,3,0},
         {0,0,2,0,0},
         {0,2,3,2,0},
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

                if (map[y, x] == 2)
                {
                    field[y, x] = Instantiate(
                    BoxPrefab,
                    new Vector3(x, map.GetLength(0) - y, 0),
                    Quaternion.identity
                    );
                }


                if (map[y, x] == 3)
                {
                    field[y, x] = Instantiate(
                    goalPrefab,
                    new Vector3(x, map.GetLength(0) - y, 0.01f),
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

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {

            Vector2Int playerIndex = GetPlayerIndex();

            MoveNumber("Player", playerIndex, playerIndex - new Vector2Int(0, 1));

        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {

            Vector2Int playerIndex = GetPlayerIndex();

            MoveNumber("Player", playerIndex, playerIndex + new Vector2Int(0, 1));

        }

        if(IsCleard()){
            clearText.SetActive(true);
        }
       

    }
    
}
