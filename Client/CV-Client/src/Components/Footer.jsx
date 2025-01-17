import React from 'react';
import '../Css/Footer.css';

const Footer = () => {
  return (
    <footer className="footer">
      <div className="footer-section">
        <h4>CV CLONE</h4>
        <ul>
          <li>Hem</li>
          <li>Logga in</li>
          <li>Skapa CV</li>
        </ul>
      </div>
      <div className="footer-section">
        <h4>Kontakt</h4>
        <ul>
          <li>Email</li>
          <li>LinkedIn</li>
        </ul>
      </div>
      <div className="footer-section">
        <h4>Resurser</h4>
        <ul>
          <li>Chat-GPT</li>
        </ul>
      </div>
    </footer>
  );
};

export default Footer;
