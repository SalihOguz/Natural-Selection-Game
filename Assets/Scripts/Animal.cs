using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Animal : MonoBehaviour
{
    public float WalkingSpeed = 1;
    public float MaxEnergy = 5;
    
    [HideInInspector]
    public float EnergyAmount;
    




    private int currentX;
    private int currentY;

    private int rowCount;
    private int columnCount;

    private int animalDirection;

    public void Spanw()
    {
        rowCount = MapGenerator.Instance.rowCount;
        columnCount = MapGenerator.Instance.columnCount;

        GetValidPositionToSpawn();
        transform.position = MapGenerator.cubeDataList[currentX, currentY].pos + (Vector3.up / 2);
        gameObject.SetActive(true);

        MapGenerator.cubeDataList[currentX, currentY].standingAnimal = this;

        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(Random.Range(0, 0.4f));
        GetNewRandomDirection();
        StartCoroutine(Walk());
    }

    IEnumerator Walk()
    {
        if (Random.Range(0f,3f) <= 1.2f)
        {
            GetNewRandomDirection();
        }
        CheckDirectionValidation();

        yield return new WaitForSeconds(0.2f / WalkingSpeed);

        // change diraction
        if (animalDirection == 0) // front
        {
            TurnForward();
        }
        if (animalDirection == 1) // back
        {
            TurnBack();
        }
        if (animalDirection == 2) // right
        {
            TurnRight();
        }
        if (animalDirection == 3) // left
        {
            TurnLeft();
        }

        yield return new WaitForSeconds(0.2f / WalkingSpeed);
        MapGenerator.cubeDataList[currentX, currentY].standingAnimal = null;


        if (animalDirection == 0) // front
        {
            GoForward();
        }
        if (animalDirection == 1) // back
        {
            GoBack();
        }
        if (animalDirection == 2) // right
        {
            GoRight();
        }
        if (animalDirection == 3) // left
        {
            GoLeft();
        }

        MapGenerator.cubeDataList[currentX, currentY].standingAnimal = this;

        Vector3 destination = MapGenerator.cubeDataList[currentX, currentY].pos + (Vector3.up / 2);        
        Vector3 midPoint = (transform.position + destination) / 2 + new Vector3(0, 0.5f, 0);
        Vector3[] pathWaypoints = new[] {midPoint, destination};

        transform.DOPath(pathWaypoints, 0.3f).SetEase(Ease.OutSine);
        StartCoroutine(Walk());
    }

    private void GoForward()
    {
        currentY++;     
    }

    private void GoBack()
    {
        currentY--;  
    }

    private void GoRight()
    {
        currentX++;
    }

    private void GoLeft()
    {
        currentX--;
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
    private void GetNewRandomDirection()
    {
        animalDirection = Random.Range(0, 4);
    }

    private void CheckDirectionValidation()
    {
        if (animalDirection == 0 && IsCubeValid(currentX, currentY + 1)) // front
        {
            return;
        }
        if (animalDirection == 1 && IsCubeValid(currentX, currentY - 1)) // back
        {
            return;
        }
        if (animalDirection == 2 && IsCubeValid(currentX + 1, currentY)) // right
        {
            return;
        }
        if (animalDirection == 3 && IsCubeValid(currentX - 1, currentY)) // left
        {
            return;
        }

        GetNewRandomDirection();
        CheckDirectionValidation();
    }

    private bool IsCubeValid(int x, int y)
    {
       
        if (animalDirection == 0) // front
        {
            if (currentY == columnCount - 2)
            {
                return false;
            }
        }
        if (animalDirection == 1) // back
        {
            if (currentY == 0)
            {
               return false;
            }
        }
        if (animalDirection == 2) // right
        {
            if (currentX == rowCount - 2)
            {
                return false;
            }
        }
        if (animalDirection == 3) // left
        {
            if (currentX == 0)
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
            currentX = Random.Range(0, rowCount - 2);
            currentY = Random.Range(0, columnCount - 2);

            CubeData cubeData = MapGenerator.cubeDataList[currentX, currentY];
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
    Mate
}