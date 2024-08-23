import React from "react";

function Greeting() {
  let message = "Good morning";
  let time = new Date(2019, 11, 25, 23, 0, 0).getHours();
  let styles = { color: "red", fontsize: "50px" };

  if (time > 12 && time < 18) {
    message = "Good afternoon";
    styles.color = "green";
    styles.fontsize = "70px";
  } else if (time > 18 && time < 24) {
    message = "Good evening";
    styles.color = "lightblue";
    styles.fontsize = "80px";
  }

  return (
    <div>
      <h1 style={styles}>{message}!</h1>
    </div>
  );
}

export default Greeting;
