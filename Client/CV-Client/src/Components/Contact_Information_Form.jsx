import React from 'react';
import '../Css/Contact_Information_Form.css'

function Contact_Information_Form ({ profilePicture, handleImageUpload}) {

    return (
      <form className = "Contact_Information-Form">

        <div className = "picture-section">
          <img className="profile-picture" src={profilePicture} alt="Profile" />
          <label htmlFor="upload-image" className="custom-image-upload-button">
            Ladda upp bild
          </label>
          <input
            className="upload-image-input"
            type="file"
            id = "upload-image"
            accept="image/*"
            onChange={handleImageUpload}
          />
        </div>

        <div className = "input-fields-1">
          
          <div className = "input-group-1">
            <label for = "name">Namn</label>
            <input type = "text" id = "name" placeholder = "Namn"></input>
          </div>

          <div className = "input-group-1">
            <label for = "adress">Adress</label>
            <input type = "text" id = "adress" placeholder = "Adress"></input>
          </div>

          <div className = "input-group-1">
            <label for = "postnummer">Postnummer</label>
            <input type = "number" id = "postnummer" placeholder = "Postnummer"></input>
          </div>

          <div className = "input-group-1">
            <label for = "telefonnummer">Telefonnummer</label>
            <input type = "tel" id = "telefonnummer" placeholder = "Telefonnummer"></input>
          </div>

          <div className = "input-group-1">
            <label for = "e-mail">E-mail</label>
            <input type = "email" id = "e-mail" placeholder = "E-mail"></input>
          </div>

          <div className = "input-group-1">
            <label for = "linkedIn">LinkedIn</label>
            <input type = "text" id = "linkedin" placeholder = "LinkedIn-URL"></input>
          </div>

          <div className = "input-group-1" id = "länk-container">
            <label for = "länk">Annan länk</label>
            <input type = "text" id = "länk" placeholder = "Annan URL"></input>
          </div>

        </div>

      </form>
    );
};

export default Contact_Information_Form;