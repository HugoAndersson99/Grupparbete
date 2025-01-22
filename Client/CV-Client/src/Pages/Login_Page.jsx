import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import Header from '../Components/Header';
import '../Css/Login_Page.css';
import { loginUser } from '../Services/User_API'; 

function Login_Page() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [errorMessage, setErrorMessage] = useState('');
  const navigate = useNavigate();

  const handleLogin = async (event) => {
    event.preventDefault();

    const loginData = {
      email: email,
      passwordHash: password, 
    };

    const result = await loginUser(loginData);

    if (result.success) {
      
      sessionStorage.setItem('authToken', result.token);
      navigate('/Mitt_Konto'); 
    } else {
      setErrorMessage(result.message); 
    }
  };

  return (
    <div className="login-container">
      <div className="half-circle"></div>
      <div className="header-container">
        <Header />
      </div>
      <div className="login-box">
        <h1 className="login-title">Logga in</h1>
        <h2 htmlFor="email">Email</h2>
        <input
          type="email"
          id="email"
          placeholder="Email"
          className="login-input"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
        <h2 htmlFor="password">Lösenord</h2>
        <input
          type="password"
          id="password"
          placeholder="Lösenord"
          className="login-input"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
        <button className="login-button" onClick={handleLogin}>Fortsätt</button>
        {errorMessage && <div className="error-message">{errorMessage}</div>}
      </div>
      <div className="login-footer">
        Inte registrerad?{' '}
        <span className="login-link" onClick={() => navigate('/Register')}>
          Klicka här
        </span>
      </div>
    </div>
  );
}

export default Login_Page;
