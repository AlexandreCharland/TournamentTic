import React from "react";

function ListOptionGrid({ selectedOption, setSelectedOption }) {
  const handleOptionClick = (option) => {
    if (selectedOption && selectedOption.text === option.text) {
      setSelectedOption(null);
    } else {
      setSelectedOption(option);
    }
  };

  const values = [
    { symbol: "X", text: "small", nbLeft: 2 },
    { symbol: "X", text: "medium", nbLeft: 2 },
    { symbol: "X", text: "large", nbLeft: 2 },
  ];

  const optionList = values.map((option) => {
    return (
      <div
        key={option.text}
        className="optionGrid"
        style={{
          display: "flex",
          backgroundColor:
            selectedOption && selectedOption.text === option.text
              ? "lightblue"
              : "white",
        }}
        onClick={() => handleOptionClick(option)}
      >
        <div className="optionGridSymbol" style={{ color: "red" }}>
          {option.symbol}
        </div>
        <div className="optionGridText">{`${option.text}, `}</div>
        <div className="optionGridNb">{option.nbLeft}</div>
      </div>
    );
  });
  return <div>{optionList}</div>;
}

export default ListOptionGrid;
