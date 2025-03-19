using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Интерфейс, указывающий, что пыль может осесть на этот объект и вызвать метод.
/// </summary>
public interface IDustySurface
{
    /// <summary>
    /// Метод, вызываемый при оседании пыли на объект
    /// </summary>
    public void BecomeDusty();
}