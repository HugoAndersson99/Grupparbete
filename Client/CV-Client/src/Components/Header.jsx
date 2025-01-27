import React from "react";
import '../Css/Header.css';
import logo from '../assets/Images/Logo.png';
import { useNavigate } from 'react-router-dom';



const Header = () => {
  const navigate = useNavigate();
  const handleNavigate = () => {
    navigate('/');
  };
  return (
    <div className="header">
      <img src={logo} alt="Logo" className="header-logo" onClick={handleNavigate}/>
      <div className="header-text" onClick={handleNavigate}>CV CLONE</div>
    </div>
  );
};

export default Header;
