Unity-harjoituspeli

Tässä on Unitylla toteutettu harjoituspeli, jota kehitän osana peliohjelmoinnin kurssia. Projekti on ensimmäisiä omia pelejäni Unityssä, joten sen tarkoituksena on demonstroida oppimiani perustaitoja peliohjelmoinnista, tekoälystä, ohjauksesta ja animaatioista. Peli on tällä hetkellä keskeneräinen.

Pelin idea

Pelaaja ohjaa hahmoa point-and-click -tyylisesti osoittamalla hiirellä paikkaa, johon hahmo liikkuu. Pelaajalla on käytössään jousiase, jota voi liikuttaa näppäimistöllä. Pelaaja voi ampua nuolia välilyöntiä painamalla. Kentässä liikkuu AI-ohjattu kummitus, joka käyttäytyy dynaamisesti pelaajan toiminnan perusteella.

Tekoälyn toiminta

Pelin kummitus hyödyntää Unityn NavMesh-navigointia. Tekoäly toimii seuraavasti:
- Kummitus partioi kahden pisteen välillä.
- Jos pelaajahahmo tulee riittävän lähelle, kummitus alkaa seurata pelaajaa.
- Kun kummitus saavuttaa pelaajan, se hyökkää. Hyökkäyksen yhteydessä kuuluu äänimerkki.
- Kummitukselle on toteutettu animoitu hahmo AI-generoidulla animaatiolla.

Toteutetut tekniset ominaisuudet

- Unity C# -skriptit
- Point-and-click -ohjaus
- Jousen suuntaaminen näppäimistöllä
- Nuolten ampuminen ja instansointi
- NavMesh-agentin käyttö
- AI-hahmon tilat: partiointi, pelaajan seuraaminen, hyökkäys
- Etäisyysmittaus ja triggerointilogiikka
- Ääniefektien soittaminen hyökkäyksessä
- AI-generoidut animaatiot kummitukselle

Projektin nykytila

Projekti on harjoitus- ja oppimiskäytössä. Sen tavoitteena on opetella:
- Unityn peruskäyttöä
- Pelimekaniikkojen rakentamista
- Tekoälyn toteuttamista NavMeshin avulla
- Pelimaailman interaktioita
- Koodin jäsentelyä ja projektin hallintaa

Peliin tullaan lisäämään myöhemmin esimerkiksi:

- osumadetektiikka
- elämämekaniikka
- lisää vihollislogiikkaa
- visuaalisia parannuksia
