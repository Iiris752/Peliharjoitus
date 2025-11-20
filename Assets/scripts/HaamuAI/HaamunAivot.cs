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
    HaamuliininTilakone m_haamunTilakoneenTila = HaamuliininTilakone.Idle;
    
    //luokan muuttuja merkitään hungarian notaation mukaisesti etuliitteellä m_
    //sen voi sitten erottaa paikallisesta muuttujasta tai metodin parametrista
    //Haamuliinin komponentit ja muuttujat niihin: 
    NavMeshAgent m_haamunNavigaatio;
    AudioSource m_haamunAudio;

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
        m_alkuAika = m_alkuAika+Time.deltaTime;

        if(m_alkuAika > 1f)
        {
            m_haamunNavigaatio.isStopped = false;
            m_haamunNavigaatio.SetDestination(m_pelaaja.transform.position);
            m_alkuAika = 0f;
        }

        float etaisyysPelaajaan = Vector3.Distance(m_pelaaja.transform.position, transform.position);
        if (etaisyysPelaajaan < 2f)
        {
            m_haamunTilakoneenTila = HaamuliininTilakone.Hyokkaa;
        }
    }

    void Hyokkaa()
    {
        //hyökkää tilassa haamu katsoo pelaajaa
        transform.LookAt(m_pelaaja.transform);
        if (!m_haamunAudio.isPlaying) m_haamunAudio.Play();

    }

    void VihollisetVoittaneet()
    {

    }
}
