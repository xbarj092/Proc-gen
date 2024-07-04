using UnityEngine;

public class LocalDataStorage : MonoSingleton<LocalDataStorage>
{
    [field: SerializeField] PlayerData PlayerData;
    [field: SerializeField] GameData GameData;
}
