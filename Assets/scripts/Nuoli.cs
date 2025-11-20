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

    //OSUMIEN KÄSITTELY

    void OnCollisionEnter(Collision osumaKohta)
    {
        if (osumaKohta.gameObject.name.Contains("Tree") || osumaKohta.gameObject.name.Contains("Maali"))
        {
            //pysäytetään nuoli
            nuolenFysiikka.isKinematic = true;
            //tuhotaan kohde, jos se on maali. Muita kohteita ei tuhota
            if (osumaKohta.gameObject.name.Contains("Maali"))
            {
                Destroy(osumaKohta.gameObject, 2f);
            }
        }
    }

}
