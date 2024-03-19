using System;
using System.Collections;
using UnityEngine;

namespace MapGenerator
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject _hallwayPrefab;
        [SerializeField] private GameObject _hallwayFloorPrefab;

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
            _hallwayGenerator = new HallwayGenerator();
        }

        private void OnEnable()
        {
            if (_hallwayGenerator != null)
            {
                _hallwayGenerator.OnHallwayWallInstantiated += InstantiateHallwayWall;
                _hallwayGenerator.OnGameObjectDestroyed += DestroyGameObject;
            }
        }

        private void OnDisable()
        {
            if (_hallwayGenerator != null)
            {
                _hallwayGenerator.OnHallwayWallInstantiated -= InstantiateHallwayWall;
                _hallwayGenerator.OnGameObjectDestroyed -= DestroyGameObject;
            }
        }

        private void Start()
        {
            _roomGenerator.GenerateRooms(DUNGEON_SIZE_X, DUNGEON_SIZE_Y, 50, _aStar);
            _hallwayGenerator.GenerateHallways(_bowyerWatson.GenerateTriangularMesh(_roomGenerator.PlacedRooms), 
                _roomGenerator.PlacedRooms, _aStar, _primsAlg, _hallwayPrefab, _hallwayFloorPrefab);
            _roomGenerator.BuildRooms(_aStar);
            StartCoroutine(nameof(WaitForHallways));
        }

        private IEnumerator WaitForHallways()
        {
            yield return new WaitForSeconds(0.5f);
            _hallwayGenerator.MakeRoomEntrances();
        }

        #region Event methods
        private void InstantiateHallwayWall(GameObject gameObject, Vector3 position, Vector3 scale)
        {
            Instantiate(gameObject, position, Quaternion.identity).transform.localScale = scale;
        }

        private void DestroyGameObject(GameObject gameObject)
        {
            Destroy(gameObject);
        }
        #endregion
    }
}
