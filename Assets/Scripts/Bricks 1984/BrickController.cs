using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrickController : MonoBehaviour
{
    [SerializeField] Text _numberText;
    [SerializeField] int _numberOfHits;

    private BricksGamePlayManager _gameplayManager;

    private void OnEnable()
    {
        //EventManager.onBrickGotHit += UpdateBrickState;
    }

    private void OnDisable()
    {
        //EventManager.onBrickGotHit -= UpdateBrickState;
    }

    private void Awake()
    {
        _numberText.text = _numberOfHits.ToString();  
    }

    void Start ()
    {
        _gameplayManager = FindObjectOfType<BricksGamePlayManager>();
	}

    void Update ()
    {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BallController>() != null)
        {
            if (_numberOfHits <= 0)
                Destroy(gameObject);
            else
            {
                //EventManager.OnBrickGotHit(this);
                _numberOfHits--;
                _numberText.text = _numberOfHits.ToString();

                if (_numberOfHits == 0)
                {
                    Destroy(gameObject);
                    _gameplayManager.bricks.Remove(this);
                }
            }
        }
    }

    private void UpdateBrickState(BrickController brick)
    {
        brick._numberOfHits--;
        brick._numberText.text = _numberOfHits.ToString();
        Debug.Log(brick + brick._numberOfHits.ToString());
    }
}
