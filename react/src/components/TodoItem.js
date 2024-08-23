import React from "react";
function TodoItem({ text = "randomItem" }) {
  return (
    <div>
      <input type="checkbox" />
      <label className="todo-item" style={{ fontSize: "50px" }}>
        {text}
      </label>
    </div>
  );
}

export default TodoItem;
