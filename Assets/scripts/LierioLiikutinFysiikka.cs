using NUnit.Framework;
using UnityEngine;

public class LierioLiikutinFysiikka : MonoBehaviour
{
    [SerializeField]
    bool aNappiPainettu = false;

    [SerializeField]
    bool sNappiPainettu = false;

    [SerializeField]
    bool wNappiPainettu = false;

    [SerializeField]
    bool dNappiPainettu = false;

    [SerializeField]
    Rigidbody fysiikkaVartalo;

    [SerializeField]
    float nopeus = 10f;

    [SerializeField]
    float kiertonopeus = 10f;


    void Start()
    {
        //haetaan gameobjektista, jossa LierioLiikutinFysiikka skripti on kiinni
        // .. Rigidbody-komponentti
        fysiikkaVartalo = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        aNappiPainettu = Input.GetKey("a");
        sNappiPainettu = Input.GetKey("s");
        wNappiPainettu = Input.GetKey("w");
        dNappiPainettu = Input.GetKey("d");
    }

    void FixedUpdate()
    {
        if (wNappiPainettu == true)
        {
            //w-napilla liikkuu eteenpäin
            fysiikkaVartalo.AddRelativeForce(Vector3.forward * nopeus);
        }
            //pyörähtää vastapäivään y-akselin ympäri, kun käyttäjä painaa a
        if (aNappiPainettu == true)
        {
            fysiikkaVartalo.AddRelativeTorque(Vector3.down * kiertonopeus);
        }
            //liikkuu taaksepäin
        if (sNappiPainettu == true)
        {
            fysiikkaVartalo.AddRelativeForce(Vector3.back * nopeus);
        }
            //pyörähtää myötäpäivään y-akselin ympäri, kun käyttäjä painaa d
        if (dNappiPainettu == true)
        {
            fysiikkaVartalo.AddRelativeTorque(Vector3.up * kiertonopeus);
        }   
    }

}
