import React from 'react';
import Header from '../Components/Header';
import '../Css/Register_Page.css';

function Register_Page() {
  return (
    <div className='register-container'>
      <div className="half-circle"></div>
      <div className='header-container'>
      <Header />
      </div>
      
      <div className='register-box'>
        <h1 className='register-title'>Registrera dig!</h1>
        <h2 htmlFor='email'>Email</h2>
        <input type='email' id='email' placeholder='Email' className='register-input' />
        <h2 htmlFor='password'>Lösenord</h2>
        <input type='password' id='password' placeholder='Lösenord' className='register-input' />
        <h2 htmlFor='repeat-password'>Upprepa Lösenord</h2>
        <input
          type='password'
          id='repeat-password'
          placeholder='Upprepa Lösenord'
          className='register-input'
        />
        <button className='register-button'>Fortsätt</button>
      </div>
    </div>
  );
}

export default Register_Page;