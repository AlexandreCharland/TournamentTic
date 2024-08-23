import React from "react";

function ButtonGridPlayer({ id, onclickFunction, isSelected }) {
  let style = {
    backgroundColor: isSelected ? "lightblue" : "white",
    border: "1px solid black",
    width: "40px",
    height: "40px",
    display: "inline-block",
    cursor: "pointer",
  };

  if (id[0] === "J") {
    let symbol = id[1];
    let size = id[2];
    return (
      <div>
        <button key={id} style={style} onClick={onclickFunction}>
          {symbol}
          {size}
        </button>
      </div>
    );
  }
  return (
    <div>
      <button key={id} style={style} onClick={onclickFunction}></button>
    </div>
  );
}

export default ButtonGridPlayer;
