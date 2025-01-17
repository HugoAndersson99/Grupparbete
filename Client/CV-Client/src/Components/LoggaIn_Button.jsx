import React from 'react';
import '../Css/LoggaIn_Button.css';

const LoggaIn_Button = ({ text, onClick }) => {
  return (
    <button className="loggain-button" onClick={onClick}>
      {text}
    </button>
  );
};

export default LoggaIn_Button;