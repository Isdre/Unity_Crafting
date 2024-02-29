using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    [SerializeField] private Transform recipe;
    [SerializeField] private Transform inventory;
    [SerializeField] private Slot result;
    [SerializeField] private CustomCursor cursor;

    [SerializeField] private List<RecipeSO> recipes = new List<RecipeSO>();
    
    [SerializeField] private float maxDistanceFromSlot = 50f;

    private List<Slot> _invetorySlots = new List<Slot>();
    private Slot[] _recipeSlots;

    private Slot _closestSlot = null;
    private bool _inResult;
    private bool _inRecipe;
    private float _minDistance;
    private float _distance;

    private void Start() {
        _recipeSlots = new Slot[9];
        foreach(Transform child in inventory)
        {
            _invetorySlots.Add(child.GetComponent<Slot>());
        }
        
        foreach(Transform child in recipe)
        {
            RecipeSlot s = child.GetComponent<RecipeSlot>();
            _recipeSlots[s.id] = s;
        }
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            OnClick();
        }
    }

    private void OnClick() {
        _inResult = false;
        _inRecipe = false;
        _minDistance = maxDistanceFromSlot;
        
        foreach (Slot s in _invetorySlots) {
            _distance = Vector3.Distance(cursor.transform.position, s.transform.position);
            if (_distance < _minDistance) {
                _minDistance = _distance;
                _closestSlot = s;
            }
        }

        foreach (Slot s in _recipeSlots) {
            _distance = Vector3.Distance(cursor.transform.position, s.transform.position);
            if (_distance < _minDistance) {
                _minDistance = _distance;
                _closestSlot = s;
                _inRecipe = true;
            }
        }
        
        _distance = Vector3.Distance(cursor.transform.position, result.transform.position);
        if (_distance < _minDistance) {
            _minDistance = _distance;
            _closestSlot = result;
            _inRecipe = false;
            _inResult = true;
        }
        
        if (_minDistance == maxDistanceFromSlot) return;
        
        ItemSO i;
        
        if (_inResult && cursor.IsHaveItem()) SetBackToInventory(cursor.takeItem());
        i = _closestSlot.changeItem(cursor.takeItem());
        if (i is not null) cursor.setItem(i);
        if (_inRecipe) CheckRecipe();
    }
    
    private void CheckRecipe() {
        bool canCraft = false;
        ItemSO resultSO = null;
        String craft = "";
        foreach (Slot s in _recipeSlots) {
            if (s.IsHaveItem()) craft += s.GetItemId().ToString();
            else craft += "0";
        }
        
        foreach (var r in recipes) {
            if (String.Join("", r.recipe.Select(x => x.ToString()).ToArray()).Equals(craft)) {
                resultSO = r.result;
                canCraft = true;
                break;
            }
        }

        if (canCraft) {
            ItemSO i;
            i = result.changeItem(resultSO);
            if (result.IsHaveItem()) SetBackToInventory(i);
            foreach (Slot s in _recipeSlots) i = s.changeItem(null);
        }
        
        
    }
    
    private void SetBackToInventory(ItemSO i) {
        foreach (Slot s in _invetorySlots) {
            if (!s.IsHaveItem()) {
                s.changeItem(i);
                return;
            }
        }
    }
}
