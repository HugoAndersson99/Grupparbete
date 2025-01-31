import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import Header from '../Components/Header';
import Footer from '../Components/Footer';
import CreateCV_Button from '../Components/CreateCV_Button';
import '../Css/MittKonto_Page.css';
import { getUserCvs, deleteCv } from '../Services/CV_API';
import { useAuth } from '../Services/AuthContext';

const MittKonto_Page = () => {
  const navigate = useNavigate();
  const { authToken } = useAuth();
  const [userCvs, setUserCvs] = useState([]);
  const [error, setError] = useState('');

  const userId = authToken ? JSON.parse(atob(authToken.split('.')[1])).nameid : null;

  const fetchCvs = async () => {
    try {
      const result = await getUserCvs(userId);
      if (result.success) {
        setUserCvs(result.data);
      } else {
        setError(result.message || "Misslyckades att hämta CVs.");
      }
    } catch (error) {
      setError("Ett fel uppstod. Försök igen senare.");
    }
  };

  useEffect(() => {
    fetchCvs();
  }, [authToken, userId]);

  const handleDeleteCv = async (cvId) => {
    const confirmDelete = window.confirm("Är du säker på att du vill radera detta CV?");
    if (!confirmDelete) return;

    try {
      const result = await deleteCv(cvId);
      if (result.success) {
        setUserCvs((prevCvs) => prevCvs.filter((cv) => cv.id !== cvId));
      } else {
        setError(result.message || "Misslyckades att radera CV.");
      }
    } catch (error) {
      setError("Ett fel uppstod vid radering.");
    }
  };

  return (
    <div className="mitt-konto-page">
      <div className="half-circle"></div>
      <div className='header-container'>
        <Header />
      </div>

      <div className="content">
        <h1 className="welcome-text">Välkommen tillbaka</h1>
        {error && <p className="error-message">{error}</p>}

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
            {userCvs && userCvs.length === 0 ? (
              <p>Du har inga sparade CVs.</p>
            ) : (
              <ul>
                {userCvs && userCvs.map((cv) => (
                  <li key={cv.id} className="cv-item">
                    <span>{cv.title || "CV utan titel"}</span>
                    <button className="delete-button" onClick={() => handleDeleteCv(cv.id)}>X</button>
                  </li>
                ))}
              </ul>
            )}
          </div>
        </div>
      </div>
      <Footer />
    </div>
  );
};

export default MittKonto_Page;
