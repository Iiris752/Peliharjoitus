using UnityEngine;
using UnityEngine.AI;

    //pidetään kirjaa haamun tilasta
    // 0 = Idle, 1 = partiointi, 2 = Seuraa pelaajaa, 3 = hyökkää 
    //4 = viholliset ovat voittaneet
    //enum näkyy hienosti Unityn inspectorissa
enum HaamuliininTilakone
{
    Idle = 0,
    Partiointi = 1,
    SeuraaPelaajaa = 2,
    Hyokkaa = 3,
    VihollisetVoittaneet = 4    
};

public class HaamunAivot : MonoBehaviour
{
    [SerializeField]
    int m_haamunHealth = 5;

    [SerializeField]
    private GameOverManager m_gameOverManager;

    [SerializeField]
    HaamuliininTilakone m_haamunTilakoneenTila = HaamuliininTilakone.Idle;
    
    //luokan muuttuja merkitään hungarian notaation mukaisesti etuliitteellä m_
    //sen voi sitten erottaa paikallisesta muuttujasta tai metodin parametrista
    //Haamuliinin komponentit ja muuttujat niihin: 
    NavMeshAgent m_haamunNavigaatio;
    AudioSource m_haamunAudio;

    Animator m_haamunAnimaatiot;

    ParticleSystem m_haamunHyokkaysPartikkeli;

    [SerializeField]
    GameObject m_alkuPiste;
    [SerializeField]
    GameObject m_loppuPiste;

    //tähän muuttujaan tallennetaan partiotilassa kohteena oleva piste (alku- tai loppupiste)
    GameObject m_kohde;

    //tämän muuttujan avulla lasketaan 3sek aika alussa ennen partiointitilaa
    float m_alkuAika = 0f;

