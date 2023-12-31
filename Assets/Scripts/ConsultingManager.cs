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
    public static void AddFish() { Instance.fish_count++; }
    public static void SubFish() { Instance.fish_count -= Instance.fish_count == 0 ? 0 : 1; }
    public static int GetFish() { return Instance.fish_count; }
}
