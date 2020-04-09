using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : GameSystem
{
    /*So, the Health System takes in all the things that has
     HP. All of this information will be displayed in game, keeping track
     of everyone's health, that includes the Player's HP, and the Boss's HP.
     
     There will also be a UI portion of the system as well.*/

    public Slider playerHealthSlider;
    public float playerMaxHealth;
    public GameEntity playerEntity;


    public Slider bossHealthSlider;
    public float bossMaxHealth;
    public GameEntity bossEntity;

    protected override void Start()
    {
        //We give the max value for the boss, and the player.
        playerEntity.HPValue = playerMaxHealth;
        bossEntity.HPValue = bossMaxHealth;
    }

    protected override void Main()
    {
        ManagerHealthMeter();
    }

    /// <summary>
    /// Manages both the player's health, and the boss' health
    /// </summary>
    void ManagerHealthMeter()
    {
        playerHealthSlider.value = playerEntity.HPValue / playerMaxHealth;
        bossHealthSlider.value = bossEntity.HPValue / bossMaxHealth;
    }
}
