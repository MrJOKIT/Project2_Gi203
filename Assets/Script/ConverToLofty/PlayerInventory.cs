using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private int _runePoint;

    public int RunePoint
    {
        get { return _runePoint; }
        set { _runePoint = value; }
    }

    [Header("Magnet Setting")] 
    [SerializeField] private float magnetRadius;
    [SerializeField] private float magnetSpeed;
    [SerializeField] private LayerMask itemLayer;
    
    [Header("Assign")] 
    [SerializeField] private TextMeshProUGUI[] runeText;

    private void Start()
    {
        _runePoint = PlayerPrefs.GetInt("RunePoint");
    }

    private void Update()
    {
        for (int i = 0; i < runeText.Length; i++)
        {
            runeText[i].text = $"{_runePoint} :";
        }
        PlayerPrefs.SetInt("RunePoint",_runePoint);
        
        Collider2D[] detectItem = Physics2D.OverlapCircleAll(transform.position, magnetRadius,itemLayer);
        foreach (Collider2D item in detectItem)
        {
            item.transform.position = Vector3.Lerp(item.transform.position,transform.position,magnetSpeed * Time.deltaTime);
        }
    }

    public void TakeRune(int runeTake)
    {
        _runePoint += runeTake;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position,magnetRadius);
    }
}
