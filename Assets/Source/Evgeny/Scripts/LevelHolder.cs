public static class LevelHolder
{
    private static int _currentLevel = 0;

    public static int CurrentLevel => _currentLevel;

    public static void SetLevel(int index)
    {
        _currentLevel = index;
    }
}