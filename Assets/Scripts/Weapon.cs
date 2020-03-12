using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IEquippable, IPurchase
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
