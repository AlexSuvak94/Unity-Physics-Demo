using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public Transform coinText;
    public GameMasterScript GM;
    private GameObject Car;

    private bool isCollected = false;
    private bool startedFollowingHero = false;

    void Start()
    {
        Car = GameObject.FindGameObjectWithTag("Car");
    }

    void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);

        if (isCollected)
        {
            Vector3 screenPos = coinText.position;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, Camera.main.WorldToScreenPoint(transform.position).z));
            transform.position = Vector3.MoveTowards(transform.position, worldPos, 50f * Time.deltaTime);

            if (Vector3.Distance(transform.position, worldPos) < 0.1f)
            {
                GM.addCoin();
                Destroy(gameObject);
            }
        }
        else
        {
            float distance = Vector3.Distance(transform.position, Car.transform.position);
            if (distance < 5f)
            {
                startedFollowingHero = true;
                Vector3 dir = (Car.transform.position - transform.position).normalized;
                transform.position += dir * 14f * Time.deltaTime;
                transform.Rotate(0f, 250f * Time.deltaTime, 0f);
            }
            else
            {
                if (startedFollowingHero == true)
                {
                    isCollected = true;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            isCollected = true;
        }
    }
}