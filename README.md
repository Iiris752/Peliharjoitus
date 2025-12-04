MysteeriSeikkailu

Unity-harjoituspeli

Peli on vielä kesken. Tämä on Unitylla toteutettu harjoituspeli, jota kehitän osana peliohjelmoinnin kurssia. Projekti on ensimmäisiä omia pelejäni Unityssä, joten sen tarkoituksena on harjoitella perustaitoja peliohjelmoinnista: AI-hahmoista, ohjauksesta ja animaatioista.

Pelin idea

Pelaaja ohjaa hahmoa point-and-click -tyylisesti osoittamalla hiirellä paikkaa, johon hahmo liikkuu. Pelaajalla on käytössään jousiase, jota voi liikuttaa näppäimistöllä. Pelaaja voi ampua nuolia välilyöntiä painamalla. Kentässä liikkuu AI-ohjattu kummitus, joka käyttäytyy pelaajan toiminnan perusteella.

Kummituksen AI:n toiminta

Pelin kummitus hyödyntää Unityn NavMesh-navigointia. AI toimii seuraavasti:
- Kummitus partioi kahden pisteen välillä.
- Jos pelaajahahmo tulee riittävän lähelle, kummitus alkaa seurata pelaajaa.
- Kun kummitus saavuttaa pelaajan, se hyökkää. Hyökkäyksen yhteydessä kuuluu äänimerkki.
- Kummitus liikkuu animoidusti ja siihen on lisätty hyökkäysefekti
- Pelaajalta vähenee health-pisteitä kummituksen hyökätessä

Teknisiä piirteitä:

- C#
- Point-and-click
- Jousen suuntaaminen
- Nuolten ampuminen ja niiden katoaminen kentässä
- NavMesh
- AI: partiointi, pelaajan seuraaminen, hyökkäys
- Etäisyys- ja triggeröintilogiikka
- Efektejä
- Animoitu liikkuminen
