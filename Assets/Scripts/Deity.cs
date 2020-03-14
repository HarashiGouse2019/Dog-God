using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Deity : MonoBehaviour
{
    /*This class will include contain the name of the deity,
     the amount of Faith you must have in order to sign a contract with them,
     rather you've made a contract with them or not,
     they're HP, Mana, and Stats, as well as the skills that you can use
     once you have signed a contract.*/

    //The name of the weapon
    public string m_name;

    //The description or lore of the weapon.
    public string caption;

    //Weapon stats (these states will be added to the players)
    public Stats stats;

    //Requirements that the player has to have in order to wield it.
    public Requirement[] requirements;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
