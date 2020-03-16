using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Resources;

public enum StateGameMulti
{
    INGREDIENTEFASE,
    RECEITAFASE,
    PONTUACAOFASE
}

public class Multiplayer : MonoBehaviour {

    [Header("Habilitar para teste sem erros")]
    public bool testando;

    public StateGameMulti stateGame;

    public Receita actualReceita;

    public Button[] textButtonsGame;
    public Text nomeIngrediente;

    public Image imageIngre;
    public Image imageCircle;
    public Text desafioText;

    public GameObject ingredientesCanvas, receitaCanvas, pontuacaoCanvas;
    public Image[] stars;
    public Text pontuacaoText, fimPontuacao;
    public Button nextLevel;

    public Text textoNextButton;
    
    private GameObject[] bonusCatch;
    private GameObject[] receitaCatch;
    private Receita[] receitaAvaible;
    public  string[] aleatorioReceitas;
    private IngredienteInfo ingredienteCatch;
    private List<string> receitaUsed;
    private int valueRandom;

    private GameObject[] desafioCatch;
    private string desafio;
    private int randomDesafio;
    private bool mudaDesafio = true;
    private int ingredienteCount;
    private List<string> desafiosLevel;

    private bool contandoPonto;
    private float countStars;
    private float pontuacao; // temporario
    private float pontuacaoContador; // temporario
    private int randomCatch;
    private bool changeTexts;
    private int correctButton; // 0 a 3
    private int actualIngrediente;

    public Animator anims;

    [SerializeField]
    private Animator trocaFader, escondaFader;

    [SerializeField]
    private Sprite Calopsita1, Calopsita2, Gato1, Gato2, Vaca1, Vaca2, Mexico1, Mexico2,VacaWrong,MexicoWrong;

    [SerializeField]
    private Image NPCSlot, PlayerSlot, NPCSlotWrong;
    // Use this for initialization

    private int currentRecipe = 0;

    private void Awake()
    {

        if (PlayerPrefs.GetString("Modo") == "Rapido" || testando)
        {
            receitaCatch = Resources.LoadAll<GameObject>("All");
        } else if(PlayerPrefs.GetString("Modo") == "Campanha")
        {
            receitaCatch = Resources.LoadAll<GameObject>("Paises/" + PlayerPrefs.GetString("PaisAtual"));
        }
        receitaAvaible = new Receita[receitaCatch.Length];
        valueRandom = receitaAvaible.Length;
        for (int i = 0; i < receitaCatch.Length; i++)
        {
            receitaAvaible[i] = receitaCatch[i].GetComponent<Receitas>().receita;
        }
        receitaUsed = new List<string>();
        desafiosLevel = new List<string>();

    }


    void Start () {

        AtualizarReceita();
        bonusCatch = Resources.LoadAll<GameObject>("Bonus");
        changeTexts = true;
        correctButton = (int)Random.Range(0, 4);
        pontuacao = 300;
        pontuacaoContador = 0;
        countStars = 0;

        if (PlayerPrefs.GetString("PaisAtual") == "Brasil")
        {
            NPCSlotWrong.sprite = VacaWrong;
        }
        else if (PlayerPrefs.GetString("PaisAtual") == "Mexico")
        {
            NPCSlotWrong.sprite = MexicoWrong;
        }


    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        { 
            PlayerPrefs.Save();
        }
    }

