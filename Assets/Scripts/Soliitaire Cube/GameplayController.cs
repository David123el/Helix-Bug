using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls the Game.
/// </summary>
public class GameplayController : MonoBehaviour
{
    #region Fields & Properties
    public static GameplayController instance;

    RaycastHit2D hit;

    GameObject _currentSelectedDice;
    Rigidbody2D _currentSelectedDiceRB2D;
    DiceData _currentSelectedDiceData;

    bool _isDiceSelected = false;

    DiceContainerController _initialDiceContainerController;
    DiceContainerController diceContainerController;
    DiceContainerController[] arrayOfDiceContainerControllers;

    int _currentDiceNumber = 0;
    private int _winConditionCounter;

    [Tooltip("Number of Filled Containers required to win the level.")]
    [SerializeField] int numOfFilledContainersToWin;

    public Image UIPanel;
    public GameObject nexlLevelButton;

    //public ParticleSystem touchParticle;
    #endregion

    private void Awake()
    {
        //if (instance == null)
        //    instance = this;

        //else if (instance != this)
        //    Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        arrayOfDiceContainerControllers = FindObjectsOfType<DiceContainerController>();
    }

    void Update()
    {
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector3 _touch = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            hit = Physics2D.Raycast(_touch, Vector2.zero);

            //touchParticle.transform.position = _touch;
            //touchParticle.Play();

            StartCoroutine("MoveDice", hit);
        }
    }

    private IEnumerator MoveDice(RaycastHit2D hit)
    {
        yield return new WaitForSeconds(0.1f);
        if (hit.collider != null)
        {
            if (hit.collider.GetComponentInParent<DiceContainerController>() != null)
            {
                diceContainerController = hit.collider.GetComponentInParent<DiceContainerController>();

                if (_isDiceSelected == false)
                {
                    if (diceContainerController.diceList.Count > 0)
                    {
                        LiftDice();

                        //if the dice number is 6 and the container has already been flaged as "filled",
                        //flag it as unfilled and down count the win condition counter.
                        if (_currentSelectedDiceData._diceNumber == 6)
                        {
                            if (diceContainerController.isFilled == true)
                            {
                                diceContainerController.isFilled = false;
                                _winConditionCounter--;
                            }
                        }
                    }
                }
                else if (_isDiceSelected == true)
                {
                    //if there is enough space in the container.
                    if (diceContainerController.diceList.Count < diceContainerController._maxNumOfCells)
                    {
                        _currentDiceNumber = _currentSelectedDiceData._diceNumber;
                        //if the container is not empty.
                        if (diceContainerController.diceList.Count > 0)
                        {
                            GameObject _currentTopDice =
                                diceContainerController.diceList[diceContainerController.diceList.Count - 1];
                            DiceData _currentTopDiceData = _currentTopDice.GetComponent<DiceData>();

                            //if the current dice number is consecutive to the dice below it,
                            //or if it's the same container and the player wants to re-place it.
                            if ((_currentDiceNumber - 1) == _currentTopDiceData._diceNumber)
                            {
                                PlaceDice(diceContainerController);

                                //check for win-condition.
                                if (_currentDiceNumber == 6)
                                {
                                    //if there are 6 dice in the container.
                                    if (diceContainerController.diceList.Count == 6)
                                    {
                                        for (int i = 0; i < diceContainerController.diceList.Count; i++)
                                        {
                                            if (diceContainerController.diceList[i].GetComponent<DiceData>()._diceNumber == i + 1)
                                                i++;

                                            if ((i == 5) && (diceContainerController.isFilled == false))
                                            {
                                                diceContainerController.isFilled = true;
                                                _winConditionCounter++;
                                            }

                                            if (_winConditionCounter == numOfFilledContainersToWin)
                                            {
                                                _winConditionCounter = 0;

                                                //enable "Next Level button.
                                                nexlLevelButton.SetActive(true);
                                            }
                                        }
                                    }
                                }
                            }
                            else PlaceDice(_initialDiceContainerController);
                        }
                        //if the container is empty.
                        else
                        {
                            //if it's dice 1 the player wants to move.
                            //if (_currentDiceNumber == 1)
                            //{
                            PlaceDice(diceContainerController);
                            //}
                        }
                    }
                    //if the container is full.
                    else PlaceDice(_initialDiceContainerController);

                    ResetVariables();
                }
            }
        }
        yield return new WaitForSeconds(0.1f);
    }

    private void PlaceDice(DiceContainerController diceContainerController)
    {
        //add the dice to the list of the touched container.
        diceContainerController.diceList.Add(_currentSelectedDice);

        //change the dice's position and renable physics simulation.
        _currentSelectedDice.transform.position =
            diceContainerController.diceContainerAnchor.position;
        _currentSelectedDice.transform.SetParent(diceContainerController.transform.Find("Dice Parent"));
        _currentSelectedDiceRB2D.simulated = true;
    }

    private void LiftDice()
    {
        //keep a reference to the original container.
        //may be used for replacing a dice in the same container.
        _initialDiceContainerController = diceContainerController;

        //assign the correct dice.
        _currentSelectedDice = diceContainerController.transform.Find("Dice Parent")
            .GetChild(diceContainerController.diceList.Count - 1).gameObject;

        _isDiceSelected = true;

        //cache the components of the currently selected dice.
        _currentSelectedDiceRB2D = _currentSelectedDice.GetComponent<Rigidbody2D>();
        _currentSelectedDiceData = _currentSelectedDice.GetComponent<DiceData>();

        //stop simulating physics for the dice and change it's position.
        _currentSelectedDiceRB2D.simulated = false;
        _currentSelectedDice.transform.position = diceContainerController.diceContainerAnchor.position;

        //remove the dice from that list, prepare to add it to a new container.
        diceContainerController.diceList.Remove
            (diceContainerController.diceList[diceContainerController.diceList.Count - 1]);
    }

    private void ResetVariables()
    {
        _currentSelectedDice = null;
        _initialDiceContainerController = null;
        _currentDiceNumber = 0;
        _isDiceSelected = false;
    }

    public void PlayNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings)
        {
            for (int i = 0; i < arrayOfDiceContainerControllers.Length; i++)
            {
                arrayOfDiceContainerControllers[i].isFilled = false;
            }

            SceneManager.LoadScene(SceneManager.GetActiveScene()
                .buildIndex + 1, LoadSceneMode.Single);
        }
        else Debug.Log("Last Scene");
    }

    public void ResetGame()
    {
        nexlLevelButton.SetActive(false);

        Destroy(_currentSelectedDice);

        //empty the variables and get them ready for the next dice.
        _currentSelectedDice = null;
        _initialDiceContainerController = null;
        _currentDiceNumber = 0;
        _isDiceSelected = false;

        //look through the dice lists and empty them.
        foreach (var item in arrayOfDiceContainerControllers)
        {
            var diceParent = item.transform.Find("Dice Parent");
            var childCount = diceParent.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Destroy(diceParent.GetChild(i).gameObject);
            }
            item.diceList.Clear();

            //create and generate a new list.
            item.InitializeContainer();
        }

        _winConditionCounter = 0;
    }
}
