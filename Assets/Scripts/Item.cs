using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour, IPurchase, IUsable, IEquippable
{
    /*An item can be something that can heal you, replenish your mana, buff your stats, etc.
     
        You give a name of the item, the description, what stat or attribute it increase, or if
        it can be used on the enemy.*/

    public Image itemImage; 
}
