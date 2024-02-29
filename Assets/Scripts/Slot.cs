using System;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour {
    [SerializeField] protected ItemSO item;
    protected Image _image;

    private void Start() {
        _image = transform.GetChild(0).GetComponent<Image>();
        if (item != null) {
            _image.sprite = item.sprite;
            _image.color = new Color(255, 255, 255, 1);
        }
        else _image.color = new Color(255, 255, 255, 0);
    }

    /// <summary>
    /// Zamiania aktualnego item na i
    /// </summary>
    /// <param name="i"></param>
    /// <returns>Zwraca aktualny item</returns>
    public ItemSO changeItem(ItemSO i) {
        if (i is not null) {
            _image.sprite = i.sprite;
            _image.color = new Color(255, 255, 255, 1);
        }
        else _image.color = new Color(255, 255, 255, 0);
        ItemSO old = item;
        item = i;
        return old;
    }

    /// <summary>
    /// Sprawdza czy Slot ItemSO item nie jest null
    /// </summary>
    /// <returns>bool czy ItemSO item nie jest null</returns>
    public bool IsHaveItem() { return item is not null; }
    
    /// <summary>
    /// Zwraca item.id. Kiedy item jest null zwraca 0
    /// </summary>
    /// <returns>int item.id lub 0</returns>
    public int GetItemId() {
        if (item is null) return 0;
        return item.id;
    }
}
