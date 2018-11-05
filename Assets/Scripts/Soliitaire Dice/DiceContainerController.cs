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
    private List<GameObject> originalDiceList = new List<GameObject>();

    //maximum number of cells in the container.
    public const int _maxNumOfCells = 6;

    public new Renderer renderer;

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
}
