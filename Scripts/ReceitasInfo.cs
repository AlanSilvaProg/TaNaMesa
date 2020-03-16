using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Receita
{

    public string NomeDaReceita;
    public string[] Ingredientes;
    [Header("Váriavel Valida no modo campanha")]
    public string nomeReceitaBonus;

    [Header("Receitas Aleatórias")]
    public string[] random;


}

