using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalManager : MonoBehaviour
{
    public static AnimalManager Instance;
    public List<GameObject> AnimalPrefabList;
    private Vector3[,] _cubePosList;

    public int TotalAnimalCount;

    [SerializeField]
    private Transform _animalsParent;
    private int _rowCount;
    private int _columnCount;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        _rowCount = MapGenerator.Instance.rowCount;
        _columnCount = MapGenerator.Instance.columnCount;
    }

    public void SpawnAnimals()
    {
        int spiciesCount = 0;
        int spawnedCount = 0;
        for (int i = 0; i < TotalAnimalCount; i++)
        {
            if (spawnedCount >= TotalAnimalCount / AnimalPrefabList.Count)
            {
                spiciesCount++;
                spawnedCount = 0;
            }
            Pos pos = GetRandomValidPositionToSpawn();
            GameObject animal = Instantiate(AnimalPrefabList[spiciesCount], MapGenerator.Instance.cubePosList[pos.X, pos.Y], Quaternion.identity, _animalsParent);
            animal.GetComponent<Animal>().Spanw(pos);
            spawnedCount++;
        }
    }

    public void GiveBirthNewAnimal(Pos pos, Species species)
    {
        GameObject animal = Instantiate(GetSpiciesPrefab(species), MapGenerator.Instance.cubePosList[pos.X, pos.Y], Quaternion.identity, _animalsParent);
        animal.GetComponent<Animal>().Spanw(pos);
        print("BORN");
    }

    private GameObject GetSpiciesPrefab(Species species)
    {
        foreach (GameObject prefab in AnimalPrefabList)
        {
            if (prefab.name == species.ToString())
            {
                return prefab;
            }
        }
        return null;
    }

    private void Update() {
        print("Animal Count: " + _animalsParent.childCount);
    }

    private Pos GetRandomValidPositionToSpawn()
    {
        int stackOverflowGuard = 0;
        
        while(true)
        {
            Pos pos = new Pos(0,0);
            pos.X = Random.Range(0, _rowCount - 2);
            pos.Y = Random.Range(0, _columnCount - 2);

            CubeData cubeData = MapGenerator.cubeDataList[pos.X, pos.Y];
            if (cubeData.cubeType != CubeType.water && cubeData.cubeFeature == CubeFeature.none && cubeData.standingAnimal == null)
            {
                return pos;
            }

            stackOverflowGuard++;

            if (stackOverflowGuard > 10000)
            {
                break;
            }
        }
        return new Pos(0,0);
    }
}
