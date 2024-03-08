using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItemView : MonoBehaviour, IPointerClickHandler
{
    public event Action<ShopItemView> Click;

    [SerializeField] private Sprite _standardBackground;
    [SerializeField] private Sprite _highLightBackground;

    [SerializeField] private Image _contentImage;
    [SerializeField] private Image _lockImage;

    [SerializeField] private IntValueView _priceView;

    [SerializeField] private Image _selectionText;

    private Image _backgroundImage;

    public ShopItem Item { get; private set; }

    public bool IsLock { get; private set; }

    public int Price => Item.Price;

    public GameObject Model => Item.Model;

    public void Initialize(ShopItem item)
    {
        _backgroundImage = GetComponent<Image>();
        _backgroundImage.sprite = _standardBackground;

        Item = item;

        _contentImage.sprite = item.Image;

        _priceView.Show(Price);
    }

    public void OnPointerClick(PointerEventData eventData) => Click?.Invoke(this);

    public void Lock()
    {
        IsLock = true;
        _lockImage.gameObject.SetActive(IsLock);
        _priceView.Show(Price);
    }

    public void UnLock()
    {
        IsLock = false;
        _lockImage.gameObject.SetActive(IsLock);
        _priceView.Hide();
    }

    public void Select() => _selectionText.gameObject.SetActive(true);
    public void UnSelect() => _selectionText.gameObject.SetActive(false);

    public void HighLight() => _backgroundImage.sprite = _highLightBackground;

    public void UnHighLight() => _backgroundImage.sprite = _standardBackground;
}