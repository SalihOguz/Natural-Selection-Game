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
        yield return new WaitForSeconds(0.5f / WalkingSpeed);

        int direction = Random.Range(0, 4);

        if (direction == 0) // front
        {
            if (currentY == cubePosList.GetLength(1) - 1)
            {
                GoBack();
            }
            else
            {
                GoForward();
            }
        }
        if (direction == 1) // back
        {
            if (currentY == 0)
            {
                GoForward();
            }
            else
            {
                GoBack();
            }
        }
        if (direction == 2) // right
        {
            if (currentX == cubePosList.GetLength(0) - 1)
            {
                GoLeft();
            }
            else
            {
                GoRight();
            }
        }
        if (direction == 3) // left
        {
            if (currentX == 0)
            {
                GoRight();
            }
            else
            {
                GoLeft();
            }
        }

        transform.DOMove(cubePosList[currentX, currentY] + (Vector3.up / 2), 0.2f);
        StartCoroutine(Walk());
    }

    private void GoForward()
    {
        currentY++;
        transform.DORotate(Vector3.zero, 0.2f);
    }

    private void GoBack()
    {
        currentY--;
        transform.DORotate(new Vector3(0, 180f, 0), 0.2f);
    }

    private void GoRight()
    {
        currentX++;
        transform.DORotate(new Vector3(0, 90f, 0), 0.2f);
    }

    private void GoLeft()
    {
        currentX--;
        transform.DORotate(new Vector3(0, -90f, 0), 0.2f);
    }
}
