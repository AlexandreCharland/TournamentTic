import React, { useState } from "react";

function Product(props) {
  const [messageVisible, setMessageVisible] = useState(false);
  const toggleMessage = () => setMessageVisible(!messageVisible);
  return (
    <div className="product">
      <button
        className="infoButton"
        onClick={toggleMessage}
        style={{ fontSize: "50px" }}
      >
        i
      </button>
      <img
        className="productimage"
        src={props.product.imagescr}
        alt="product"
      />
      <div className="productNameAndPrice">
        <h3 className="productName">{props.product.name}</h3>
        <p> {props.product.price}</p>
      </div>
      <h1 className="productcategory">{props.product.category}</h1>
      <p className="productstock"> {props.product.stock}</p>

      {/* Conditional Message */}
      {messageVisible && (
        <div className="message">Product ID: {props.product.id}</div>
      )}
    </div>
  );
}
export default Product;
