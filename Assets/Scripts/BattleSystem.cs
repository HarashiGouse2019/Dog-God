public class BattleSystem : GameSystem
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
