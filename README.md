# VacParticules

Simulateur visuel en VB.NET (WinForms) du mouvement de particules dans une chambre a vide,
de la pression atmospherique jusqu'a l'ultra haut vide (UHV, 1E-9 ... 1E-12 mbar).

Ouvrir `VacParticules.slnx` avec Visual Studio 2026, puis F5.
(Projet cible `net8.0-windows` : pour viser .NET 10, changer le `TargetFramework` du `.vbproj` en `net10.0-windows`.)

## Utilisation

- **Pression** : slider logarithmique de 1E+3 a 1E-12 mbar. Le regime (atmospherique, vide primaire,
  HV, UHV, XHV) et le libre parcours moyen sont affiches.
- **Temperature** : 100 a 1000 K, repartit les vitesses selon Maxwell-Boltzmann (N2).
- **Nombre de particules** puis **Reinitialiser** pour repeupler la chambre.
- **Clic dans la chambre** : injecte une bouffee de 25 particules a l'endroit du clic (simule une fuite).
- **Vitesse de simulation** : echelle de temps x0,01 a x10.
- Options : collisions entre particules, reflexion diffuse (loi du cosinus) sur les parois,
  trainees, couleur selon la vitesse (bleu = lent, rouge = rapide).

## Physique

- Libre parcours moyen : `lambda = kB*T / (sqrt(2)*pi*d^2*p)` (d = 3,7E-10 m pour N2).
  A 1E-9 mbar et 293 K, lambda ~ 68 km : les particules ont des trajectoires balistiques
  (lignes droites) et ne rebondissent que sur les parois.
- A pression elevee, lambda devient nanometrique : chaque particule subit des collisions
  aleatoires avec le gaz de fond (probabilite `1 - exp(-v*dt/lambda)` par pas de temps),
  ce qui produit un mouvement brownien visible.
- Parois : reflexion speculaire par defaut, ou diffuse (loi du cosinus de Maxwell).
- Collisions particule/particule : elastiques, masses egales (option).

## Structure

- `Simulation/ConstantesPhysique.vb` : constantes SI, lambda, regimes de vide.
- `Simulation/Chambre.vb` : dimensions, pression, temperature.
- `Simulation/Particule.vb` : position, vitesse, rayon.
- `Simulation/MoteurPhysique.vb` : integration temporelle (sous-pas), collisions.
- `Rendu/ChambreView.vb` : UserControl double buffer, trainees, rendu des particules.
- `Form1.vb` / `Form1.Designer.vb` : interface et boucle de simulation (Timer 15 ms).
