using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelHolder
{
    private static int _currentLevel = 1;

    public static int CurrentLevel => _currentLevel;
}
