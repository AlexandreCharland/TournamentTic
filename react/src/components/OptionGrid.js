import React from "react";

function OptionGrid(props) {
  return (
    <div
      className="optionGrid"
      style={{ display: "flex" }}
      onClick={() => console.log("test")}
    >
      <div className="optionGridSymbol" style={{ color: "red" }}>
        {props.symbol}
      </div>
      <div className="optionGridText"> - {props.text}</div>
      <div className="optionGridNb"> - {props.nbLeft}</div>
    </div>
  );
}

export default OptionGrid;
