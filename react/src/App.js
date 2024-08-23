import React from "react";
import MainPage from "./componentForTicTacToe/MainPage";
import PlayingPageLocal from "./componentForTicTacToe/PlayingPageLocal";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<MainPage />} />
        <Route path="/playingLocal" element={<PlayingPageLocal />} />
      </Routes>
    </Router>
  );
}

export default App;
