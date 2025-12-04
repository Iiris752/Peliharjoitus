using UnityEngine;

public class PickElement : MonoBehaviour
{
    Rigidbody poimittavanElementinFysiikka;

    [SerializeField] float ylosNousuAikaMuuttuja = 1.0f;
    [SerializeField] float alasMenoAikaMuuttuja = 1.0f;
    [SerializeField] float ylosMenoVoima = 20f;
    
    float ylosNousuAika = 0f;
    bool laskeuduAlas = false;
    float alhaallaOloaika = 0f;

    void Start()
    {
        poimittavanElementinFysiikka = GetComponent<Rigidbody>();
    }

    void Update()
    {
        ylosNousuAika = ylosNousuAika + Time.deltaTime;
        if (ylosNousuAika > ylosNousuAikaMuuttuja)
        {
            laskeuduAlas = true;
        }
        if (laskeuduAlas == true)
        {
            alhaallaOloaika = alhaallaOloaika + Time.deltaTime;
            if (alhaallaOloaika > alasMenoAikaMuuttuja)
            {
                laskeuduAlas = false;
                alhaallaOloaika = 0f;
                ylosNousuAika = 0f;
            }
        }
    }

    void FixedUpdate()
    {
        if (laskeuduAlas == false)
        {
            poimittavanElementinFysiikka.AddForce(Vector3.up * ylosMenoVoima);
        }
    }

}