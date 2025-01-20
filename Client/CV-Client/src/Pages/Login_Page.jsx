import React from 'react';
import { useNavigate } from 'react-router-dom';
import Header from '../Components/Header';
import '../Css/Login_Page.css'

function Login_Page() {
  const navigate = useNavigate();

  return (
    <div className='login-container'>
      <div className="half-circle"></div>
      <div className='header-container'>
      <Header />
      </div>
      <div className='login-box'>
        <h1 className='login-title'>Logga in</h1>
        <h2 htmlFor='email'>Email</h2>
        <input type='email' id='email' placeholder='Email' className='login-input'/>
        <h2 htmlFor="password">Lösenord</h2>
        <input type="password" id="password" placeholder="Lösenord" className="login-input" />
        <button className="login-button">Fortsätt</button>
      </div>
      <div className='login-footer'>
          Inte registrerad?{' '}
          <span className="login-link" onClick={() => navigate('/Register')}>
              Klicka här
          </span>
      </div>
    </div>
  );
};

export default Login_Page;
