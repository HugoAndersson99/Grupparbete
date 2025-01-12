import React from 'react';
import { useNavigate } from 'react-router-dom';
import Header from '../Components/Header';
import '../Css/Login_Page.css'

function Login_Page() {
  const navigate = useNavigate();

  return (
    <div className='login-container'>
      <Header />
      <div className='login-box'>
        <h2 className='login-title'>Logga in</h2>
        <label htmlFor='email'>Email</label>
        <input type='email' id='email' placeholder='Email' className='login-input'/>
        <label htmlFor="password">Lösenord</label>
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
