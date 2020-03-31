using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cakewalk.IoC;

public abstract class GameEntity : MonoBehaviour
{
    /*GameEntity is the actual objects that are in the Battle Scene. That would most likely be
     you and Dog God. Anything that can take damage and use skills are included as a Game Entity.
     
     You give the name for the game entity, define the entity's stats, skills, weapon of use, hp, level, and mana.*/

    public enum EntityType
    {
        PLAYER,
        BOSS,
        DEITY
    }

    public string entityName;

    public EntityType entityType;

    [Header("HP")]
    public float HPValue;

    [Header("Mana")]
    public float MPValue;

    [Header("Level")]
    public float LevelRankValue;

    [Header("Stats")]
    public Stats stats;

    [Header("Skills / Divine Prowess")]
    public SkillSet skills;

    [Header("Weapons")]
    public WeaponSet weapons;
}
