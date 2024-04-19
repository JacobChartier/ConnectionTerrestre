using System;
using System.Collections;
using UnityEngine;

public interface IItemBase
{
    public delegate void OnDataChange();

    [Obsolete]
    public void Use();

    public void Use(Scenes scene);
}