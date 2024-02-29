using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomCursor : MonoBehaviour
{
    private ItemSO item;
    private Image _image;

    private void Start()
    {
        _image = GetComponent<Image>();
        if (item != null) {
            _image.sprite = item.sprite;
            _image.color = new Color(255, 255, 255, 1);
        }
        else _image.color = new Color(255, 255, 255, 0);
    }

    private void Update()
    {
        transform.position = Input.mousePosition;
    }
    
    /// <summary>
    /// Sprawdza czy CustomCursor ItemSO item nie jest null
    /// </summary>
    /// <returns>bool czy ItemSO item nie jest null</returns>
    public bool IsHaveItem() { return item is not null; }
    
    /// <summary>
    /// Ustawia ItemS0 item na i.
    /// </summary>
    /// <param name="i"></param>
    public void setItem(ItemSO i) {
        item = i;
        _image.sprite = item.sprite;
        _image.color = new Color(255, 255, 255, 1);
    }

    /// <summary>
    /// Zwraca ItemS0 item na i, zmienia item na null.
    /// </summary>
    /// <returns>ItemS0 item</returns>
    public ItemSO takeItem() {
        ItemSO old = item;
        item = null;
        _image.color = new Color(255, 255, 255, 0);
        return old;
    }
}
