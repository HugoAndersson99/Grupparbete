import React from 'react';
import { useNavigate } from 'react-router-dom';
import Header from '../Components/Header';
import Footer from '../Components/Footer';
import CreateCV_Button from '../Components/CreateCV_Button';
import '../Css/MittKonto_Page.css';

const MittKonto_Page = () => {
  const navigate = useNavigate();
const { authToken } = useAuth();
const [userCvs, setUserCvs] = useState(null); 
const [error, setError] = useState('');

  const userId = authToken ? JSON.parse(atob(authToken.split('.')[1])).nameid : null;

  const fetchCvs = async () => {
    if (!authToken || !userId) {
      console.error("Ingen användare inloggad.");
      setError("Du måste vara inloggad för att se dina CVs.");
      return;
    }
  
    try {
      console.log("Hämtar CVs för userId:", userId);
      const result = await getUserCvs(userId);
      console.log("CV-lista från servern:", result.data);
  
      if (result.success) {
        setUserCvs(result.data);
      } else {
        setError(result.message || "Misslyckades att hämta CVs.");
      }
    } catch (error) {
      console.error("Fel vid hämtning av CVs:", error);
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
      console.error("Fel vid radering av CV:", error);
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
}
export default MittKonto_Page;

