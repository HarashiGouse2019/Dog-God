﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stats : MonoBehaviour
{
    /*Mainly for the Player, they are given these kinds of stats:
        Strength
        Vitality
        Dexterity
        Endurance
        Critical
        Faith
        Intelligence
        Luck

        For Weapons, it varies, such as how much damage they have
        the require status to wield it one handed, two handed, or dual-wield it,
        consider it considers that option for you,
         */

    //The reason that we have StatProperty is simple because stats can vary depending on the object.
    //Stat Property can either be damage, health, mana, all player stats, etc.
    //Having it this way will allow us to access the property by name, and retreive it.
    public List<StatProperty> properties;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
