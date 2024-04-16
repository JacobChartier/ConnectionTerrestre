using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugItem : Item
{
    protected override void Load()
    {
        Name = "<color=#FF0000>[Debug Item]</color>";

        Category = Category.DEBUG;
        Rarety = Rarety.DEBUG;
    }
}
