using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Level Data/Areas", fileName = "New Area")]
public class AreaData : ScriptableObject
{
    public string AreaName;
    public List<LevelData> Levels = new List<LevelData>();
}
