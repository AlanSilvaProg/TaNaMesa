using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BonusInfo
{
    [Header("Nome da receita")]
    public string nomeReceita;
    [TextArea]
    public string receitaEscrita;
    [Header("Quantidades de estrelas necessárias para liberar a Receita")]
    public int estrelas;

    public Sprite icone;

}

public class ReceitasBonus : MonoBehaviour
{

    public BonusInfo info;

}



