using UnityEngine;

public class Nuoli : MonoBehaviour
{
    Rigidbody nuolenFysiikka;

    bool annaImpulssi = true;

    [SerializeField]
    float nuolenLahtoNopeus = 35f;

    //nuoli pysyy olemassa tietyn ajan
    [SerializeField]
    float nuolenElossaoloaika = 3f;



    void Start()
    {
        nuolenFysiikka = GetComponent<Rigidbody>();

    }
    void Update()
    {
        //laitettu back koska sininen z-akseli oli väärinpäin nuolessa
        //pikselipohjainen liikkuminen:
        //transform.position = transform.TransformPoint(Vector3.back * 0.1f);
        //ei toimi fysiikalla, joten laitettiin kommentteihin

        //fysiikkapohjainen liikkuminen:
        //IMPULSSI  pitää antaa vain kerran, joten siksi käytetään booleania ja if-lausetta
        //annetaan myös impulssille tarpeeksi voimaa *50f
        if (annaImpulssi == true)
        {
            nuolenFysiikka.AddRelativeForce(Vector3.back * 50f, ForceMode.Impulse);
            annaImpulssi = false;

        }

        // nuoli tuhoutuu 3 sekunnin kuluttua
        nuolenElossaoloaika = nuolenElossaoloaika - Time.deltaTime;
        if (nuolenElossaoloaika < 0f)
        {
            Destroy(gameObject);
        }

    }

    //Osumat:
    void OnCollisionEnter(Collision osumaKohta)
    {
        Debug.Log($"Nuoli osui: {osumaKohta.collider.name}");

        // haamuosuma
        if (osumaKohta.gameObject.name.Contains("Haamu"))
        {
            Debug.Log("Haamuun osui!");
            var haamuliini = osumaKohta.collider.GetComponentInParent<HaamunAivot>();
            if (haamuliini != null)
            {
                haamuliini.OtaVahinkoa();
            }
            // pysäytetään ja poistetaan nuoli, ettei tule tuplahittejä:
            nuolenFysiikka.isKinematic = true;
            Destroy(gameObject, 0.05f);
            return;
        }

        //puuhun, maaliin osuminen:
        if (osumaKohta.gameObject.name.Contains("Tree") || osumaKohta.gameObject.name.Contains("Maali"))
        {
            // pysäytetään nuoli
            nuolenFysiikka.isKinematic = true;

            // tuhota kohde, jos se on maali
            if (osumaKohta.gameObject.name.Contains("Maali"))
            {
                Destroy(osumaKohta.gameObject, 2f);
            }

            //poistetaan nuoli
            Destroy(gameObject, 0.05f);
        }
    }



}
