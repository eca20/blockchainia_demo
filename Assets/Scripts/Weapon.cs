using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string name;
    public int ID;
    public FireMode fireMode;

    public void Shoot(){
        
    }

    public enum FireMode
    {
        Semi,
        Auto,
        Burst
    }
}
