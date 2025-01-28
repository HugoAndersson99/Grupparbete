import React from 'react';
import '../Css/Footer.css';
import { Link } from 'react-router-dom';

const Footer = () => {
  return (
    <footer className="footer">
      <div className="footer-section">
        <h4>CV CLONE</h4>
        <ul>
          <li><Link to="/">Hem</Link></li>
          <li><Link to="/login">Logga in</Link></li>
          <li><Link to="/Build_CV">Skapa CV</Link></li>
        </ul>
      </div>
      <div className="footer-section">
        <h4>Kontakt</h4>
        <ul>
          <li><a href="mailto:contact@example.com">Email</a></li>
          <li><a href="https://www.linkedin.com/search/results/all/?fetchDeterministicClustersOnly=true&heroEntityKey=urn%3Ali%3Aorganization%3A1477645&keywords=nbi%2Fhandelsakademin&origin=RICH_QUERY_SUGGESTION&position=0&searchId=e58cae95-db39-427e-be54-76abefe47d85&sid=okl&spellCorrectionEnabled=false">LinkedIn</a></li>
        </ul>
      </div>
      <div className="footer-section">
        <h4>Resurser</h4>
        <ul>
          <li><a href="https://chat.openai.com" target="_blank" rel="noopener noreferrer">Chat-GPT</a></li>
        </ul>
      </div>
    </footer>
  );
};

export default Footer;
