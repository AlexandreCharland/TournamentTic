import React from "react";
import { useNavigate } from "react-router-dom";

function ButtonLocal() {
  let navigate = useNavigate();

  const handleClick = () => {
    navigate("/playingLocal");
  };

  return (
    <div>
      <button onClick={handleClick} style={{ margin: "10px" }}>
        Local (même machine)
      </button>
    </div>
  );
}
export default ButtonLocal;