    // Update is called once per frame
    void Update () {
        PlayerPrefs.Save();
        if (PlayerPrefs.GetString("PoP") == "Sim")
        {
            MakePopUp();
        }

        if(stateGame == StateGameMulti.INGREDIENTEFASE)
        {
            if (ingredientesCanvas.activeSelf == false)
            {
                //gameObject.SendMessage("IngreVisit");
                ingredientesCanvas.SetActive(true);
                receitaCanvas.SetActive(false);
                pontuacaoCanvas.SetActive(false);
            }

            for (int i = 0; i < textButtonsGame.Length; i++)
            {
                if (i == 0)
                {
                    textButtonsGame[i].gameObject.SetActive(true);
                    textButtonsGame[i].interactable = false;
                }
                else if (i < 4)
                {
                    textButtonsGame[i].gameObject.SetActive(false);
                }

                if (i == 4)
                {
                    if (actualIngrediente == 0)
                    {
                        textButtonsGame[i].interactable = false;
                    }
                    else
                    {
                        textButtonsGame[i].interactable = true;
                    }
                }
                if (i == 5)
                {
                    textButtonsGame[i].interactable = true;
                }

            }

            ingredienteCatch = (Resources.Load("Ingredientes/" + actualReceita.Ingredientes[actualIngrediente]) as GameObject).GetComponent<Ingrediente>().ingredienteInfo;
            imageIngre.sprite = ingredienteCatch.imageIngrediente;

            imageCircle.enabled = true;
            imageIngre.enabled = true;
            desafioText.enabled = true;

            if (mudaDesafio)
            {

                desafioCatch = Resources.LoadAll<GameObject>("Desafios/" + actualReceita.Ingredientes[actualIngrediente] + "/");
                randomDesafio = (int)Random.Range(0, desafioCatch.Length);
                desafio = desafioCatch[randomDesafio].GetComponent<DesafioInfo>().desafio;
                desafiosLevel.Add(desafio);
                mudaDesafio = false;

            }


            if (changeTexts)
            {
                ChangeTexts();
            }

        }
        else
        {
            imageIngre.enabled = false;
            imageCircle.enabled = false;
            desafioText.enabled = false;
        }

        if (stateGame == StateGameMulti.RECEITAFASE)
        {

            receitaCanvas.SetActive(true);
            ingredientesCanvas.SetActive(false);
            pontuacaoCanvas.SetActive(false);

            for (int i = 0; i < textButtonsGame.Length; i++)
            {
                textButtonsGame[i].gameObject.SetActive(true);
                if(i == 5)
                {
                    textButtonsGame[i].interactable = false;
                }
                else
                {
                    textButtonsGame[i].interactable = true;
                }
            }
            if (changeTexts)
            {
                ChangeTexts();
            }
        }

        if(stateGame == StateGameMulti.PONTUACAOFASE)
        {

            ingredientesCanvas.SetActive(false);
            receitaCanvas.SetActive(false);
            pontuacaoCanvas.SetActive(true);

            if (!contandoPonto)
            {
                ContaPonto();
                fimPontuacao.text = "";
                nextLevel.gameObject.SetActive(false);
            }

           
        }
        else
        {
            pontuacaoCanvas.SetActive(false);
        }

    }

    public void AtualizarReceita()
    {
        if (receitaAvaible.Length != receitaUsed.Count)
        {
            randomCatch = (int)Random.Range(0, valueRandom);
        }
        else
        {
            return;
        }
        
            for (int f = 0; f < receitaUsed.Count; f++)
            {
                for (int i = 0; i < receitaAvaible.Length; i++)
                {
                    if (receitaAvaible[i].NomeDaReceita == receitaUsed[f] && i == randomCatch)
                    {
                        AtualizarReceita();
                        return;
                    }
                }
            }

        Receita receitaCatch;
        receitaCatch = receitaAvaible[randomCatch];
        actualReceita = receitaCatch;
        receitaUsed.Add (actualReceita.NomeDaReceita);
        
        

    }

    public void ChangeTexts()
    {

        if (stateGame == StateGameMulti.INGREDIENTEFASE)
        {
            nomeIngrediente.text = actualReceita.Ingredientes[actualIngrediente];
            desafioText.text = desafiosLevel[actualIngrediente];
        }

        if(stateGame == StateGameMulti.RECEITAFASE)
        {
            for (int i = 0; i < textButtonsGame.Length; i++)
            {
                if(i == correctButton)
                {
                    textButtonsGame[i].GetComponentInChildren<Text>().text = actualReceita.NomeDaReceita;
                }
                else if(i < 4)
                {
                    textButtonsGame[i].GetComponentInChildren<Text>().text = actualReceita.random[i];
                }
            }
        }

        changeTexts = false;

    }

