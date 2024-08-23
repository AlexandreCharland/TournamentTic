import React from "react";

function Joke(props) {
  let Quest = props.joke.question;
  let Punc = props.joke.punchline;

  return (
    <div className="Joke">
      <h3 style={{ display: Quest ? "block" : "none" }}>Question: {Quest}</h3>
      <p
        style={{
          display: Quest ? "block" : "none",
          fontFamily: "serif",
        }}
      >
        Punchline: {Punc}
      </p>
    </div>
  );
}

export default Joke;
