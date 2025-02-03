using UnityEngine;

public class StageOneRoom
{
    public Vector2Int center; //ID, center of the room
    public int[] size = new int[2]; //width, height
    public RoomType roomType;
    public Vector2Int topEntrance, bottomEntrance, leftEntrance, rightEntrance;
    public bool hasTopDoor, hasBottomDoor, hasLeftDoor, hasRightDoor;
}

public enum RoomType
{
    Spawn,
    Normal, Elite,
    Treasure, 
    Upgrade, Shop,  //Consider
    Exit
}
