import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom'; 
import Header from '../Components/Header';
import '../Css/Register_Page.css';
import { registerUser } from '../Services/User_API'; 

function Register_Page() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [repeatPassword, setRepeatPassword] = useState('');
  const [errorMessage, setErrorMessage] = useState('');
  const [successMessage, setSuccessMessage] = useState('');
  const navigate = useNavigate(); 

  const handleRegister = async (event) => {
    event.preventDefault();
    setErrorMessage('');
    setSuccessMessage('');

    if (password !== repeatPassword) {
      setErrorMessage('Lösenorden matchar inte.');
      return;
    }

    const newUser = {
      email: email,
      passwordHash: password, 
    };

    const result = await registerUser(newUser);

    if (result.success) {
      setSuccessMessage('Registrering lyckades! Omdirigerar till login...');
      setTimeout(() => {
        navigate('/Login');
      }, 2000); 
    } else {
      setErrorMessage(result.message);
    }
  };

  return (
    <div className='register-container'>
      <div className="half-circle"></div>
      <div className='header-container'>
        <Header />
      </div>
      <div className='register-box'>
        <h1 className='register-title'>Registrera dig!</h1>
        <h2 htmlFor='email'>Email</h2>
        <input
          type='email'
          id='email'
          placeholder='Email'
          className='register-input'
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
        <h2 htmlFor='password'>Lösenord</h2>
        <input
          type='password'
          id='password'
          placeholder='Lösenord'
          className='register-input'
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
        <h2 htmlFor='repeat-password'>Upprepa Lösenord</h2>
        <input
          type='password'
          id='repeat-password'
          placeholder='Upprepa Lösenord'
          className='register-input'
          value={repeatPassword}
          onChange={(e) => setRepeatPassword(e.target.value)}
        />
        <button className='register-button' onClick={handleRegister}>Fortsätt</button>
        {errorMessage && <div className="error-message">{errorMessage}</div>}
        {successMessage && <div className="success-message">{successMessage}</div>}
      </div>
    </div>
  );
}

export default Register_Page;
