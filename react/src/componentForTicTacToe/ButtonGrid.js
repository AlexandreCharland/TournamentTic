import React from "react";

function ButtonGrid({ id, onclickFunction, isSelected, idToDisplay }) {
  let style = {
    backgroundColor: isSelected ? "lightblue" : "white",
    border: "1px solid black",
    width: "40px",
    height: "40px",
    display: "inline-block",
    cursor: "pointer",
  };
  if (idToDisplay.length === 1) {
    return (
      <div>
        <button key={id} style={style} onClick={onclickFunction}></button>
      </div>
    );
  }
  if (idToDisplay[0] === "J") {
    let symbol = idToDisplay[1];
    let size = idToDisplay[2];
    return (
      <div>
        <button key={id} style={style} onClick={onclickFunction}>
          {symbol}
          {size}
        </button>
      </div>
    );
  }
}

export default ButtonGrid;
