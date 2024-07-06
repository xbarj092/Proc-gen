using System;

[Serializable]
public class RarityData
{
    public string Type;
    public float Rarity;

    public RarityData(string type, float rarity)
    {
        Type = type;
        Rarity = rarity;
    }
}
