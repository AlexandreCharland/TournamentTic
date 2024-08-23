import React from "react";
import { useNavigate } from "react-router-dom";
function ButtonOnline() {
  let navigate = useNavigate();
  // main page pour l'instant
  const handleClick = () => {
    navigate("/");
  };
  return (
    <div>
      <button onClick={handleClick} style={{ margin: "10px" }}>
        En ligne
      </button>
    </div>
  );
}
export default ButtonOnline;
