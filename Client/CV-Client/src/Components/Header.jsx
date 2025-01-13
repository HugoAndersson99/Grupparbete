import React from "react";
import '../Css/Header.css';
import logo from '../assets/Images/Logo.png';

const Header = () => {
  return (
    <div className="header">
      <img src={logo} alt="Logo" className="header-logo" />
      <div className="header-text">CV CLONE</div>
    </div>
  );
};

export default Header;
