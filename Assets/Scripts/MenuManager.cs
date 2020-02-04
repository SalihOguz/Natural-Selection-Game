﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Text MyAnimalName;
    public Text MyAnimalCount;
    public Text ChickenAnimalCount;
    public Image PopulationBar;
    public Text Percentage;
    public GameObject InGameScreen;
    public GameObject SetupScreen;

    public InputField AnimalName;
    public InputField UserSpeedInput;
    public InputField  UserStrengthInput;
    public InputField UserSenseInput;
    public Toggle UserPlantToggle;
    public Text UserEnergyCost;

    public InputField EnemySpeedInput;
    public InputField  EnemyStrengthInput;
    public InputField EnemySenseInput;
    public Toggle EnemyPlantToggle;
    public Text EnemyEnergyCost;

    public HexMapCamera hexCamera;
    public GameObject WinScreen;
    public GameObject GameOverScreen;

    bool isStarted = false;
    float refreshTime = 1f;
    float currentRefreshProgress = 1f;

    private void Start() {
        
    }

    public void Speedup()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 5;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("Main");
    }

    public void StartGame()
    {
        InGameScreen.SetActive(true);
        SetupScreen.SetActive(false);

        MyAnimalName.text = AnimalName.text;
        AnimalManager.Instance.AnimalPrefabList[0].GetComponent<Animal>().Speed = int.Parse(UserSpeedInput.text);
        AnimalManager.Instance.AnimalPrefabList[0].GetComponent<Animal>().Strength = int.Parse(UserStrengthInput.text);
        AnimalManager.Instance.AnimalPrefabList[0].GetComponent<Animal>().SenseDistance = int.Parse(UserSenseInput.text);
        AnimalManager.Instance.AnimalPrefabList[0].GetComponent<Animal>().DietType = GetUserDietType();

        AnimalManager.Instance.AnimalPrefabList[1].GetComponent<Animal>().Speed = int.Parse(EnemySpeedInput.text);
        AnimalManager.Instance.AnimalPrefabList[1].GetComponent<Animal>().Strength = int.Parse(EnemyStrengthInput.text);
        AnimalManager.Instance.AnimalPrefabList[1].GetComponent<Animal>().SenseDistance = int.Parse(EnemySenseInput.text);
        AnimalManager.Instance.AnimalPrefabList[1].GetComponent<Animal>().DietType = GetEnemyDietType();

        hexCamera.enabled = true;

        AnimalManager.Instance.SpawnAnimals();
        isStarted = true;
    }

    private DietType GetUserDietType()
    {
        if (UserPlantToggle.isOn)
        {
            return DietType.Herbivore;
        }
        else
        {
            return DietType.Carnivore;
        }
    }

    private DietType GetEnemyDietType()
    {
        if (EnemyPlantToggle.isOn)
        {
            return DietType.Herbivore;
        }
        else
        {
            return DietType.Carnivore;
        }
    }

    private void Update() {
        currentRefreshProgress += Time.deltaTime;

        if (currentRefreshProgress >= refreshTime)
        {
            currentRefreshProgress = 0;

            int[] counts = AnimalManager.Instance.GetAnimalCounts();
            MyAnimalCount.text = counts[0].ToString();
            ChickenAnimalCount.text = counts[1].ToString();

            float perc = (float)counts[0] / ((float)counts[0] + (float)counts[1]);
            PopulationBar.fillAmount = perc;
            Percentage.text = perc.ToString("P0");

            if (isStarted)
            {
                if (perc > 0.85f)
                {
                    WinScreen.SetActive(true);
                }
                else if (counts[0] == 0)
                {
                    GameOverScreen.SetActive(true);
                }
            }
        }
    }

    public void UserDataChanged()
    {
        try
        {
            float energy = (Mathf.Pow(float.Parse(UserStrengthInput.text), 2) * Mathf.Pow(float.Parse(UserSpeedInput.text), 2) + int.Parse(UserSenseInput.text)) / 5;
            UserEnergyCost.text = "Movement Energy Cost: " + Math.Round(energy, 2);
        }
        catch
        {

        }
    }

    public void EnemyDataChanged()
    {
        try
        {
            float energy = (Mathf.Pow(float.Parse(EnemyStrengthInput.text), 2) * Mathf.Pow(float.Parse(EnemySpeedInput.text), 2) + int.Parse(EnemySenseInput.text)) / 5;
            EnemyEnergyCost.text = "Movement Energy Cost: " + Math.Round(energy, 2);
        }
        catch
        {

        }
    }
}
