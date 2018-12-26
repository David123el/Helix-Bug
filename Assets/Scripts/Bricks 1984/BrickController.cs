using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrickController : MonoBehaviour
{
    [SerializeField] Text _numberText;
    [SerializeField] int _numberOfHits;
    [SerializeField] ParticleSystem _explosionParticles;
    [SerializeField] GameObject _BrickSpriteRenderer;

    private PolygonCollider2D _BrickCollider;
    private BricksGamePlayManager _gameplayManager;
    private Animation _animation;

    //public AnimationClip animationClip;

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
        _animation = GetComponentInChildren<Animation>();
        _BrickCollider = GetComponent<PolygonCollider2D>();
	}

    void Update ()
    {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BallController>() != null)
        {
            //EventManager.OnBrickGotHit(this);
            _numberOfHits--;
            _numberText.text = _numberOfHits.ToString();
            _animation.Play();

            if (_numberOfHits <= 0)
            {
                StartCoroutine("PlayExplosionEffect");

                //Destroy(gameObject);
                _gameplayManager.bricks.Remove(this);
            }
        }
    }

    private void UpdateBrickState(BrickController brick)
    {
        brick._numberOfHits--;
        brick._numberText.text = _numberOfHits.ToString();
        Debug.Log(brick + brick._numberOfHits.ToString());
    }

    IEnumerator PlayExplosionEffect()
    {
        _BrickSpriteRenderer.SetActive(false);
        _BrickCollider.enabled = false;

        _explosionParticles.Play();
        yield return new WaitForSeconds(_explosionParticles.main.duration);

        Destroy(gameObject);
    }
}
