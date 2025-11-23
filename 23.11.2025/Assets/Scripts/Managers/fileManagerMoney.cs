using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

//Bu scripte oyuncu verilerini tutmak için dosya iþlemleri yazýlmýþtýr.

public class fileManagerMoney : MonoBehaviour
{
    public TMP_Text moneyText;
    public int money = 0;

    string folderPath;
    string filePath;
    private void Start()
    {
        folderPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "2D_RPG_GAME");
        filePath = Path.Combine(folderPath, "gameData(Money).txt");

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath); // Eðer klasör yoksa oluþturudk oyuncu ilk giridðidne klasör yok çünkü
        }

        loadData();

        loadMoneyUI();
    }
    public void createFile()
    {
        saveData();
    }

    public void saveData()
    {
        string dataToSave = "Oyuncunun_Parasi : " + money.ToString();

        File.WriteAllText(filePath, dataToSave);
    }

    public void loadData()
    {
        if (!File.Exists(filePath))
        {
            return;
        }

        string loadedData = File.ReadAllText(filePath);

        if (loadedData.StartsWith("Oyuncunun_Parasi : "))
        {

            string moneyString = loadedData.Replace("Oyuncunun_Parasi : ", "");

            money = int.Parse(moneyString);

        }
    }

    //Oyun kapanýnca bütün verileri kaydediyoruz.
    private void OnApplicationQuit()
    {
        saveData();
    }

    //Parayý dosaydan okuduk ve ui üzerindeki texte yazdýk.
    public void loadMoneyUI()
    {

        if (moneyText != null)
        {

            moneyText.text = money.ToString();

        }

    }
}