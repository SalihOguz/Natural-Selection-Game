using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public int MaxFoodCount;
    public int CurrentFoodCount;

    public int FoodRefreshTime = 10; // sec

    private List<Animal> _eatingAnimals = new List<Animal>();

    public void EatFood(Animal animal)
    {
        _eatingAnimals.Add(animal);

    }

    private IEnumerator FinishFood()
    {
        yield return new WaitForSeconds(0.2f);

        
    }
}
