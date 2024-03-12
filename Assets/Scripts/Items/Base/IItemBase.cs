using System.Collections;
using UnityEngine;

public interface IItemBase
{
    public delegate void OnDataChange();

    public void Use();
}