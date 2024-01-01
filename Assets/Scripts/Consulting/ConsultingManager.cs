using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class ConsultingManager
{
    private static ConsultingManager instance;
    public static ConsultingManager Instance
    {
        get
        {
            if(instance == null) instance = new ConsultingManager();
            return instance;
        }
    }

    int fish_count = 0;
    int Chance = 10;
    public static void AddFish() { Instance.fish_count++; }
    public static int GetFish() { return Instance.fish_count; }
    public static void SubChance() 
    { 
        Instance.Chance -= 1;
        if (Instance.Chance < 0)
        {
            Debug.Log("FAIL");
        }
    }
    public static int GetChance() { return Instance.Chance; }
}