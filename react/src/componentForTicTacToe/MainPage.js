import React from "react";
import ButtonLocal from "./ButtonLocal";
import ButtonOnline from "./ButtonOnline";

function MainPage() {
  return (
    <div>
      <h1> 3D Tic-Tac-Toe Game</h1>
      <div>
        <h2>Règles du jeu</h2>
        <ul>
          <li>1</li>
          <li>2</li>
          <li>3</li>
        </ul>
      </div>

      <div>
        <h2>Lancer une partie</h2>
      </div>

      <ButtonLocal />
      <ButtonOnline />
    </div>
  );
}

export default MainPage;
