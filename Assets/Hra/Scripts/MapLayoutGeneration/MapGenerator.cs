using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private Room _room;
    [SerializeField] private GameObject _hallway;

    private AStar _aStar;
    private PrimsAlg _primsAlg;
    private RoomGenerator _roomGenerator;
    private HallwayGenerator _hallwayGenerator;
    private BowyerWatson _bowyerWatson;

    private const int DUNGEON_SIZE_X = 50;
    private const int DUNGEON_SIZE_Y = 50;

    private void Awake()
    {
        _primsAlg = new PrimsAlg();
        _bowyerWatson = new BowyerWatson();
        _aStar = new AStar(DUNGEON_SIZE_X + 10, DUNGEON_SIZE_Y + 10);
        _roomGenerator = GetComponent<RoomGenerator>();
        _hallwayGenerator = GetComponent<HallwayGenerator>();
    }

    private void Start()
    {
        _roomGenerator.GenerateRooms(DUNGEON_SIZE_X, DUNGEON_SIZE_Y, 50, _aStar, _room);
        _hallwayGenerator.GenerateHallways(_bowyerWatson.GenerateTriangularMesh(_roomGenerator.PlacedRooms), _roomGenerator.PlacedRooms, _aStar, _primsAlg, _hallway);
        _roomGenerator.BuildRooms(_aStar);
    }
}
