using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeSO", menuName = "ScriptableObjects/RecipeSO")]
public class RecipeSO : ScriptableObject
{
    /// <summary>
    /// Receptura składa się z listy z 9 integerami reprezentującymi id przedmiotów, które wchodzą w jej skład.
    /// W crafingu 0 pole to górny lewy róg, 2 górny prawy róg i 3 to środkodek lewo.
    /// Wartość 0 dla pustego pola
    /// </summary>
    public List<int> recipe = new List<int>();
    public ItemSO result;
}
