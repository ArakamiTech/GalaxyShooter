using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private float _speed = 5.0f;
    [SerializeField]
    private GameObject _enemyExplosionPrefab;
    private UIManager _uIManager;
    [SerializeField]
    private AudioClip _clip;
    void Start()
    {
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -6f)
        {
            float _random = Random.Range(-6f, 6f);
            transform.position = new Vector3(_random, 6, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (other.tag == "Laser")
        {
            if (other.transform.parent != null)
            {
                Destroy(other.transform.parent.gameObject);
            }
            Destroy(other.gameObject);
            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
            _uIManager.UpdateScore();
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
            Destroy(this.gameObject);
        }
        else if (other.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
            if (player != null)
            {
                player.Damage();
            }
        }
        Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
