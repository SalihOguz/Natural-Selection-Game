using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Animal : MonoBehaviour
{
    public float WalkingSpeed = 1;
    private int currentX;
    private int currentY;

    private Vector3[,] cubePosList;

    public void Spanw()
    {
        cubePosList = MapGenerator.Instance.cubePosList;
        currentX = Random.Range(0, cubePosList.GetLength(0));
        currentY = Random.Range(0, cubePosList.GetLength(1));
        transform.position = cubePosList[currentX, currentY] + (Vector3.up / 2);
        gameObject.SetActive(true);

        StartCoroutine(Walk());
    }

    IEnumerator Walk()
    {
        int direction = GetDirection();

        yield return new WaitForSeconds(0.2f / WalkingSpeed);

        // change diraction
        if (direction == 0) // front
        {
            TurnForward();
        }
        if (direction == 1) // back
        {
            TurnBack();
        }
        if (direction == 2) // right
        {
            TurnRight();
        }
        if (direction == 3) // left
        {
            TurnLeft();
        }

        yield return new WaitForSeconds(0.3f / WalkingSpeed);

        if (direction == 0) // front
        {
            GoForward();
        }
        if (direction == 1) // back
        {
            GoBack();
        }
        if (direction == 2) // right
        {
            GoRight();
        }
        if (direction == 3) // left
        {
            GoLeft();
        }

        transform.DOMove(cubePosList[currentX, currentY] + (Vector3.up / 2), 0.2f);
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
    private int GetDirection()
    {
        int direction = Random.Range(0, 4);

        if (direction == 0) // front
        {
            if (currentY == cubePosList.GetLength(1) - 1)
            {
                direction = 1;
            }
        }
        if (direction == 1) // back
        {
            if (currentY == 0)
            {
               direction = 0;
            }
        }
        if (direction == 2) // right
        {
            if (currentX == cubePosList.GetLength(0) - 1)
            {
                direction = 3;
            }
        }
        if (direction == 3) // left
        {
            if (currentX == 0)
            {
                direction = 2;
            }
        }

        return direction;
    }
}
