Unity-harjoituspeli

Peli on vielä kesken :)! Tämä on Unitylla toteutettu harjoituspeli, jota kehitän osana peliohjelmoinnin kurssia. Projekti on ensimmäisiä omia pelejäni Unityssä, joten sen tarkoituksena on demonstroida oppimiani perustaitoja peliohjelmoinnista, AI-hahmoista, ohjauksesta ja animaatioista. Peli on tosiaan tällä hetkellä keskeneräinen.

Pelin idea

Pelaaja ohjaa hahmoa point-and-click -tyylisesti osoittamalla hiirellä paikkaa, johon hahmo liikkuu. Pelaajalla on käytössään jousiase, jota voi liikuttaa näppäimistöllä. Pelaaja voi ampua nuolia välilyöntiä painamalla. Kentässä liikkuu AI-ohjattu kummitus, joka käyttäytyy pelaajan toiminnan perusteella.

Kummituksen AI:n toiminta

Pelin kummitus hyödyntää Unityn NavMesh-navigointia. AI toimii seuraavasti:
- Kummitus partioi kahden pisteen välillä.
- Jos pelaajahahmo tulee riittävän lähelle, kummitus alkaa seurata pelaajaa.
- Kun kummitus saavuttaa pelaajan, se hyökkää. Hyökkäyksen yhteydessä kuuluu äänimerkki.
- Kummitus liikkuu animoidusti

Teknisiä piirteitä:

- C#
- Point-and-click
- Jousen suuntaaminen
- Nuolten ampuminen ja niiden katoaminen kentässä
- NavMesh
- AI: partiointi, pelaajan seuraaminen, hyökkäys
- Etäisyys- ja triggeröintilogiikka
- Ääniefekti
- Animoitu liikkuminen

Peliin tullaan lisäämään myöhemmin esim
- osumadetektiikka
- elämämekaniikka
- lisää vihollislogiikkaa
- visuaalisia parannuksia
