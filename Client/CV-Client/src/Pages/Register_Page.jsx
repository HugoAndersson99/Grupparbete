import React from 'react';
import Header from '../Components/Header';
import '../Css/Register_Page.css';

function Register_Page() {
  return (
    <div className='register-container'>
      <Header />
      <div className='register-box'>
        <h2 className='register-title'>Registrera dig</h2>
        <label htmlFor='email'>Email</label>
        <input type='email' id='email' placeholder='Email' className='register-input' />
        <label htmlFor='password'>Lösenord</label>
        <input type='password' id='password' placeholder='Lösenord' className='register-input' />
        <label htmlFor='repeat-password'>Upprepa Lösenord</label>
        <input
          type='password'
          id='repeat-password'
          placeholder='Upprepa Lösenord'
          className='register-input'
        />
        <button className='register-button'>Registrera</button>
      </div>
    </div>
  );
}

export default Register_Page;