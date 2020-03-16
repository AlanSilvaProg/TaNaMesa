using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExibicaoSingle : MonoBehaviour
{

    /* [Header("Adicionar todos os text de titulos aqui")]
     public Text[] titulos;

     [Header("Adicionar todos os espaços de receita aqui")]
     public Text[] receitasExtenso;*/

    private GameObject[] bonusCatch;
    private ReceitasBonus[] bonus;

    // private bool atualizou;

    [SerializeField]
    private GameObject botaoPrefab;

    [SerializeField]
    private float yDist;

    private int spawnedRecipes = 0;

    public GameObject exclamacao;

    [SerializeField]
    private GameObject parentObj;

    [SerializeField]
    private GameObject context;

    
    public Image receitaChef;

    public SpriteRenderer chefNoRestaurante;

    public Sprite psita, gato;
    
    public Text chefText;
    // Update is called once per frame
    void Start()
    {
        atualizarRestaurante();
        //PlayerPrefs.DeleteAll();
        /* PlayerPrefs.SetString("bonus1", "Obtido");
         PlayerPrefs.SetString("bonusReceita", "Obtido");
         PlayerPrefs.SetString("bonusReceita2", "Obtido");*/
        bonusCatch = Resources.LoadAll<GameObject>("Bonus");
        bonus = new ReceitasBonus[bonusCatch.Length];
        for (int i = 0; i < bonusCatch.Length; i++)
        {
            bonus[i] = bonusCatch[i].GetComponent<ReceitasBonus>();
        }

        

        /*if(PlayerPrefs.GetString("Atualizar") == "Sim")
        {
            atualizou = false;
            AtualizarBook();
            PlayerPrefs.SetString("Atualizar", " ");
        }*/
       // for (int i = 0; i < bonus.Length; i++)
       // {

            {
                for (int y = 0; y < bonus.Length; y++)
                {
                   
                    if (PlayerPrefs.GetString(bonus[y].info.nomeReceita) == "Obtido")
                    {
                        
                        //  receitasExtenso[i].text = bonus[y].info.receitaEscrita;
                        GameObject botaoTemp =  Instantiate(botaoPrefab,new Vector3(parentObj.transform.position.x, parentObj.transform.position.y + ((Screen.height/8 )* -spawnedRecipes),0),Quaternion.identity,parentObj.transform);

                        ReceitasBonus receitainfo = botaoTemp.GetComponent<ReceitasBonus>();
                        receitainfo.info = bonusCatch[y].GetComponent<ReceitasBonus>().info;
                        botaoTemp.GetComponent<ReceitaSlotButton>().theName.text = bonusCatch[y].GetComponent<ReceitasBonus>().info.nomeReceita;
                         botaoTemp.GetComponent<ReceitaSlotButton>().icone.sprite = bonusCatch[y].GetComponent<ReceitasBonus>().info.icone;
                        spawnedRecipes++;
                        continue;
                    }
                }


            }

        if (PlayerPrefs.GetInt("Notificacao", 0) == 0)
        {
            exclamacao.SetActive(false);
        }
      // }
        /*public void AtualizarBook()
        {
            for(int i = 0; i < titulos.Length; i++)
            {
                if (atualizou == false)
                {
                    for (int y = 0; y < bonus.Length; y++)
                    {
                        if (PlayerPrefs.GetString(bonus[y].info.nomeReceita) == "Obtido")
                        {
                            titulos[i].text = bonus[y].info.nomeReceita;
                          //  receitasExtenso[i].text = bonus[y].info.receitaEscrita;
                            atualizou = true;
                            continue;
                        }
                    }
                }
                if (!atualizou)
                {
                    return;
                }
                else
                {
                    atualizou = false;
                }
            }
        }*/

        
    }

    public void OpenBook()
    {
        PlayerPrefs.SetInt("Notificacao", 0);
        exclamacao.SetActive(false);
    }

    public void SetIcon(Sprite spr)
    {
        receitaChef.sprite = spr;
    }

    public void SetTxt(string str)
    {
        chefText.text = str;
    }

   public void atualizarRestaurante()
    {
        if (PlayerPrefs.GetInt("Personagem") == 2)
        {
            chefNoRestaurante.sprite = psita;
        }
        else
        {
            chefNoRestaurante.sprite = gato;
        }
    }

}
