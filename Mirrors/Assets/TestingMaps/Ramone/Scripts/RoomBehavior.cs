using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehavior : MonoBehaviour
{
    public GameObject[] walls; //Up, Down, Right then Left.
    public GameObject[] doors;

    //public bool[] testingRoomStatus;
    // Start is called before the first frame update
    //void Start()
    //{
    //    UpdateRooms(testingRoomStatus);
    //}

    public void UpdateRooms(bool[] status)
    {
        for (int i = 0; i< status.Length; i++)
        {
            doors[i].SetActive(status[i]);
            walls[i].SetActive(!status[i]);
        }
    }
}
