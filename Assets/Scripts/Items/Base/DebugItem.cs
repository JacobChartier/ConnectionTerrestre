using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Items
{
    public class DebugItem : Item
    {
        public DebugItem()
        {
            Name = "Test Item";
            Category = Category.DEBUG;
        }

        public override void Use()
        {
            Debug.Log($"{Name} ({typeof(DebugItem)}) has been used.");
        }

        protected override void Load()
        {
            Icon = Resources.Load<Sprite>("Sprites/DebugIcon");
        }
    }
}