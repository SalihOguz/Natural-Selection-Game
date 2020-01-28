using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalManager : MonoBehaviour
{
    public static AnimalManager Instance;
    public GameObject AnimalPrefab;
    private Vector3[,] _cubePosList;

    [SerializeField]
    private Transform _animalsParent;

    //private List<Animal> poolAnimals;

    private void Awake() {
        Instance = this;
    }

    public void SpawnAnimals()
    {
        foreach (Transform animal in _animalsParent)
        {
            animal.GetComponent<Animal>().Spanw();
        }
    }

    public void GiveBirthNewAnimal() // TODO spanw animal prefab according to animal type and position
    {
        GameObject animal = Instantiate(AnimalPrefab, Vector3.zero, Quaternion.identity, _animalsParent);
        animal.GetComponent<Animal>().Spanw();
        print("BORN");
    }

    private void Update() {
        print("Animal Count: " + _animalsParent.childCount);
    }
}
