using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour, IPurchase, IUsable
{
    /*An item can be something that can heal you, replenish your mana, buff your stats, etc.
     
        You give a name of the item, the description, what stat or attribute it increase, or if
        it can be used on the enemy.*/

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
