using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGen : MonoBehaviour
{
    public class Cell
    {
        public bool isvisited = false;
        public bool[] status = new bool[4]; //Again, UDLR.
    }

    public Vector2 size;
    public int mazestart = 0;
    public GameObject room;
    public Vector2 offset;

    List<Cell> gameboard;

    // Start is called before the first frame update
    void Start()
    {
        MazeMaker();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateHalls()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                Cell currentcell = gameboard[Mathf.FloorToInt(i + j * size.x)];
                if (currentcell.isvisited)
                {
                    var newRoom = Instantiate(room, new Vector3(i * offset.x, 0, -j * offset.y), Quaternion.identity, transform).GetComponent<RoomBehavior>();
                    newRoom.UpdateRooms(currentcell.status);

                    newRoom.name += " " + i + " " + j;
                }
            }
        }
    }

    void MazeMaker()
    {
        gameboard = new List<Cell>();
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                gameboard.Add(new Cell());
            }
        }

        int currcell = mazestart;
        Stack<int> path = new Stack<int>();
        int loop = 0;
        while (loop < 1000) //Might add variable for maximum size
        {
            loop++;
            gameboard[currcell].isvisited = true;

            if (currcell == gameboard.Count -1)
            {
                break;
            }
            List<int> neighbors = NeighborCheck(currcell);

            if(neighbors.Count  ==0)
            {
                if (path.Count == 0)
                {
                    break;
                }
                else
                {
                    currcell = path.Pop();
                }
            }
            else
            {
                path.Push(currcell);
                int newCell = neighbors[Random.Range(0, neighbors.Count)];

                if(newCell > currcell)
                {
                    //down or right
                    if ((newCell-1) == currcell)
                    {
                        gameboard[currcell].status[2] = true;
                        currcell = newCell;
                        gameboard[currcell].status[3] = true;
                    }
                    else
                    {
                        gameboard[currcell].status[1] = true;
                        currcell = newCell;
                        gameboard[currcell].status[0] = true;
                    }
                }
                else
                {
                    //up or left
                    if ((newCell + 1) == currcell)
                    {
                        gameboard[currcell].status[3] = true;
                        currcell = newCell;
                        gameboard[currcell].status[2] = true;
                    }
                    else
                    {
                        gameboard[currcell].status[0] = true;
                        currcell = newCell;
                        gameboard[currcell].status[1] = true;
                    }
                }
            }
        }
        GenerateHalls();
    }

    List<int> NeighborCheck(int cell)
    {
        List<int> neighbors = new List<int>();
        //Up Neighbor
        if (cell - size.x >= 0 && !gameboard[Mathf.FloorToInt(cell-size.x)].isvisited)
        {
            neighbors.Add(Mathf.FloorToInt(cell - size.x));
        }
        //Down Neighbor
        if (cell + size.x < gameboard.Count && !gameboard[Mathf.FloorToInt(cell + size.x)].isvisited)
        {
            neighbors.Add(Mathf.FloorToInt(cell + size.x));
        }
        //Right Neighbor
        if ((cell+1) % size.x != 0 && !gameboard[Mathf.FloorToInt(cell + 1)].isvisited)
        {
            neighbors.Add(Mathf.FloorToInt(cell + 1));
        }
        //Left Neighbor
        if (cell % size.x != 0 && !gameboard[Mathf.FloorToInt(cell - 1)].isvisited)
        {
            neighbors.Add(Mathf.FloorToInt(cell - 1));
        }
        return neighbors;
    }
}
