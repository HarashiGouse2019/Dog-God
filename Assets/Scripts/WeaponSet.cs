using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cakewalk.IoC;

public class WeaponSet : MonoBehaviour, IObjectSet
{
    /*WeaponSet operates exactly like SkillSet, but mainly concerning
     the player and deities. The Player and Deities have their own weapon set
     but when the Player signs contracts with Deities, they are able to use their
     weapons as well.*/

    public List<Weapon> weapons;

    public void PassOn()
    {

    }

    public void Append()
    {

    }
    public void Remove()
    {

    }
    public void Clear()
    {

    }
}
