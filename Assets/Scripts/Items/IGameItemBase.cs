using System.Collections;
using UnityEngine;

public interface IGameItemBase
{
    public void Use();

    public void SetData(string name = default, Category category = default, Rarety rarety = default);
}