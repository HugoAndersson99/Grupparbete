import React from 'react';
import '../Css/Contact_Information_Form.css'

function Contact_Information_Form ({ profilePicture, handleImageUpload, handleInputChange, name, address, zip_code, phoneNumber, email, linkedin, otherLink}) {

    return (
      <form className = "Contact_Information-Form">

        <div className = "picture-section">
          <img className = "profile-picture" src = {profilePicture} alt="Profile" />
          <label htmlFor = "upload-image" className = "custom-image-upload-button">
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
            <label htmlFor = "name">Namn</label>
            <input
              type="text"
              id="name"
              name="name"
              placeholder="Namn"
              maxLength="30"
              value={name}
              onChange={handleInputChange}
            />
          </div>

          <div className = "input-group-1">
            <label htmlFor = "adress">Adress</label>
            <input 
              type = "text" 
              id = "adress" 
              name="address"
              placeholder = "Adress"
              maxLength="30" 
              value={address}
              onChange={handleInputChange}
            />
          </div>

          <div className = "input-group-1">
            <label htmlFor = "postnummer">Postnummer</label>
            <input 
              type = "number" 
              id = "postnummer" 
              name="zip-code"
              placeholder = "Postnummer"
              pattern="[0-9]{3}-[0-9]{2}"
              maxLength={5} 
              value={zip_code}
              onChange={handleInputChange}
            />
          </div>

          <div className = "input-group-1">
            <label htmlFor = "telefonnummer">Telefonnummer</label>
            <input 
              type = "tel" 
              id = "telefonnummer" 
              name="phoneNumber"
              placeholder = "Telefonnummer"
              pattern="[0-9]{3}-[0-9]{3}-[0-9]{4}"
              maxLength="10" 
              value={phoneNumber}
              onChange={handleInputChange}
            />
          </div>

          <div className = "input-group-1">
            <label htmlFor = "e-mail">E-mail</label>
            <input 
              type = "email" 
              id = "e-mail"
              name="email"
              placeholder = "E-mail"
              maxLength="30"
              value={email}
              onChange={handleInputChange}
            />
          </div>

          <div className = "input-group-1">
            <label htmlFor = "linkedIn">LinkedIn</label>
            <input 
              type = "url" 
              id = "linkedin" 
              name="linkedin"
              placeholder = "LinkedIn-URL"
              maxLength="40"
              value={linkedin}
              onChange={handleInputChange}
            />
          </div>

          <div className = "input-group-1" id = "länk-container">
            <label htmlFor = "länk">Annan länk</label>
            <input 
            type = "url" 
            id = "länk"
            name="otherLink" 
            placeholder = "Annan URL"
            maxLength="40"
            value={otherLink}
            onChange={handleInputChange}
            />
          </div>

        </div>

      </form>
    );
};

export default Contact_Information_Form;