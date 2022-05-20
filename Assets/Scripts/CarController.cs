using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarController : MonoBehaviour
{
    [SerializeField] float carSpeed;
    [SerializeField] float steerSpeed;
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject winMessage;

    public int numOfGroups;
    public TextMeshProUGUI numOfPeople;
    public TextMeshProUGUI carStatus;

    bool carFull = false;
    string isCarFull = "No";
    People destroyGameobject;
    void Start()
    {
        carStatus.text = "Car full:" + isCarFull;
        numOfGroups = FindObjectsOfType<People>().Length;
        SoundManager.Instance().PlayBGM(Sound.BGMSOund);
        numOfPeople.text = "Remaining rescues:" + numOfGroups;
    }

    void Update()
    {
        CarMovement();
    }

    private void CarMovement()
    {
        float speed = Input.GetAxis("Vertical") * carSpeed * Time.deltaTime;
        float steer = Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime;
        transform.Translate(0, speed, 0);
        transform.Rotate(0, 0, -steer);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            Debug.Log("U r dead");
            SoundManager.Instance().PlayBGM(Sound.Gameover);
            StartCoroutine( CarBlast());

        }

        if (collision.gameObject.CompareTag("People") &&carFull==false)
        {
            PeopleInCar(collision);
        }
        if (collision.gameObject.CompareTag("Hospital") && carFull == true)
        {

            PeopleAtHospital();
        }

        if (collision.gameObject.CompareTag("Explosion"))
        {
            SoundManager.Instance().PlayBGM(Sound.Explosion);
           StartCoroutine( CarBlast());
        }
    }

    private void PeopleAtHospital()
    {
        Destroy(destroyGameobject);
        numOfGroups--;
        numOfPeople.text = "Remaining rescues:" + numOfGroups;
        isCarFull = "No";
        carStatus.text = "Car full:" + isCarFull;
        SoundManager.Instance().PlaySFX(Sound.save);
        Debug.Log("saved");
        carFull = false;
        if (numOfGroups <= 0)
        {
            StartCoroutine( Win());
        }
    }

    private void PeopleInCar(Collision2D collision)
    {
        collision.gameObject.GetComponentInParent<People>().SetTimer();
        destroyGameobject = collision.gameObject.GetComponentInParent<People>();
        isCarFull = "Yes";
        carStatus.text = "Car full:" + isCarFull;
        Debug.Log("enter");
        SoundManager.Instance().PlaySFX(Sound.enter);
        carFull = true;
        Destroy(collision.gameObject);
    }

    IEnumerator Win()
    {
        SoundManager.Instance().PlayBGM(Sound.win);
        winMessage.SetActive(true);
        this.enabled = false;
        yield return new WaitForSeconds(3f);
        winMessage.SetActive(false);
        winScreen.SetActive(true);

    }

    public IEnumerator CarBlast()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        this.enabled = false;
        yield return new WaitForSeconds(2f);
        gameOver.SetActive(true);
          
    }
} 

