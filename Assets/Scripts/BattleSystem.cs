using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum OpenOptions
{
    SKILL,
    ITEM,
    WEAPONS
}

public class BattleSystem : GameSystem, IOpenItemList, IOpenSkillList, IOpenWeaponList
{
    /*The battle system is the transaction between the player, as well as the boss.
     The battle system will include the following functions:
        Interacting with attack. This should then have a value changed stating that attacking phase has
        commenced.
        
        Interacting with defend. This should have us not attack, and allow our opponent to attack. We might need have a seperate script
        or implement our Dog God different than our Player in which it picks out it's move based on the situation. Attacking without a skill chain 
        is just a standard attack.
        
        Interact with our skill list. Depending on our AP, we can create a Chain Attack, where we attack more than once, allowing us to deliver
        more damage. As soon as the AP count is 0, we can't use any more skills. The player will then have to ATTACK in order to use the chain.

        Interacting with our item list. You can stash it in two of the 4 slots surrounding the player options. However, using an item
        will require 1 turn from you.

        Equip Weapons and Items. You have 4 slots on both sides on the screen. The top 2 slots are for weapons, while the bottom 2 are mainly
        for items that you can stash.


         */


    private static BattleSystem Instance;
    //Our button options on the main player interface.
    [Header("Options")]
    public Button skillButton;
    public Button itemButton;
    public Button attackButton;
    public Button defendButton;


    //Our slots on the left and right side of our main player interface
    [Header("Slots")]
    public Button slot1;
    public Button slot2;
    public Button slot3;
    public Button slot4;

    //List of entities in battle. In all honestly, it's just the player and the boss
    public List<GameEntity> entities;

    public static float DamageValue { get; private set; }

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        } else
        {
            Destroy(gameObject);
        }
    }

    protected override void Main()
    {
        List<string> notes = new List<string>();
        notes[0] = "Try me!";
    }

    /// <summary>
    /// Open an option.
    /// </summary>
    /// <param name="option"></param>
    void Open(OpenOptions option)
    {
        switch (option)
        {
            case OpenOptions.SKILL: OpenSkillList(); break;

            case OpenOptions.ITEM: OpenItemList(); break;

            case OpenOptions.WEAPONS: OpenWeaponList(); break;

            default: break;
        }
    }

    /// <summary>
    /// Opens the Skill List on Command
    /// </summary>
    public void OpenSkillList()
    {
        Open(OpenOptions.SKILL);
    }

    /// <summary>
    /// Opens the Item List on Command
    /// </summary>
    public void OpenItemList()
    {
        Open(OpenOptions.ITEM);
    }

    /// <summary>
    /// Opens the Weapon List on Command
    /// </summary>
    public void OpenWeaponList()
    {
        Open(OpenOptions.WEAPONS);
    }

    /// <summary>
    /// Signify that the player is ready to attack.
    /// </summary>
    public void SignalToAttack()
    {

    }


    /// <summary>
    /// Signify that the player is going to defend.
    /// </summary>
    public void SignalToDefend()
    {

    }

    /// <summary>
    /// A simple command from the player to attack the boss.
    /// </summary>
    public void Attack()
    {
        /*We need to first create the stats for the player. Major thing to do
         first before continuing this is to create the "Title Screen" where you create
         a profile, you name it, and you use 100 sp to distribute to your existing 
         stats. We'll be relying on all the stats. We'll have to see what weapon we're using to 
         so that we can add that value to our DamageValue. This is going to be a lot, but that's the initial plan.*/
        SendDamageTo(entities[1]);
    }

    /// <summary>
    /// We can tell damage to anything that is damaagable or is
    /// in the battle field.
    /// </summary>
    /// <param name="_entity">The entity to deliver damage to.</param>
    public void SendDamageTo(GameEntity _entity)
    {
        _entity.HPValue -= DamageValue;
    }

    /// <summary>
    /// Updates the damage value to be sent to the target.
    /// </summary>
    /// <param name="_value">The amount of damage</param>
    public void UpdateDamageValue(float _value)
    {
        DamageValue = _value;
    }
}
