using UnityEngine;

public static class TutorialPrefs
{
    private const string Key = "HasSeenPickupTutorial";

    public static bool HasSeenPickupTutorial
    {
        get => PlayerPrefs.GetInt(Key, 0) == 1;
        set => PlayerPrefs.SetInt(Key, value ? 1 : 0);
    }
}
