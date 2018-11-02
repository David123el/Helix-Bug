using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the behaviour of the Container.
/// </summary>
public class DiceContainerController : MonoBehaviour
{
    #region Fields & Properties
    //list of dice currently in the container.
    public List<GameObject> diceList = new List<GameObject>();
    //original list of dice initialy placed in the container.
    public List<GameObject> originalDiceList = new List<GameObject>();

    //number of cells taken initaly for placing the dice.
    private int _numberOfCells;
    //maximum number of cells in the container.
    public int _maxNumOfCells = 6;

    public new Renderer renderer;

    public GameObject[] dice;

    public Transform diceParent;
    public Transform diceContainerAnchor;

    public bool isFilled = false;
    #endregion

    private void Awake()
    {
        originalDiceList.AddRange(diceList);
    }

    private void Start()
    {
        InitializeContainer();
    }

    public void InitializeContainer()
    {
        diceList.Clear();
        isFilled = false;

        for (int i = 0; i < originalDiceList.Count; i++)
        {
            //each iterate, get a unique position depending on the desired height.
            //getting the division of the container and multiplying by the number of cell.
            Vector3 _posToInitDice = new Vector3(transform.position.x, (renderer.bounds.size.y / 6) * i, transform.position.z);

            GameObject _currentDice = Instantiate(originalDiceList[i], _posToInitDice, Quaternion.identity);
            diceList.Add(_currentDice);
            _currentDice.transform.SetParent(diceParent);
        }
    }

    #region ScriptableObject way
    /*[SerializeField] DiceContainerData _diceContainerData;

    public DiceContainerData DiceContainerData
    {
        get
        {
            return _diceContainerData;
        }

        set
        {
            _diceContainerData = value;
        }
    }

    [SerializeField] private Transform _diceParent;

    public GameObject[] dice;

    private void OnEnable()
    {
        //sets number of cells according to a given number.
        _diceContainerData.CellsPos = new List<Transform>(DiceContainerData.NumOfCells);
        Debug.Log(_diceContainerData.CellsPos.Count);
        //loop through the list of cells positions and sets the position for each one.
        for (int i = 0; i < _diceContainerData.CellsPos.Count; i++)
        {
            _diceContainerData.CellsPos[i].position = new Vector3(transform.position.x, transform.position.y / _diceContainerData.CellsPos.Count, transform.position.z);
        }

        //loop through the list of cells and randomly throw dice in them.
        for (int i = 0; i < _diceContainerData.CellsPos.Count; i++)
        {
            //get one of the dice randomly.
            var _numOfDice = Random.Range(0, dice.Length);

            //instantiate a dice in the given position.
            GameObject _dice = Instantiate(dice[_numOfDice], _diceContainerData.CellsPos[i]);

            //set the parent to be "Dice Parent".
            _dice.transform.SetParent(_diceParent);
        }
    }*/
    #endregion
}
