using UnityEngine;

public class LierioLiikutin : MonoBehaviour
{
    public bool liikutinKaynnissa = false;
    public int kokonaisLuku = 50;
    public float desimaaliLuku = 3.14f;
    public string teksti = "Iiriksen lieriö!";

    public Vector3 lierionPaikka = new Vector3(0.0f, 0.0f, 0.0f);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("LierioLiikutin script is active.");
        transform.position = lierionPaikka;
    }

    // Update is called once per frame
    void Update()
    {
        //alustetaan muuttujat x ja y
        float xakseli = 0.0f;
        float zakseli = 0.0f;

        //liikutaan vasemmalle
        if (Input.GetKey("a"))
        {
            Debug.Log("A painettu");
            xakseli = xakseli - 0.01F;
        }

        //liikutaan taaksepäin
        if (Input.GetKey("s"))
        {
            zakseli = zakseli - 0.01F;
        }

        //liikutaan oikealle
        if (Input.GetKey("d"))
        {
            xakseli = xakseli + 0.01F;
        }

        //liikutaan eteenpäin
        if (Input.GetKey("w"))
        {
            zakseli = zakseli + 0.01F;
        }

            Vector3 pluslasku = new Vector3(xakseli, 0.00f, zakseli);
        transform.position = transform.position + pluslasku;
    }
}
