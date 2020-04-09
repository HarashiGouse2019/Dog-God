using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    /*The id of the weapon so that we can use it to register into the
     WeaponsLog List!!!*/
    public bool Obtained { get; set; } = false;

    //The description or lore of the weapon.
    [TextArea(minLines: 1, maxLines: 10)]
    public string caption;

    //Weapon stats (these states will be added to the players)
    [Dependency] public WeaponStats stats;

    //Requirements that the player has to have in order to wield it.
    public Requirement[] requirements;

    //And an image associated with a weapon
    public Texture2D weaponImage;

    //Damage Type
    public enum WeaponDamageType
    {
        NONE = 0,
        BURNING = 1 << 0,
        FREEZING = 1 << 1,
        POISON = 1 << 2,
        LIGHTNING = 1 << 3,
        LIGHT = 1 << 4,
        DARK = 1 << 5,
        ONEHAND = 1 << 6,
        TWOHAND = 1 << 7,
        DUALWIELD = 1 << 8
    }

    public WeaponDamageType weaponDamageType;

    public void Test()
    {
        //Assuming that our straight sword has One-Hand damage, we can also make it take Lightning damage as well.
        weaponDamageType = weaponDamageType | WeaponDamageType.LIGHTNING;
        
        if((weaponDamageType & WeaponDamageType.LIGHTNING) == WeaponDamageType.LIGHTNING)
        {
            //This means that the bit in our weapons is 1,
            //meaning that our weapon does add lightning damage when
            //in use.
        }

        //We don't like our build. We'll remove lightning damage from our
        //straight sword.
        weaponDamageType &= ~WeaponDamageType.LIGHTNING;

        //We'll toggle it on or off.
        weaponDamageType ^= WeaponDamageType.LIGHTNING; 
    }
}
