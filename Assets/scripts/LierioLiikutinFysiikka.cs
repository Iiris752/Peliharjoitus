using System.Numerics;
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
            fysiikkaVartalo.AddRelativeForce(Vector3.forward * nopeus);
        }
        if (sNappiPainettu == true)
        {
            fysiikkaVartalo.AddRelativeForce(Vector3.back * nopeus);
        }
        if (aNappiPainettu == true)
        {
            fysiikkaVartalo.AddRelativeForce(Vector3.left * nopeus);
        }
        if (dNappiPainettu == true)
        {
            fysiikkaVartalo.AddRelativeForce(Vector3.right * nopeus);

        }
    }
}
