using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class TransformManager : MonoBehaviour
{
    [SerializeField] private GameObject _cheese;
    [SerializeField] private GameObject _cheeseRenderer;
    [SerializeField] private GameObject _pie;
    [SerializeField] private GameObject _pieRenderer;
    [SerializeField] private GameObject _circle;
    [SerializeField] private GameObject _circleRenderer;

    [SerializeField] private List<GameObject> _heart;
    [SerializeField] private List<GameObject> _star;
    [SerializeField] private List<GameObject> _snow;

    [SerializeField] private List<GameObject> _lastParticle;

    [SerializeField] private ParticleSystem _snowParticle;
    [SerializeField] private ParticleSystem _heartParticle;
    [SerializeField] private ParticleSystem _starParticle;

    [SerializeField] private ParticleSystem _heartExplosion;
    [SerializeField] private ParticleSystem _snowExplosion;
    [SerializeField] private ParticleSystem _starExplosion;
    [SerializeField] private ParticleSystem _smokePuff;

    [SerializeField] private GameObject _spawnPos;
    [SerializeField] private GameObject _girlGameObject;


    public ParticleSystem particle;
    [SerializeField] private FinishMovement _finishMovement;
    [SerializeField] private GameObject CombMovement;

    [SerializeField] private Material red;
    [SerializeField] private Material blue;


    [SerializeField] private GameObject _restartButton;
    [SerializeField] private GameObject _continueButton;
    [SerializeField] private GameObject _instaPanel;
    [SerializeField] private GameObject _backGround;


    private bool isWin;

    private void OnTriggerEnter(Collider other)
    {
        #region Kaybetme
        if (other.tag == "Saw" || other.tag == "Spikes")
        {
            if(_lastParticle.Count != 0)
            {
                StartCoroutine(StopParticle(_lastParticle[0].name));
            }
            StartCoroutine(DisableCamera(1f));
            transform.DOJump(new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z - 2),3f,1,1f);
            if(other.tag == "Spikes")
            {
                Destroy(other.gameObject.GetComponent<Collider>());
                other.transform.DOMoveY(-1f, 1.5f);
            }   
            if (_lastParticle.Count != 0)
            {
                for(int i = 0; i < _lastParticle.Count; i++)
                {

                    _lastParticle[i].SetActive(false);
                }
            }
            _lastParticle.Clear();
        }
        if(other.tag == "Space")
        {
            Destroy(Camera.main.gameObject.GetComponent<CameraScript>());
            Destroy(gameObject.GetComponent<PlayerMovement>());
            Destroy(gameObject.GetComponent<Rigidbody>());
            gameObject.GetComponent<Collider>().isTrigger = true;
            Vector3 currentPos = transform.position;
            Vector3 diePos = new Vector3(transform.position.x, -120f, transform.position.z);
            Debug.Log("Oyunu bitir");
            transform.DOMoveY(-6f, 1.5f).SetEase(Ease.InOutElastic);
            StartCoroutine(RestartButton());
        }
        #endregion
        #region Sekil Degistirme
        if (other.gameObject.tag == "Comb")
        {
            Animator anim = other.GetComponent<Animator>();
            anim.SetTrigger("Baskı");
            StartCoroutine(DisableCamera(0.5f));
            PlayerMovement._stopTime = 1.5f;
            StartCoroutine(CreateComb());
            _smokePuff.gameObject.SetActive(true);
            _smokePuff.Play();
            Destroy(other.GetComponent<Collider>());
        }
        if (other.tag == "Cube")
        {
            StartCoroutine(DisableCamera(0.5f));
            PlayerMovement._stopTime = 1.5f;
            if (gameObject.layer == 10)
            {
                _cheese.SetActive(false);
            }
            else if (gameObject.layer == 9)
            {
                _pie.SetActive(false);
            }
            _circle.SetActive(true);
            
            gameObject.layer = 8;
            _smokePuff.gameObject.SetActive(true);
            _smokePuff.Play();
            Destroy(other.GetComponent<Collider>());
        }
        if (other.tag == "Clasp")
        {
            Animator anim = other.GetComponent<Animator>();
            anim.SetTrigger("Baskı");
            StartCoroutine(DisableCamera(0.5f));
            PlayerMovement._stopTime = 1.5f;
            StartCoroutine(CreateClasp());
            _smokePuff.gameObject.SetActive(true);
            _smokePuff.Play();
            Destroy(other.GetComponent<Collider>());
        }
        #endregion
        #region Dokme islemi
        if (other.tag == "Star")
        {
            if (_lastParticle.Count != 0)
            {
                _lastParticle.Clear();
            }
            for (int i = 0; i < _star.Count; i++)
            {
                _star[i].SetActive(true);
                _lastParticle.Add(_star[i]);
            }
            if (!_starExplosion.gameObject.activeInHierarchy)
            {
                _starExplosion.gameObject.SetActive(true);
            }
            else
            {
                _starExplosion.Play();
            }
        }
        if (other.tag == "Heart")
        {
            if (_lastParticle.Count != 0)
            {
                _lastParticle.Clear();
            }
            for (int i = 0; i < _heart.Count; i++)
            {
                _heart[i].SetActive(true);
                _lastParticle.Add(_heart[i]);
            }
            if (!_heartExplosion.gameObject.activeInHierarchy)
            {
                _heartExplosion.gameObject.SetActive(true);
            }
            else
            {
                _heartExplosion.Play();
            }
        }
        if(other.tag == "Snow")
        {
            if (_lastParticle.Count != 0)
            {
                _lastParticle.Clear();
            }
            for (int i = 0; i < _snow.Count; i++)
            {
                _snow[i].SetActive(true);
                _lastParticle.Add(_snow[i]);
            }
            if (!_snowExplosion.gameObject.activeInHierarchy)
            {
                _snowExplosion.gameObject.SetActive(true);
            }
            else
            {
                _snowExplosion.Play();
            }
        }
        #endregion
        #region Renk Degistirme
        if (other.tag == "RedColor")
        {
            _circleRenderer.GetComponent<Renderer>().material = red;
            _cheeseRenderer.GetComponent<Renderer>().material = red;
            _pieRenderer.GetComponent<Renderer>().material = red;
        }
        if (other.tag == "BlueColor")
        {
            _circleRenderer.GetComponent<Renderer>().material = blue;
            _cheeseRenderer.GetComponent<Renderer>().material = blue;
            _pieRenderer.GetComponent<Renderer>().material = blue;
        }
        #endregion
        #region Bitis
        if(other.tag == "Freeze")
        {
            //PlayerMovement._stopTime = 2f;
            //StartCoroutine(DisableCamera(1f));
            Destroy(_cheeseRenderer.GetComponent<JellyMesh>());
            Destroy(_circleRenderer.GetComponent<JellyMesh>());
            Destroy(_pieRenderer.GetComponent<JellyMesh>());
            Color color = _cheeseRenderer.GetComponent<Renderer>().material.color;
            color = new Color(color.r, color.g, color.b, 1f);
            _cheeseRenderer.GetComponent<Renderer>().material.color = color;
            _pieRenderer.GetComponent<Renderer>().material.color = color;
            _circleRenderer.GetComponent<Renderer>().material.color = color;
        }
        if(other.tag == "Finish")
        {
            StartCoroutine(FinishSpawning());
            
        }
        #endregion
    }
    public IEnumerator DisableCamera(float time)
    {
        Camera.main.gameObject.GetComponent<CameraScript>().enabled = false;
        yield return new WaitForSeconds(time);
        Camera.main.gameObject.GetComponent<CameraScript>().enabled = true;
    }
    public IEnumerator StopParticle(string lastparticle)
    {
        if(lastparticle == "Heart")
        {
            _heartParticle.gameObject.SetActive(true);
        }
        if(lastparticle == "Snow")
        {
            _snowParticle.gameObject.SetActive(true);
        }
        if(lastparticle == "Star")
        {
            _starParticle.gameObject.SetActive(true);

        }
        yield return new WaitForSeconds(1f);
        _snowParticle.gameObject.SetActive(false);
        _heartParticle.gameObject.SetActive(false);
        _starParticle.gameObject.SetActive(false);
    }
    public IEnumerator FinishSpawning()
    {
        Animator anim = _girlGameObject.GetComponent<Animator>();
        transform.DOMoveX(0, 1f);
        PlayerMovement.isFinish = true;
        _heartExplosion.gameObject.SetActive(false);
        _snowExplosion.gameObject.SetActive(false);
        _starExplosion.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        transform.DOMove(_spawnPos.transform.position, 1f);
        yield return new WaitForSeconds(1f);
        if(gameObject.layer == 9)
        {
            CombMovement.SetActive(true);
            var currentGameObject1 = Instantiate(gameObject, CombMovement.transform.position, Quaternion.Euler(0, 180, 180));
            currentGameObject1.transform.parent = _spawnPos.transform;
            Destroy(currentGameObject1.gameObject.GetComponent<Rigidbody>());
            Destroy(currentGameObject1.gameObject.GetComponent<PlayerMovement>());
            Destroy(currentGameObject1.gameObject.GetComponent<TransformManager>());
            currentGameObject1.transform.parent = CombMovement.transform;
        }
        if(gameObject.layer == 10)
        {
            var currentGameObject = Instantiate(gameObject, _spawnPos.transform.position, Quaternion.Euler(-90f, 0, 0));
            currentGameObject.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            currentGameObject.transform.parent = _spawnPos.transform;
            Destroy(currentGameObject.gameObject.GetComponent<Rigidbody>());
            Destroy(currentGameObject.gameObject.GetComponent<PlayerMovement>());
            Destroy(currentGameObject.gameObject.GetComponent<TransformManager>());
            currentGameObject.transform.parent = _spawnPos.transform;
        }
        _cheeseRenderer.SetActive(false);
        _circleRenderer.SetActive(false);
        _pieRenderer.SetActive(false);
        if (gameObject.layer == 8)
        {
            anim.SetTrigger("Sad");
            isWin = false;
        }
        else
        {
            anim.SetTrigger("Win");
            isWin = true;
        }
        StartCoroutine(DisableCamera(8f));
        yield return new WaitForSeconds(3f);
        if(gameObject.layer == 8)
        {
            UIManager.isSad = true;
        }
        else
        {
            UIManager.isLike = true;
        }
        _instaPanel.SetActive(true);
        _backGround.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        if (isWin)
        {
            _continueButton.gameObject.SetActive(true);
        }
        else
        {
            _restartButton.gameObject.SetActive(true);
        }
        Destroy(gameObject);
    }
    public IEnumerator RestartButton()
    {
        yield return new WaitForSeconds(1.5f);
        _restartButton.SetActive(true);
    }
    public IEnumerator CreateComb()
    {
        yield return new WaitForSeconds(0.3f);
        if (gameObject.layer == 8)
        {
            _circle.SetActive(false);
        }
        else if (gameObject.layer == 9)
        {
            _pie.SetActive(false);
        }
        _cheese.SetActive(true);
        gameObject.layer = 10;
    }
    public IEnumerator CreateClasp()
    {
        yield return new WaitForSeconds(0.3f);
        if (gameObject.layer == 8)
        {
            _circle.SetActive(false);
        }
        else if (gameObject.layer == 10)
        {
            _cheese.SetActive(false);
        }
        _pie.SetActive(true);
        gameObject.layer = 9;
    }
}
