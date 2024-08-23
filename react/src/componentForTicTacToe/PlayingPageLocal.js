import React from "react";
import "../styles/style.css";
import Grid from "./Grid";

function PlayingPageLocal() {
  // faire apparaitre la liste des piece restantes du joueur sur le coté !!si on clique sur n'importe quel bouton de la grille!!
  // la liste disparait !!si on clique une deuxieme!! fois
  // |!si un bouton de la grille est cliqué!|, on regarde si une piece de la liste est cliqué
  // si une piece de la liste est clique on l'ajoute temporairement à la case , en dimuniant la quantité de pieces restantes du joueur
  // si la quantité de pieces est null, on ne fait rien
  // si le joueur clique sur le bouton ok le choix temporaire est maintenue, et le boutton ok se fige eet le boutton annuler ce defige
  // le seul coup possible est annuler ou finir le tour, annuler retablie l'état d'avant le coups temporaire
  // il faudrait un boutton tour valider pour que l'ajout soit definitif

  // quand on clique sur un bouton on doit avoir le choix entre ajouter et déplacer

  return (
    <div>
      <h1> 3D Tic-Tac-Toe Game</h1>
      <div></div>
      <div>
        <div>
          <Grid />
        </div>
      </div>
      <div>
        ATTENTION : le tour se termine automatiquement après un coup correcte
      </div>
    </div>
  );
}

export default PlayingPageLocal;

//cliquer sur un bouton de la ranger :
// la couleur change
// si il clique sur le bouton une deuxieme fois la couleur part
// si il clique sur un autre boutton la couleur change pour la deuxième et disparait pour le premier
// si il  clique ensuite sur la grille on genere le mouve on envoie au back end

// on attend la réponse du back end
// bon mouv : on retire la piece de la liste on la place sur la grille on termine le tour, et on demande si le jeux est terminer
// mauv mouv : la piece reste selectionner et on affiche un message d'erreur

//cliquer sur un boutton de la grille :
// popup qui demande de deplacer la piece uniquement si la piece est au top
// si il clique une deuxieme fois pop up disparait
// sil clique deplacer la bordure change de couleur et message "choisisser le nouvelle emplacement pour cette piece"
// sil clique sur la meme case on ne fait rien (la grille ne change pas) et on recommence le tour
// si il clique sur une autre case on demande au back
// bon mouv : on retire la piece de sa place on la place sur la nouvelle place et on termine le tour, et on demande si le jeux est terminer
// mauv mouv : la grille reste pareil et on affiche un message d'erreur
