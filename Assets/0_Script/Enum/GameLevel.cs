
[System.Flags]
public enum GameLevel 
{
    None,
    Level1 = 1 << 0, // 1
    Level2 = 1 << 1, // 2
    Level3 = 1 << 2, // 4

    All = Level1 | Level2 | Level3 // 7
}
