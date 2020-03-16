using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class IngredienteInfo
{
    public Sprite imageIngrediente;
    [Header("Ingredientes Aleatórios")]
    public string[] randomIngre;
}


public class Ingrediente : MonoBehaviour {

    public IngredienteInfo ingredienteInfo;


}
