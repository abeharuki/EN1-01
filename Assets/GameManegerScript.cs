using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class GameManegerScript : MonoBehaviour
{

    int[] map;
    void PrintArreay()
    {
        string debugText = "";
        for(int i = 0; i < map.Length; i++)
        {
            debugText += map[i].ToString() + ", ";
        }
        Debug.Log(debugText);
    }
    int GetPlayerIndex()
    {
        for (int i = 0; i < map.Length; i++)
        {
            if (map[i] == 1)
            {
                return i;
            }
            return -1;
        }
    }

    bool MoveNumber(int number,int moveFrom,int moveTo)
    {
        if(moveTo<0||moveTo >= map.Length)
        {
            return false;
        }
        map[moveTo] = number;
        map[moveFrom] = 0;
        return true;

    }
   


    // Start is called before the first frame update
    void Start()
    {
        map = new int[] { 0, 0, 0, 1, 0, 0, 0, 0, 0 };
        PrintArreay();

    }

   

    // Update is called once per frame
    void Update()
    {
       


        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
           
            int playerIndex = GetPlayerIndex;

            if (playerIndex < map.Length - 1)
            {
               
                map[playerIndex + 1] = 1;
                map[playerIndex] = 0;
            }
            PrintArreay();
        }


    }
}