    public void InputButtonGame(int indiceButton)
    {
        if (!TutorialScript.onTuto)
        {
            changeTexts = true;

            if (stateGame == StateGameMulti.INGREDIENTEFASE)
            {
                if (indiceButton == 6)
                {
                    gameObject.SendMessage("AdiviVisit");
                    stateGame = StateGameMulti.RECEITAFASE;
                    actualIngrediente = 0;
                    trocaFader.SetTrigger("PassIN");
                    
                    return;
                }

                if (indiceButton == 4 && actualIngrediente > 0)
                {
                    actualIngrediente--;
                    return;
                }

                if (actualIngrediente >= actualReceita.Ingredientes.Length - 1)
                {
                    stateGame = StateGameMulti.RECEITAFASE;
                    actualIngrediente = 0;
                    trocaFader.SetTrigger("PassIN");

                }
                else
                {
                    actualIngrediente++;
                    if (actualIngrediente > ingredienteCount)
                    {
                        mudaDesafio = true;
                        ingredienteCount++;
                    }
                }

            }
            else
            {
                if (stateGame == StateGameMulti.RECEITAFASE)
                {
                    if (indiceButton == correctButton)
                    {
                        //Debug.Log(PlayerPrefs.GetString("PaisAtual"));
                        if (PlayerPrefs.GetString("PaisAtual") == "Brasil")
                        {
                            if (currentRecipe == 0)
                            {
                                NPCSlot.sprite = Vaca1;
                            }
                            else
                            {
                                NPCSlot.sprite = Vaca2;
                            }
                        }
                        else if (PlayerPrefs.GetString("PaisAtual") == "Mexico") 
                        {
                            if(currentRecipe == 0)
                            {
                                NPCSlot.sprite = Mexico1;
                            }
                            else
                            {
                                NPCSlot.sprite = Mexico2;
                            }
                        }
                        if(PlayerPrefs.GetInt("Personagem") == 2)
                        {
                            if(currentRecipe == 0)
                            {
                                PlayerSlot.sprite = Calopsita1;
                            }
                            else
                            {
                                PlayerSlot.sprite = Calopsita2;
                            }
                        }
                        else
                        {
                            if(currentRecipe == 0)
                            {
                                PlayerSlot.sprite = Gato1;
                            }
                            else
                            {
                                PlayerSlot.sprite = Gato2;
                            }
                        }
                        stateGame = StateGameMulti.PONTUACAOFASE;
                    }
                    else
                    {
                        if (indiceButton < 4)
                        {
                            anims.SetTrigger("WrongFeedback");
                            pontuacao -= 50;
                        }
                    }

                    if (indiceButton == 4)
                    {
                        escondaFader.SetTrigger("EscondeIn");
                        stateGame = StateGameMulti.INGREDIENTEFASE;
                        
                        actualIngrediente = actualReceita.Ingredientes.Length - 1;
                    }
                }
            }
        }
    }

    public void ContaPonto()
    {
        contandoPonto = true;
        float pontuacaoAtual = pontuacao + pontuacaoContador;
        StartCoroutine("Contagem", pontuacaoAtual);
    }

    IEnumerator Contagem(float pointAtual)
    {
        yield return new WaitForEndOfFrame();
        pontuacaoContador ++;
        countStars += 0.01f;
        pontuacaoText.text = "" + (int)pontuacaoContador;

        
        if ( countStars > 0 && countStars < 1.1f)
        {
            stars[0].fillAmount = countStars;
        }
        else if (countStars > 1 && countStars < 2.1f)
        {
            stars[1].fillAmount = (countStars - 1);
        }
        else if (countStars > 2 && countStars < 3.1f)
        {
            stars[2].fillAmount = (countStars - 2);
        }
        

        if (pontuacaoContador < pointAtual)
        {
            StartCoroutine("Contagem", pointAtual);
        }
        else
        {
            FimPonto();
        }
    }