    GameObject m_pelaaja;
    void Start()
    {
        //haamu alkaa idle tilasta
        m_haamunTilakoneenTila = HaamuliininTilakone.Idle;
        // haetaan haamun NavMeshAgent komponentti talteen
        m_haamunNavigaatio = GetComponent<NavMeshAgent>();

        m_pelaaja = GameObject.Find("PelaajaHahmo");
        m_haamunAudio = GetComponent<AudioSource>();
        m_haamunAnimaatiot = GetComponent<Animator>();
        m_haamunHyokkaysPartikkeli = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_haamunTilakoneenTila)
        {
            case HaamuliininTilakone.Idle:
                {
                    Idle();
                    break;
                }
            case HaamuliininTilakone.Partiointi:
                {
                    Partio();
                    break;
                }

            case HaamuliininTilakone.SeuraaPelaajaa:
                {
                    SeuraaPelaajaa();
                    break;
                }

            case HaamuliininTilakone.Hyokkaa:
                {
                    Hyokkaa();
                    break;
                }

            case HaamuliininTilakone.VihollisetVoittaneet:
                //lopetetaan kaikki toiminnot
                m_haamunNavigaatio.isStopped = true;
                break;

            default:
                break;
        }
    }

    void Idle()
    {
        //asetetaan tilan alkuvaatimukset
        m_haamunAnimaatiot.SetBool("HaamuLiikkuu", false);
        m_haamunAnimaatiot.SetBool("HaamuHyokkaa", false);
        m_haamunNavigaatio.speed = 3.5f;
        
        //lasketaan aikaa
        m_alkuAika += Time.deltaTime;
        //jos aikaa on kulunut 3 sekuntia, vaihdetaan tilaa partiointiin
        if (m_alkuAika >= 3f)
        {
            m_haamunTilakoneenTila = HaamuliininTilakone.Partiointi;
        }
    }

    void Partio()
    {
        //tilan alkuvaatimukset:
        m_haamunAnimaatiot.SetBool("HaamuLiikkuu", true);
        m_haamunAnimaatiot.SetBool("HaamuHyokkaa", false);
        m_haamunNavigaatio.speed = 3.5f;
        
        //ensimmäisellä kerralla asetetaan kohteeksi alkupiste
        if (m_kohde == null)
        {
            m_kohde = m_alkuPiste;
        }
        //haamun päästyä johonkin positioon voi olla tilanne, että haamulla ei ole polkua:
        if (m_haamunNavigaatio.hasPath == false)
        {
            m_haamunNavigaatio.SetDestination(m_kohde.transform.position);
        }


        float etaisyysKohteeseen = Vector3.Distance(m_kohde.transform.position, transform.position);
        // Tarkastellaan onko lähellä kohdetta?
        if (etaisyysKohteeseen < 2f)
        {
            //pingpongataan kohdetta
            if (m_kohde == m_alkuPiste) m_kohde = m_loppuPiste;
            else m_kohde = m_alkuPiste;

            m_haamunNavigaatio.SetDestination(m_kohde.transform.position);
        }

        //Tarkastellaan tilakoneen siirtymistä seuraavaan tilaan:

        float etaisyysPelaajaan = Vector3.Distance(m_pelaaja.transform.position, transform.position);
        if (etaisyysPelaajaan < 7f)
        {
            //pysäyttää tilakoneen
            m_haamunNavigaatio.isStopped = true;
            //vaihtaa tilaa seuraamaan pelaajaa
            m_haamunTilakoneenTila = HaamuliininTilakone.SeuraaPelaajaa;
        }
        
    }

    void SeuraaPelaajaa()
    {
        //tilan alkuvaatimukset:
        m_haamunAnimaatiot.SetBool("HaamuLiikkuu", true);
        m_haamunAnimaatiot.SetBool("HaamuHyokkaa", false);
        m_haamunNavigaatio.speed = 1.5f;

        m_alkuAika = m_alkuAika+Time.deltaTime;

        if(m_alkuAika > 1f)
        {
            m_haamunNavigaatio.isStopped = false;
            m_haamunNavigaatio.SetDestination(m_pelaaja.transform.position);
            m_alkuAika = 0f;
        }

        float etaisyysPelaajaan = Vector3.Distance(m_pelaaja.transform.position, transform.position);
        if (etaisyysPelaajaan < 3f)
        {
            m_haamunTilakoneenTila = HaamuliininTilakone.Hyokkaa;
        }
        if(etaisyysPelaajaan > 8f)
        {
            m_haamunTilakoneenTila = HaamuliininTilakone.Partiointi;
        }
    }

    void Hyokkaa()
    {
        //tilan alkuvaatimukset:
        m_haamunAnimaatiot.SetBool("HaamuLiikkuu", true);
        m_haamunAnimaatiot.SetBool("HaamuHyokkaa", true);
        m_haamunNavigaatio.speed = 1.5f;

        //hyökkää tilassa haamu katsoo pelaajaa
        transform.LookAt(m_pelaaja.transform);
    
        //tarkastetaan onko pelaajalla healthia jäljellä?
        //haetaan m_pelaaja Gameobjektista scriptit PointNClick käyttäen GetComponent palvelua
        PointNClick pelaajanScripti = m_pelaaja.GetComponent<PointNClick>();
        int pelaajanHealthTallaHetkella = pelaajanScripti.HaePelaajanHealth();
        if (pelaajanHealthTallaHetkella <= 0)
        {
            Debug.Log("Viholliset voitti");
        }

        //tarkastetaan onko etäisyys pelaajaan pitempi kuin 2 yksikköä
        float etaisyysPelaajaan = Vector3.Distance(m_pelaaja.transform.position, transform.position);
        if (etaisyysPelaajaan > 4f)
        {
            m_haamunTilakoneenTila = HaamuliininTilakone.SeuraaPelaajaa;
            m_haamunHyokkaysPartikkeli.Clear();
            m_haamunHyokkaysPartikkeli.Stop();
        }
    }

    //tätä metodia kutsutaan haamun attack animaation sisältä, framelta 17 käyttäen eventtiä
    public void HaamuAntaaLamaa()
    {
        Debug.Log("Haamu läimii");

        if (!m_haamunAudio.isPlaying) m_haamunAudio.Play();
        m_haamunHyokkaysPartikkeli.Play();

        //tehdään pelaajalle 1 yksikkö vahinkoa:
        PointNClick pelaajanScripti = m_pelaaja.GetComponent<PointNClick>();
        pelaajanScripti.TeePelaajalleVahinkoaYksiYksikko();
    }

    public int HaeHaamunHealth()
    {
        return m_haamunHealth;
    }
    public void OtaVahinkoa()
    {
        m_haamunHealth = m_haamunHealth - 1;
        Debug.Log("haamulle vahinkoa " + m_haamunHealth);

        if (m_haamunHealth <= 0)
        {
            if (m_gameOverManager != null)
                m_gameOverManager.ShowGameOver("Pelaaja voitti!");
            else    
                Debug.Log("Gameover manager puuttuu");
        }
    }
}
