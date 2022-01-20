using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    [SerializeField] private Room[] _roomPrefabs;
    [SerializeField] private Room _startRoom;

    private Room[,] _spawnedRooms;

    private IEnumerator Start()
    {
        _spawnedRooms = new Room[11, 11];
        _spawnedRooms[5, 5] = _startRoom;

        for (int i = 0; i < 12; i++)
        {
            Spawn();
            yield return new WaitForSeconds(0.7f);
        }
                
    }

    private void Spawn()
    {
        HashSet<Vector2Int> vacantPlaces = new HashSet<Vector2Int>();

        for (int x = 0; x < _spawnedRooms.GetLength(0); x++)
        {
            for (int y = 0; y < _spawnedRooms.GetLength(1); y++)
            {
                if (_spawnedRooms[x, y] == null) continue;

                int maxX = _spawnedRooms.GetLength(0) - 1;
                int maxY = _spawnedRooms.GetLength(1) - 1;

                if (x > 0 && _spawnedRooms[x - 1, y] == null)
                    vacantPlaces.Add(new Vector2Int(x - 1, y));
                if (y > 0 && _spawnedRooms[x, y - 1] == null)
                    vacantPlaces.Add(new Vector2Int(x, y - 1));
                if (x < maxX && _spawnedRooms[x + 1, y] == null)
                    vacantPlaces.Add(new Vector2Int(x + 1, y));
                if (y < maxY && _spawnedRooms[x, y + 1] == null)
                    vacantPlaces.Add(new Vector2Int(x, y + 1));
            }
        }

        Room newRoom = Instantiate(_roomPrefabs[Random.Range(0, _roomPrefabs.Length)]);
        int limit = 400;
        while (limit-- > 0)
        {
            Vector2Int position = vacantPlaces.ElementAt(Random.Range(0, vacantPlaces.Count));
            

            if (ConnectToDoor(newRoom, position))
            {
                newRoom.transform.position = new Vector3(position.x - 5, 0, position.y - 5) * 10;
                _spawnedRooms[position.x, position.y] = newRoom;
                break;
            } 
        }      
    }

    private bool ConnectToDoor(Room room, Vector2Int posRoom)
    {
        int maxX = _spawnedRooms.GetLength(0) - 1;
        int maxY = _spawnedRooms.GetLength(1) - 1;

        List<Vector2Int> neighbours = new List<Vector2Int>();

        if (room._doorF != null && posRoom.y < maxY && _spawnedRooms[posRoom.x, posRoom.y + 1]?._doorB != null) neighbours.Add(Vector2Int.up);
        if (room._doorB != null && posRoom.y > 0 && _spawnedRooms[posRoom.x, posRoom.y - 1]?._doorF != null) neighbours.Add(Vector2Int.down);
        if (room._doorR != null && posRoom.x < maxX && _spawnedRooms[posRoom.x + 1, posRoom.y]?._doorL != null) neighbours.Add(Vector2Int.right); 
        if (room._doorL != null && posRoom.x > 0 && _spawnedRooms[posRoom.x - 1, posRoom.y]?._doorR != null) neighbours.Add(Vector2Int.left);

        if (neighbours.Count == 0) return false;

        Vector2Int selectDirection = neighbours[Random.Range(0, neighbours.Count)];
        Room selectedRoom = _spawnedRooms[posRoom.x + selectDirection.x, posRoom.y + selectDirection.y];

        if (selectDirection == Vector2Int.up)
        {
            room._doorF.SetActive(false);
            selectedRoom._doorB.SetActive(false);
        }

        else if (selectDirection == Vector2Int.down)
        {
            room._doorB.SetActive(false);
            selectedRoom._doorF.SetActive(false);
        }

        else if(selectDirection == Vector2Int.right)
        {
            room._doorR.SetActive(false);
            selectedRoom._doorL.SetActive(false);
        }

        else if(selectDirection == Vector2Int.left)
        {
            room._doorL.SetActive(false);
            selectedRoom._doorR.SetActive(false);
        }

        return true;
    }
}
