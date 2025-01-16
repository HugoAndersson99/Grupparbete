import React from 'react';
import '../Css/CreateCV_Button.css';

const CreateCV_Button = ({ text, onClick }) => {
  return (
    <button className="create-cv-button" onClick={onClick}>
      {text}
    </button>
  );
};

export default CreateCV_Button;
