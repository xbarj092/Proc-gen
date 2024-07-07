using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ItemEffect : MonoBehaviour
{
    [SerializeField] private TMP_Text _effectText;
    [SerializeField] private Image _effectImage;

    public void Init(Sprite sprite, string text)
    {
        _effectText.text = text;
        _effectImage.sprite = sprite;
    }
}
