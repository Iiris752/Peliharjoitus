using UnityEngine;
using UnityEngine.AI;

public class PointNClick : MonoBehaviour
{
    [SerializeField]
    int m_pelaajanHealth = 25;
    NavMeshAgent polunEtsija;

    //ammuntamekaniikka
    GameObject nuolenLahtoPaikka;

    //varsijousi
    GameObject varsijousi;

    [SerializeField]
    GameObject nuolenPrototyyppi;

    [SerializeField]
    float varsijousenKaantoNopeus = 0.5f;

    void Start()
    {
        polunEtsija = GetComponent<NavMeshAgent>();
        //"Nuoli" pitää olla täsmälleen samanlailla nimetty kuin GameObject kentässä
        nuolenLahtoPaikka = GameObject.Find("Nuoli");

        varsijousi = GameObject.Find("Varsijousi");
    }

    // Update is called once per frame
    void Update()
    {
        //luetaan hiiren vasemman napin painallus
        bool hiirenNappiPainettu = Input.GetMouseButtonDown(0);

        //jos hiiren vasen nappi on painettu, tutkitaan mitä hiiren osoitin osoittaa
        if (hiirenNappiPainettu == true)
        {
            //haetaan kentästä pääkamera ja sen Kamera-komponentti
            Camera kenttaKamera = Camera.main;

            //muodostetaan kenttäkameran kuvasta (siitä kohtaa, missä hiiren kursori on)
            //säde, joka menee suoraan eteenpäin kentässä.
            Ray sade = kenttaKamera.ScreenPointToRay(Input.mousePosition);

            //tehdään säteelle osumatesti, eli tutkitaan osuuko säde johonkin pelin objektiin
            bool sadeosui = Physics.Raycast(sade, out RaycastHit osuma);

            //piirretään debug ray kenttään, jotta tiedetään
            //mihin suuntaan säde menee
            Debug.DrawRay(sade.origin, sade.direction * 100f, Color.red, 10f);

            //jos säde osui johonkin, tulostetaan osuman tiedot konsoliin
            if (sadeosui == true)
            {
                Debug.Log("Säde osui: " + osuma.transform.name);
                polunEtsija.SetDestination(osuma.point);
            }
        }

        //ammuntamekaniikka
        if (Input.GetKeyDown("space") == true)
        {
            //instantiate kloonaa peliobjektin kenttään (unity documentation mukaan)
            GameObject luotuNuoli = Instantiate(nuolenPrototyyppi,
                nuolenLahtoPaikka.transform.position, nuolenLahtoPaikka.transform.rotation);

        }

        //varsijousen kääntäminen
        bool vasemmalle = Input.GetKey("a");
        bool oikealle = Input.GetKey("d");

        if (vasemmalle == true)
        {
            //vasemmalle saa liikkua jos y > 300 tai y < 70
            if (varsijousi.transform.localEulerAngles.y > 300 ||
                varsijousi.transform.localEulerAngles.y < 80 )
            {
                //käännetään varsijousta vasemmalle:
                varsijousi.transform.eulerAngles = varsijousi.transform.eulerAngles - Vector3.up * varsijousenKaantoNopeus;
            }
        }

        if (oikealle == true)
        {
            //oikealle saa liikkua jos y on suurempi kuin 300 TAI y on pienempi kuin 70
            if (varsijousi.transform.localEulerAngles.y < 290f ||
                    varsijousi.transform.localEulerAngles.y < 70f)
            //käännetään varsijousta oikealle:
            {
                varsijousi.transform.eulerAngles = varsijousi.transform.eulerAngles
                + Vector3.up * varsijousenKaantoNopeus;
            }
        }
    }    
    
    //Getteri muuttujan m_pelaajanHealth saamiseksi HaamunAivot scriptin käyttöön:
    public int HaePelaajanHealth()
    {
        return m_pelaajanHealth;
    }
}