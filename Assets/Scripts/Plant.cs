using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public int MaxFoodCount;
    public int CurrentFoodCount;
    public float EnergyPerFood = 25f;

    public int FoodRefreshTime = 20; // sec

    private List<Animal> _eatingAnimals = new List<Animal>();

    public void EatFood(Animal animal)
    {
        _eatingAnimals.Add(animal);
        StartCoroutine(FinishFood(animal));
    }

    private IEnumerator FinishFood(Animal animal)
    {
        yield return new WaitForSeconds(0.2f);

        if (CurrentFoodCount > 0)
        {
            animal.CurrentEnergy += EnergyPerFood;
            CurrentFoodCount--;
        }
    }

    void Start()
    {
        StartCoroutine(RefreshFood());
    }

    IEnumerator RefreshFood()
    {
        yield return new WaitForSeconds(FoodRefreshTime);
        CurrentFoodCount = Mathf.Clamp(CurrentFoodCount + 1, 0, MaxFoodCount);
        StartCoroutine(RefreshFood());
    }
}
