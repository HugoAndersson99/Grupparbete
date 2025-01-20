import React from 'react';
import '../Css/MittKonto_Button.css';

const MittKonto_Button = ({ text, onClick }) => {
  return (
    <button className="mittkonto-button" onClick={onClick}>
      {text}
    </button>
  );
};

export default MittKonto_Button;