using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CubeContainerData", menuName = "ScriptableObjects/Solitaire Cube")]
public class DiceContainerData : ScriptableObject
{
    [SerializeField] int _numOfCells;
    [SerializeField] int _numOfDice;

    private List<Transform> _cellsPos;
    private List<GameObject> _diceInCells;

    public int NumOfCells
    {
        get
        {
            return _numOfCells;
        }

        set
        {
            _numOfCells = value;
        }
    }

    public int NumOfDice
    {
        get
        {
            return _numOfDice;
        }

        set
        {
            _numOfDice = value;
        }
    }

    public List<Transform> CellsPos
    {
        get
        {
            return _cellsPos;
        }

        set
        {
            _cellsPos = value;
        }
    }

    public List<GameObject> DiceInCells
    {
        get
        {
            return _diceInCells;
        }

        set
        {
            _diceInCells = value;
        }
    }
}
