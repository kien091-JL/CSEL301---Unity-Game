using System;

[Serializable]
public class PlayerEntry
{
    public string name;
    public float time; // Store time as a float (seconds) for easy sorting

    public PlayerEntry(string name, float time)
    {
        this.name = name;
        this.time = time;
    }
}