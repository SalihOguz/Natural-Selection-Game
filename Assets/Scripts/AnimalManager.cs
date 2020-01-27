using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalManager : MonoBehaviour
{
    public static AnimalManager Instance;
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

    private void Update() {
        print("Animal Count: " + _animalsParent.childCount);
    }
}
