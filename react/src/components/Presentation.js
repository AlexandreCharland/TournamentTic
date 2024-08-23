import React from "react";

function Presentation(props) {
  console.log(props);

  return (
    <div className="presentation">
      <img
        className="presentation-img"
        src={props.contact.imagescr}
        alt="presentation"
        style={{
          borderRadius: "5%",
          objectFit: "cover",
          width: "100px",
          height: "100px",
        }}
      />
      <h1 className="presentation-name">{props.contact.name}</h1>
      <p className="presentation-info">Phone : {props.contact.Phone}</p>
      <p className="presentation-info">Email : {props.contact.Email}</p>
    </div>
  );
}

export default Presentation;
