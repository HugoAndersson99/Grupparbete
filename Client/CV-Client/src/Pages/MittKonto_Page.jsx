import React from 'react';
import { useNavigate } from 'react-router-dom';
import Header from '../Components/Header';
import Footer from '../Components/Footer';
import CreateCV_Button from '../Components/CreateCV_Button';
import '../Css/MittKonto_Page.css';

const MittKonto_Page = () => {
const navigate = useNavigate();

  return (
    <div className="mitt-konto-page">
      <div className="half-circle"></div>
      <div className='header-container'>
        <Header />
      </div>
      
      <div className="content">
        <h1 className="welcome-text">Välkommen tillbaka</h1>
          <div className="cv-section">
          <div className="cv-header">
            <h1 className="cv-title">Dina CVs</h1>
            <CreateCV_Button
              className="create-cv-button"
              text="Skapa nytt CV"
              onClick={() => navigate('/Build_CV')}
            />
          </div>
          <div className="cv-list">
            <ul>
              <li className="cv-item">
                <span>CV_example1.pdf</span>
                <button className="delete-button">X</button>
              </li>
              <li className="cv-item">
                <span>CV_example2.pdf</span>
                <button className="delete-button">X</button>
              </li>
              <li className="cv-item">
                <span>CV_example3.pdf</span>
                <button className="delete-button">X</button>
              </li>
              <li className="cv-item">
                <span>CV_example4.pdf</span>
                <button className="delete-button">X</button>
              </li>
            </ul>
          </div>
        </div>
      </div>
      <Footer />
    </div>
  );
};

export default MittKonto_Page;
