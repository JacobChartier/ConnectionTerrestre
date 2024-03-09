using System.Collections;
using UnityEngine;

public interface IGameItemBase
{
    public delegate void OnDataChange();

    public void Use();

    public void SetData(string name = default, Category category = default, Rarety rarety = default);
}