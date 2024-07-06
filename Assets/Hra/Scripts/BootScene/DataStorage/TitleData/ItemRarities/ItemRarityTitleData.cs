using System;

[Serializable]
public class ItemRarityTitleData
{
    public RarityData[] RarityData;

    public ItemRarityTitleData(RarityData[] rarityData)
    {
        RarityData = rarityData;
    }
}
