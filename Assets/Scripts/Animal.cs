using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Animal : MonoBehaviour
{
    public float Speed = 1;
    public int Strength = 1;
    public int SenseDistance = 5;
    public Gender Gender;
    public DietType DietType;
    public Species Species;
    public bool IsPregnant = false;
    
    [HideInInspector]
    public float CurrentEnergy;

    private AnimalState _currentState;
    public AnimalState CurrentState{
        get=> _currentState;
    }

    private int _currentX;
    private int _currentY;

    private int _rowCount;
    private int _columnCount;

    private int _animalDirection;
    private float _moveSpeed;
    private float _energyConsumption;
    private float _energyEmergency;

    private float _matingEnergy = 50;

    private int stackOverflowGuard = 0;

    public void Spanw()
    {
        _rowCount = MapGenerator.Instance.rowCount;
        _columnCount = MapGenerator.Instance.columnCount;

        GetValidPositionToSpawn();
        transform.position = MapGenerator.cubeDataList[_currentX, _currentY].pos + (Vector3.up / 2);
        gameObject.SetActive(true);

        MapGenerator.cubeDataList[_currentX, _currentY].standingAnimal = this;
        _currentState = AnimalState.RandomWalk;
        _moveSpeed = 0.4f / Speed;
        _energyConsumption = (Mathf.Pow(Strength, 3) * Mathf.Pow(Speed, 2) + SenseDistance) / 5;
        print("_energyConsumption " + _energyConsumption);
        CurrentEnergy = 100;
        _energyEmergency = _energyConsumption * 60;
        Gender = (Gender)Random.Range(0,2);

        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(Random.Range(0, 0.4f));
        GetRandomDirection();
        StartCoroutine(Walk());
    }

    IEnumerator Walk()
    {
        CurrentEnergy -= _energyConsumption;
        CheckDeath();

        GetNewDirection();

        yield return new WaitForSeconds(_moveSpeed / 2);

        ChangeDirection();

        yield return new WaitForSeconds(_moveSpeed / 2);
        
        GoToDirection();

        stackOverflowGuard = 0;
        StartCoroutine(Walk());
    }

    #region Helper Functions

    private void CheckDeath()
    {
        if (CurrentEnergy <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void ChangeDirection()
    {
        if (_animalDirection == 0) // front
        {
            TurnForward();
        }
        if (_animalDirection == 1) // back
        {
            TurnBack();
        }
        if (_animalDirection == 2) // right
        {
            TurnRight();
        }
        if (_animalDirection == 3) // left
        {
            TurnLeft();
        }
    }

    private void GoToDirection()
    {
        MapGenerator.cubeDataList[_currentX, _currentY].standingAnimal = null;

        if (_animalDirection == 0) // front
        {
            GoForward();
        }
        if (_animalDirection == 1) // back
        {
            GoBack();
        }
        if (_animalDirection == 2) // right
        {
            GoRight();
        }
        if (_animalDirection == 3) // left
        {
            GoLeft();
        }

        MapGenerator.cubeDataList[_currentX, _currentY].standingAnimal = this;

        Vector3 destination = MapGenerator.cubeDataList[_currentX, _currentY].pos + (Vector3.up / 2);        
        Vector3 midPoint = (transform.position + destination) / 2 + new Vector3(0, 0.5f, 0);
        Vector3[] pathWaypoints = new[] {midPoint, destination};

        transform.DOPath(pathWaypoints, 0.3f).SetEase(Ease.OutSine);
    }

    
    private void GoForward()
    {
        _currentY++;     
    }

    private void GoBack()
    {
        _currentY--;  
    }

    private void GoRight()
    {
        _currentX++;
    }

    private void GoLeft()
    {
        _currentX--;
    }

    private void TurnForward()
    {
        transform.DORotate(Vector3.zero, 0.2f);
    }

    private void TurnBack()
    {
        transform.DORotate(new Vector3(0, 180f, 0), 0.2f);
    }

    private void TurnRight()
    {
        transform.DORotate(new Vector3(0, 90f, 0), 0.2f);
    }

    private void TurnLeft()
    {
        transform.DORotate(new Vector3(0, -90f, 0), 0.2f);
    }

    #endregion

    private void GetNewDirection() // TODO
    {
            EnvironmentScan scan = CheckNeightbors();

            if (scan.foodList.Count > 0  && CurrentEnergy <= _energyEmergency && _currentState != AnimalState.Mating)
            {
                Pos closest = GetClosest(scan.foodList);

                if (GetDistance(closest) <= 1.1f)
                {
                    _currentState = AnimalState.EatFood;
                    _animalDirection = -1;
                    MapGenerator.cubeDataList[closest.X, closest.Y].standingPlant.EatFood(this);
                    return;
                }
                else
                {
                    _currentState = AnimalState.GoToFood;
                    GetDirectionToPos(closest);
                }
            }
            else if (scan.mateList.Count > 0)
            {
                Pos closest = GetClosest(scan.mateList);

                if (GetDistance(closest) <= 1.1f)
                {
                    _currentState = AnimalState.Mating;
                    _animalDirection = -1;
                    MapGenerator.cubeDataList[closest.X, closest.Y].standingAnimal.Mate(this);
                    CurrentEnergy -= _matingEnergy;

                    if (CurrentEnergy <= 0)
                    {
                        print("DIED DURING MATING");
                    }
                    return;
                }
                else
                {
                    _currentState = AnimalState.LookForMate;
                    GetDirectionToPos(closest);
                }
            }
            else
            {
                GetForwardHeavyRandomDirection();
            }
            CheckDirectionValidation();
        
    }

    private void GetRandomDirection()
    {
        stackOverflowGuard++;
        if (stackOverflowGuard > 50)
        {
            _animalDirection = -1;
        }
        else
        {
            _animalDirection = Random.Range(0, 4);
        }
    }

    private void GetForwardHeavyRandomDirection()
    {
        if (Random.Range(0f, 100f) < 30f)
        {
            GetRandomDirection();
        }
    }

    private void GetDirectionToPos(Pos pos)
    {
        if (Mathf.Abs(_currentX - pos.X) > Mathf.Abs(_currentY - pos.Y)) // move in X axis
        {
            if (pos.X > _currentX)
            {
                _animalDirection = 2;
            }
            else
            {
                _animalDirection = 3;
            }
        }
        else
        {
            if (pos.Y > _currentY)
            {
                _animalDirection = 0;
            }
            else
            {
                _animalDirection = 1;
            }
        }

        CheckDirectionValidation();
    }

    private void CheckDirectionValidation()
    {
        stackOverflowGuard++;
        if (stackOverflowGuard > 50)
        {
            return;
        }

        if (_animalDirection == 0 && IsCubeValid(_currentX, _currentY + 1)) // front
        {
            return;
        }
        if (_animalDirection == 1 && IsCubeValid(_currentX, _currentY - 1)) // back
        {
            return;
        }
        if (_animalDirection == 2 && IsCubeValid(_currentX + 1, _currentY)) // right
        {
            return;
        }
        if (_animalDirection == 3 && IsCubeValid(_currentX - 1, _currentY)) // left
        {
            return;
        }

        GetRandomDirection();
        CheckDirectionValidation();
    }

    private bool IsCubeValid(int x, int y) // check edges of map, water, features and animals
    {
       
        if (_animalDirection == 0) // front
        {
            if (_currentY == _columnCount - 2)
            {
                return false;
            }
        }
        if (_animalDirection == 1) // back
        {
            if (_currentY == 0)
            {
               return false;
            }
        }
        if (_animalDirection == 2) // right
        {
            if (_currentX == _rowCount - 2)
            {
                return false;
            }
        }
        if (_animalDirection == 3) // left
        {
            if (_currentX == 0)
            {
                return false;
            }
        }

        CubeData cubeData = MapGenerator.cubeDataList[x, y];
        if (cubeData.standingAnimal != null)
        {
            return false;
        }

         if (cubeData.cubeType == CubeType.water || cubeData.cubeFeature != CubeFeature.none)
        {
            return false;
        }

        return true;
    }

    private void GetValidPositionToSpawn()
    {
        int stackOverflowGuard = 0;
        
        while(true)
        {
            _currentX = Random.Range(0, _rowCount - 2);
            _currentY = Random.Range(0, _columnCount - 2);

            CubeData cubeData = MapGenerator.cubeDataList[_currentX, _currentY];
            if (cubeData.cubeType != CubeType.water && cubeData.cubeFeature == CubeFeature.none && cubeData.standingAnimal == null)
            {
                break;
            }

            stackOverflowGuard++;

            if (stackOverflowGuard > 10000)
            {
                Destroy(gameObject);
            }
        }
    }

    private EnvironmentScan CheckNeightbors()
    {
       EnvironmentScan scan = new EnvironmentScan();

        int minX = Mathf.Max(0, _currentX - SenseDistance);
        int maxX = Mathf.Min(_rowCount - 1, _currentX + SenseDistance);
        int minY = Mathf.Max(0, _currentY - SenseDistance);
        int maxY = Mathf.Min(_columnCount - 1, _currentY + SenseDistance);

        for (int x = minX; x < maxX; x++)
        {
            for (int y = minY; y < maxY; y++)
            {
                if (x == _currentX && y == _currentY)
                {
                    continue;
                }
                
                CubeData cubeData = MapGenerator.cubeDataList[x,y];
                Animal standingAnimal = cubeData.standingAnimal;
                if (standingAnimal != null)
                {
                    if (standingAnimal.DietType == DietType.Carnivore && standingAnimal.Strength > Strength) // found a possible predator
                    {
                        scan.predetorList.Add(new Pos(x, y));
                    }
                    else if (!IsPregnant && standingAnimal.Species == Species && standingAnimal.Gender != Gender 
                                && !standingAnimal.IsPregnant && standingAnimal.CurrentState != AnimalState.Mating && standingAnimal.CurrentEnergy > _matingEnergy)
                    {
                        scan.mateList.Add(new Pos(x, y));
                    }
                }
                else if (cubeData.standingPlant != null && cubeData.standingPlant.CurrentFoodCount > 0)
                {
                    scan.foodList.Add(new Pos(x, y));
                }
            }
        }

        return scan;
    }

    private Pos GetClosest(List<Pos> posList)
    {
        Pos minPos = posList[0];
        float minLength = SenseDistance * 2;
        foreach (Pos pos in posList)
        {
            float dist = GetDistance(pos);
            if (dist < minLength)
            {
                minPos = pos;
            }
        }
        return minPos;
    }

    private float GetDistance(Pos pos)
    {
        return Mathf.Sqrt(Mathf.Pow(_currentX - pos.X, 2) + Mathf.Pow(_currentY - pos.Y, 2));
    }

    public void Mate(Animal animal)
    {
        if (Gender == Gender.Female)
        {
            IsPregnant = true;
            _animalDirection = -1;
            StartCoroutine(GiveBirth());
        }
    }

    IEnumerator GiveBirth()
    {
        yield return new WaitForSeconds(10f);
        if (CurrentEnergy >= 0)
        {
            AnimalManager.Instance.GiveBirthNewAnimal();
            IsPregnant = false;
            CurrentEnergy -= 50;

            if (CurrentEnergy <= 0)
            {
                print("DIED DURING BIRTH");
            }
        }
        else
        {
            print("DIED BEFORE BIRTH");
            Die();
        }

    }
}

public class EnvironmentScan
{
    public List<Pos> foodList = new List<Pos>();
    public List<Pos> mateList = new List<Pos>();
    public List<Pos> predetorList = new List<Pos>();
}

public class Pos {
    public int X;
    public int Y;

    public Pos(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }
}

public enum AnimalState
{
    RandomWalk,
    RunAway,
    Chase,
    GoToFood,
    EatFood,
    LookForFood,
    LookForMate,
    Mating
}

public enum DietType
{
    Carnivore,
    Herbivore
}

public enum Gender
{
    Male = 0,
    Female = 1
}

public enum Species
{
    Chicken
}