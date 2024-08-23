import React, { useState } from "react";
import ListOptionGrid from "./ListOptionGrid";
function Grid() {
  const [buttonSelected, setButtonSelected] = useState(null);
  const [selectedOption, setSelectedOption] = useState(null);

  const handleButtonSelected = (index) => {
    if (buttonSelected === index) {
      setButtonSelected(null);
    } else {
      setButtonSelected(index);
    }
  };

  const gridButton = Array.from({ length: 9 }).map((_, i) => {
    return (
      <button
        style={{
          fontSize: "50px",
          width: "100px",
          height: "100px",
          border: "1px solid black",
          backgroundColor: buttonSelected === i ? "red" : "white",
        }}
        className="gridButton"
        id={i}
        key={i}
        onClick={() => handleButtonSelected(i)}
      >
        {buttonSelected === i ? (selectedOption ? selectedOption.text : i) : i}
      </button>
    );
  });

  return (
    <div style={{ display: "flex" }}>
      <div>
        <div style={{ display: "flex" }}>{gridButton.slice(0, 3)}</div>
        <div style={{ display: "flex" }}>{gridButton.slice(3, 6)}</div>
        <div style={{ display: "flex" }}>{gridButton.slice(6, 9)}</div>
      </div>
      <div>
        {buttonSelected !== null ? (
          <ListOptionGrid
            selectedOption={selectedOption}
            setSelectedOption={setSelectedOption}
          />
        ) : null}
      </div>
    </div>
  );
}
export default Grid;