    public void FimPonto()
    {

        //Fanfarra is love
        //Fanfarra is Life
        //Faça Fanfarra aqui

       
        if (PlayerPrefs.GetString("Modo") == "Campanha")
        {
            

            for (int i = 0; i < bonusCatch.Length; i++)
            {
               // Debug.Log(bonusCatch[i].GetComponent<ReceitasBonus>().info.nomeReceita.Equals(actualReceita.nomeReceitaBonus) + ":::" + bonusCatch[i].GetComponent<ReceitasBonus>().info.nomeReceita + ":::" + actualReceita.nomeReceitaBonus);
                if (bonusCatch[i].GetComponent<ReceitasBonus>().info.nomeReceita == actualReceita.nomeReceitaBonus)
                {
                   // Debug.Log("foi");
                    if (countStars >= bonusCatch[i].GetComponent<ReceitasBonus>().info.estrelas)
                    {
                       // Debug.Log("foi2");
                        currentRecipe++;
                        //Debug.Log(currentRecipe);
                        if (PlayerPrefs.GetString(actualReceita.nomeReceitaBonus) != "Obtido")
                        {
                           
                            PlayerPrefs.SetInt("Notificacao", 1);
                        }
                        PlayerPrefs.SetString(actualReceita.nomeReceitaBonus, "Obtido");
                        PlayerPrefs.SetString("Atualizar", "Sim");
                        
                        continue;
                    }
                }
            }
        }

        if (receitaAvaible.Length == receitaUsed.Count)
        {
            textoNextButton.text = "Voltar para o Menu";
        }
        else
        {
            textoNextButton.text = "Próxima Receita";
        }

        fimPontuacao.text = "Sua Pontuação Atual";
        nextLevel.gameObject.SetActive(true);
    }

    public void NextLevel()
    {

        if (receitaAvaible.Length == receitaUsed.Count)
        {
            if(PlayerPrefs.GetString("Atualizar") == "Sim")
            {
                BookCook();
            }else if(PlayerPrefs.GetString("Modo") == "Campanha")
            {
                PlayerPrefs.SetString("Over", "Go");
            }
            SceneManager.LoadScene(0);
            return;
        }

        correctButton = (int)Random.Range(0, 4);
        randomCatch = (int)Random.Range(0, 5);
        AtualizarReceita();
        ResetVariaveis();
        ResetStars();


        //ARRUMAR ISSO DEPOIS, SOLUÇÂO PREGUICOSA QUE SÒ FUNCIONA COM 2 LEVELS. Q FEIO PEDRO Q FEIO
        PlayerPrefs.SetInt("Mexico", 1);

    }

    public void ResetVariaveis()
    {

        contandoPonto = false;
        pontuacaoCanvas.SetActive(false);
        ingredientesCanvas.SetActive(true);
        stateGame = StateGameMulti.INGREDIENTEFASE;
        actualIngrediente = 0;
        ingredienteCount = 0;
        mudaDesafio = true;
        desafiosLevel.Clear();
        pontuacao = 300;
        countStars = 0;

    }

    public void ResetStars()
    {
        for(int i = 0; i < stars.Length; i++)
        {
            stars[i].fillAmount = 0;
        }
    }

    public void MakePopUp()
    {
        //Fazer trigger true do pop up 
        // Pop Up tem que ser um botão, para quando clicar ele executar a Função BookCook
    }

    public void BookCook()
    {
        PlayerPrefs.SetString("Book", "Go");
        SceneManager.LoadScene(0);
    }

   
}
