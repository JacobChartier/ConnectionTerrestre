using System.Collections;
using Unity.XR.OpenVR;
using UnityEngine;

namespace Assets.Scripts.Items
{
    public class DebugItem : Item
    {
        public DebugItem()
        {
            Name = "Debug Item";
            Category = Category.DEBUG;
        }

        public override void Use()
        {
            Debug.Log($"{Name} ({typeof(DebugItem)}) has been used.");
        }

        protected override void LoadAssets()
        {
            Icon = Resources.Load<Sprite>("Sprites/DebugIcon");
        }

        protected override void GenerateData()
        {
            base.GenerateData();
        }
    }
}