using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cakewalk.IoC;

public abstract class Weapon : ScriptableObject, IEquippable, IPurchase
{
    /*Weapons will include a name, as well as if they are one of the following kinds
        Sword
        Great Sword
        Shield
        Great Shield
        Wand
        Staff
        Great Staff
        Gun
        Rifle
        Bow
        Great Bow

        They will have the following Stats

        As well as have either 1 or 2 skills that make up their Divine Prowess
         */

    //The name of the weapon
    public string m_name;

    //The description or lore of the weapon.
    [TextArea(minLines: 1, maxLines: 10)]
    public string caption;

    //Weapon stats (these states will be added to the players)
    [Dependency] public Stats stats;

    //Requirements that the player has to have in order to wield it.
    public Requirement[] requirements;

}
