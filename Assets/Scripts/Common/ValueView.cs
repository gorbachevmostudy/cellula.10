using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueView<T> : MonoBehaviour where T : IConvertible
{
    [SerializeField] private Text _text;

    public void Show(T value)
    {
        gameObject.SetActive(true);
        _text.text = value.ToString();
    }

    public void Hide() => gameObject.SetActive(false);
}
