using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;

public class SaveCoins : MonoBehaviour
{
    public static SaveCoins instance { get; private set; }

    //What we want to save
    public int currentWeapon;
    public int money;
    public static bool[] WeaponUnlocked = new bool[10] { true, false, false, false, false, false, false, false, false, false };

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;

        DontDestroyOnLoad(gameObject);
        Load();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData_Storage data = (PlayerData_Storage)bf.Deserialize(file);

            money = data.money;
            
            WeaponUnlocked = data.WeaponUnlocked;

            if (data.WeaponUnlocked == null)
                WeaponUnlocked = new bool[10] { true, false, false, false, false, false, false, false, false, false };

            file.Close();
        }
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData_Storage data = new PlayerData_Storage();

        data.currentWeapon = currentWeapon;
        data.money = money;
        data.WeaponUnlocked = WeaponUnlocked;

        bf.Serialize(file, data);
        file.Close();
    }
}

[Serializable]
class PlayerData_Storage
{
    public int currentWeapon;
    public int money;
    public bool[] WeaponUnlocked;
}

