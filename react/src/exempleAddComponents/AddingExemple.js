import React, { useState } from "react";

function AddingExemple() {
  const [letterSelected, setLetterSelected] = useState(null);
  const [letters, setLetters] = useState(["A", "B", "C", "D"]); // Utilisez l'état pour les lettres

  function handleClick(letter) {
    if (letterSelected === letter) {
      setLetterSelected(null);
    } else {
      setLetterSelected(letter);
    }
  }

  function deleteLetter() {
    if (letterSelected !== null) {
      setLetters((prevLetters) =>
        prevLetters.filter((letter) => letter !== letterSelected)
      );
      setLetterSelected(null); // Réinitialiser la sélection
    }
  }

  function letterTableToString() {
    return letters.join(" ") + "\n";
  }

  function DeleteMessage() {
    if (letterSelected !== null) {
      return (
        <div>
          Supprimer la lettre {letterSelected} ?
          <div>
            <button onClick={() => deleteLetter()}>Oui</button>
            <button onClick={() => setLetterSelected(null)}>Non</button>
          </div>
        </div>
      );
    }
  }

  let buttons = letters.map((letter) => (
    <button
      key={letter}
      onClick={() => handleClick(letter)}
      style={{
        margin: "10px",
        border: "1px solid black",
        backgroundColor: letterSelected === letter ? "lightblue" : "white",
      }}
    >
      {letter}
    </button>
  ));

  return (
    <div>
      AddingExemple
      <div>{letterTableToString()}</div>
      <div>{buttons}</div>
      <div>{DeleteMessage()}</div>
    </div>
  );
}

export default AddingExemple;
