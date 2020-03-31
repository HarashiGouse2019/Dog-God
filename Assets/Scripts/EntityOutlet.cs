using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityOutlet : MonoBehaviour
{
    /*EntityOutlet is a sort of "Container" or "Handler" for GameEntity scriptable objects.*/

    public GameEntity gameEntity;

    /*All of these values are made to be private, taking all values from whatever srcriptable object
     you take in.*/
    public GameEntity.EntityType entityType;

    public string entityName;

    public float hp, mp, levelRank;

    public Stats stats;

    public SkillSet skills;

    public WeaponSet weapons;


    void AddNewSkillToSkillSet()
    {
        
    }

    void AddNewWeaponToWeaponSet()
    {

    }

    void UpgradeHP()
    {

    }

    void IncreaseHP()
    {

    }

    void DecreaseHP()
    {

    }

    void UpgradeMP()
    {

    }

    void IncreaseMP()
    {

    }

    void DecreaseMP()
    {

    }

    void UpgradeLevel()
    {

    }

    void IncreaseExperience()
    {

    }

    void DecreaseExperience()
    {

    }

    void ResetAllAttributes()
    {

    }
}
