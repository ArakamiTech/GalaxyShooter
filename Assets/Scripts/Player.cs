using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _laserLeft;
    [SerializeField]
    private GameObject _laserRight;
    [SerializeField]
    private GameObject _laserCenter;
    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private GameObject _shieldGameObject;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private float _speed = 5.0f;
    private UIManager _uiManager;
    private GameManager _gameManager;
    private float _fireRate = 0.25f;
    private float _canFire = 0.0f;
    public bool canTripleShoot = false;
    public bool isSpeedBoostActive = false;
    public bool isShieldtActive = false;
    private SpanwManager _spawnManager;
    private AudioSource _audioSource;
    [SerializeField]
    private GameObject[] _engines;
    private int _hitCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        _hitCount = 0;
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager != null)
        {
            _uiManager.UpdateLives(_lives);
        }
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        _spawnManager = GameObject.Find("SpanwManager").GetComponent<SpanwManager>();
        if (_spawnManager != null)
        {
            _spawnManager.StartSpawnRoutines();
        }
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    public void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (isSpeedBoostActive == true)
        {
            transform.Translate(Vector3.right * _speed * 1.5f * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * 1.5f * verticalInput * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);
        }

        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }

        if (transform.position.x > 9.5f)
        {
            transform.position = new Vector3(-9.5f, transform.position.y, 0);
        }
        else if (transform.position.x < -9.5f)
        {
            transform.position = new Vector3(9.5f, transform.position.y, 0);
        }
    }

    private void Shoot()
    {
        if (Time.time > _canFire)
        {
            _audioSource.Play();
            if (canTripleShoot == true)
            {
                Instantiate(_laserLeft, transform.position + new Vector3(-0.5f, -0.5f, 0), Quaternion.identity);
                Instantiate(_laserRight, transform.position + new Vector3(0.5f, -0.5f, 0), Quaternion.identity);
                Instantiate(_laserCenter, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.75f, 0), Quaternion.identity);
            }
            _canFire = Time.time + _fireRate;
        }
    }

    public void Damage()
    {
        if (isShieldtActive == true)
        {
            isShieldtActive = false;
            _shieldGameObject.SetActive(false);
            return;
        }
        _hitCount++;
        if (_hitCount == 1)
        {
            _engines[0].SetActive(true);
        }
        else
        if (_hitCount == 2)
        {
            _engines[1].SetActive(true);
        }
        _lives--;
        _uiManager.UpdateLives(_lives);
        if (_lives < 1)
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _gameManager._gameOver = true;
            _uiManager.ShowTitleScreen();
            Destroy(this.gameObject);
        }
    }

    public void SpeedBoostPowerUpOn()
    {
        isSpeedBoostActive = true;
        StartCoroutine(SpeedBoostDownRoutine());

    }

    public IEnumerator SpeedBoostDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isSpeedBoostActive = false;
    }

    public void TripleShotPowerUpOn()
    {
        canTripleShoot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShoot = false;
    }

    public void ShieldBoostPowerUp()
    {
        isShieldtActive = true;
        _shieldGameObject.SetActive(true);
    }

}
