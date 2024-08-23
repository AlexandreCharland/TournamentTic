import React from "react";
import { useState } from "react";
import ButtonGridPlayer from "./ButtonGridPlayer";
import ButtonGrid from "./ButtonGrid";

function Grid() {
  const [player, setPlayer] = useState("J1");
  const [moveMessage, setMoveMessage] = useState("");
  const [previousMove, setPreviousMove] = useState(null);
  const [moveAskingController, setMoveAskingController] = useState(false);

  function changePlayer() {
    if (player === "J1") {
      setPlayer("J2");
      setJ1buttonSelected(null);
    } else {
      setPlayer("J1");
      setJ2buttonSelected(null);
    }
  }

  // for the grid
  const [buttonSelected, setButtonSelected] = useState(null);
  const [GridButtonId, setGridButtonId] = useState({
    0: ["0"],
    1: ["1"],
    2: ["2"],
    3: ["3"],
    4: ["4"],
    5: ["5"],
    6: ["6"],
    7: ["7"],
    8: ["8"],
  });

  function handleClick(position) {
    if (buttonSelected === position) {
      setButtonSelected(null);
    } else {
      setButtonSelected(position);
    }

    let playerButtonSelected =
      player === "J1" ? J1buttonSelected : J2buttonSelected;
    let setterPieceIdforPlayer =
      player === "J1" ? setJ1piecesId : setJ2piecesId;
    let setterPlayerbuttonSelected =
      player === "J1" ? setJ1buttonSelected : setJ2buttonSelected;

    //DANS LE CAS DE PLACER UNE PIECE
    if (playerButtonSelected !== null) {
      //requete au back end
      // playerButtonSelected[2] + 9 + position
      // -------------------------
      // si oui
      // on ajoute à la map
      setGridButtonId((prev) => {
        const newGridButtonId = { ...prev }; // Créez une nouvelle copie de l'objet
        newGridButtonId[position] = [
          ...newGridButtonId[position],
          playerButtonSelected,
        ]; // Ajoutez l'élément sélectionné
        return newGridButtonId;
      });
      //et on suprimme de la liste J1
      setterPieceIdforPlayer((prev) => {
        return prev.filter((piece) => piece !== playerButtonSelected);
      });
      setterPlayerbuttonSelected(null);

      //on change de joueur
      changePlayer();
      // si non
      //message invalide et on ne fait rien
    } else if (playerButtonSelected === null) {
      //DANS LE CAS DE DÉPLACER UNE PIECE
      if (!moveAskingController) {
        let value = GridButtonId[position];
        if (GridButtonId[position][value.length - 1][1] === player[1]) {
          setMoveMessage(
            "Voulez vous bouger la piece " +
              GridButtonId[position][value.length - 1] +
              " ?"
          );
          setPreviousMove(position);
        }
      } else if (moveAskingController) {
        if (previousMove === position) {
          setMoveAskingController(false);
          setMoveMessage("");
        } else {
          let lengthPrevious = GridButtonId[previousMove].length;
          let idToMove = GridButtonId[previousMove][lengthPrevious - 1];
          //requete au back end
          //idToMove[2] + previous + position
          // si oui on déplace
          setGridButtonId((prev) => {
            const newGridButtonId = { ...prev };
            newGridButtonId[previousMove] = newGridButtonId[
              previousMove
            ].filter((piece) => piece !== idToMove);
            return newGridButtonId;
          });
          setGridButtonId((prev) => {
            const newGridButtonId = { ...prev };
            newGridButtonId[position] = [
              ...newGridButtonId[position],
              idToMove,
            ];
            return newGridButtonId;
          });
          setMoveAskingController(false);
          setMoveMessage("");
          setPreviousMove(null);
          changePlayer();
        }
        // si non
        // setMoveAskingController(false);
        // setMoveMessage(""); // + message d'erreur
      }
    }
  }

  let grid = Object.keys(GridButtonId).map((index) => {
    let value = GridButtonId[index];
    let length = value.length;
    return (
      <ButtonGrid
        key={index} // Ajoutez une clé unique
        id={index}
        onclickFunction={() => handleClick(index)}
        isSelected={buttonSelected === index}
        idToDisplay={value[length - 1]} // Affichez le dernier élément du tableau
      />
    );
  });

  let gridDisplay = (
    <div>
      <div style={{ display: "flex" }}>{grid.slice(0, 3)}</div>
      <div style={{ display: "flex" }}>{grid.slice(3, 6)}</div>
      <div style={{ display: "flex" }}>{grid.slice(6, 9)}</div>
    </div>
  );

  //------------for J1-----------------
  const [J1buttonSelected, setJ1buttonSelected] = useState(null);
  const [J1piecesId, setJ1piecesId] = useState([
    "J100",
    "J101",
    "J110",
    "J111",
    "J120",
    "J121",
  ]);

  function handleClickJ1(position) {
    if (moveMessage === "" && player === "J1") {
      if (J1buttonSelected === position) {
        setJ1buttonSelected(null);
      } else {
        setJ1buttonSelected(position);
      }
    }
  }

  let J1Pieces = J1piecesId.map((id) => {
    return (
      <ButtonGridPlayer
        id={id}
        onclickFunction={() => handleClickJ1(id)}
        isSelected={J1buttonSelected === id}
      />
    );
  });

  //------------for J2-----------------
  const [J2buttonSelected, setJ2buttonSelected] = useState(null);
  const [J2piecesId, setJ2piecesId] = useState([
    "J200",
    "J201",
    "J210",
    "J211",
    "J220",
    "J221",
  ]);

  function handleClickJ2(position) {
    if (moveMessage === "" && player === "J2") {
      if (J2buttonSelected === position) {
        setJ2buttonSelected(null);
      } else {
        setJ2buttonSelected(position);
      }
    }
  }

  let J2Pieces = J2piecesId.map((id) => {
    return (
      <ButtonGridPlayer
        id={id}
        onclickFunction={() => handleClickJ2(id)}
        isSelected={J2buttonSelected === id}
      />
    );
  });

  //-----------------end of J1 and J2------------------

  let moveMessageComponent = (
    <div>
      {moveMessage && (
        <div>
          <div>{moveMessage}</div>
          <div style={{ display: "flex" }}>
            <button
              onClick={() => {
                setMoveMessage("Choisissez la nouvelle case");
                setMoveAskingController(true);
              }}
            >
              OUI
            </button>
            <button
              onClick={() => {
                setMoveMessage("");
                setMoveAskingController(false);
              }}
            >
              NON
            </button>
          </div>
        </div>
      )}
    </div>
  );

  let playerText = <div>C'est au tour de {player} de jouer !</div>;
  let piecesToDisplay = player === "J1" ? J1Pieces : J2Pieces;

  return (
    <div>
      <div style={{ display: "flex" }}>
        {gridDisplay}
        {moveMessageComponent}
      </div>

      <div>{playerText}</div>
      <div style={{ display: "flex" }}>{piecesToDisplay} </div>

      <div>{J1buttonSelected}</div>
      <div>{J2buttonSelected}</div>
      <div>{buttonSelected}</div>
    </div>
  );
}

export default Grid;
