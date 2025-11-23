using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

// Bu scripte oyuncunun can verilerini tutmak için dosya iþlemleri yazýlmýþtýr.

public class fileManagerHealth : MonoBehaviour
{

    public Slider healthSlider;  
    
    public float maxHealth = 100f;
    public float currentHealth = 100f;

    string folderPath;
    string filePath;

    private void Start()
    {
        folderPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "2D_RPG_GAME");
        filePath = Path.Combine(folderPath, "gameData(Health).txt");


        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        loadData();

        updateHealthUI();
    }

    public void createFile()
    {
        saveData();
    }

    public void saveData()
    {
        string dataToSave = "Oyuncunun_Cani : " + currentHealth.ToString();
        File.WriteAllText(filePath, dataToSave);
    }

    public void loadData()
    {
        if (!File.Exists(filePath))
        {
            currentHealth = maxHealth; 
            return;
        }

        string loadedData = File.ReadAllText(filePath);

        if (loadedData.StartsWith("Oyuncunun_Cani : "))
        {
            string healthString = loadedData.Replace("Oyuncunun_Cani : ", "");
            if (float.TryParse(healthString, out float loadedHealth))
            {
                currentHealth = Mathf.Clamp(loadedHealth, 0, maxHealth);
            }
            else
            {
                currentHealth = maxHealth;
            }
        }
    }
    private void OnApplicationQuit()
    {
        saveData();
    }

    public void updateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth / maxHealth;
        }
    }
}
