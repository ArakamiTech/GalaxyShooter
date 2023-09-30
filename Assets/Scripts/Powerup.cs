using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    //0 = Triple Shot
    //1 = Speed Boost
    //2 = shields
    [SerializeField]
    private int powerUpId;
    [SerializeField]
    private AudioClip _clip;

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -6)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                if (powerUpId == 0)
                {
                    player.TripleShotPowerUpOn();
                }
                else
                if (powerUpId == 1)
                {
                    player.SpeedBoostPowerUpOn();
                }
                else
                if (powerUpId == 2)
                {
                    player.ShieldBoostPowerUp();
                }
            }
            Destroy(gameObject);
        }
    }
}
