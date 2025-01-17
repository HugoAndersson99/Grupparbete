import React from 'react';
import { useNavigate } from 'react-router-dom';
import Header from '../Components/Header';
import Footer from '../Components/Footer';
import '../Css/Home_Page.css';
import diagram from '../assets/Images/Diagram.png';
import pic1 from '../assets/Images/Pic1.png';
import pic2 from '../assets/Images/Pic2.png';
import pic3 from '../assets/Images/Pic3.png';
import screen from '../assets/Images/Screen.png';
import folder from '../assets/Images/Folder.png';
import CreateCV_Button from '../Components/CreateCV_Button';
import LoggaIn_Button from '../Components/LoggaIn_Button';

const Home_Page = () => {
  const navigate = useNavigate();

  return (
    <div className="home-page-container">
      <div className="half-circle"></div>
      <div className='header-container'>
        <Header />
        <div className='header-buttons'>
        <LoggaIn_Button text="Logga In" onClick={() => navigate('/Login')}/>
        <CreateCV_Button text="Skapa CV" onClick={() => navigate('/Build_CV')} />
        </div>

      </div>
      
      <section className="home">
        <div className="home-content">
          <h1>Skapa Ditt CV<br />Med Hjälp Av<br />AI!</h1>
          <p>Antagligen den bästa hjälpen<br />du kan få på nätet.</p>
          <CreateCV_Button text="Skapa CV" onClick={() => navigate('/Build_CV')} />
        </div>
        <img src={diagram} alt="Diagram" className="home-chart" />
      </section>

      <h2 className="testimonials-title">Se Vad Andra Användare Tycker!</h2> 
      <section className="testimonials">
        <div className="testimonials-grid">
          <div className="testimonial">
            <img src={pic1} alt="Henry" className="avatar" />
            <p className='review-name'>Henry Williams</p>
            <p className='review-text'>"Woah, this AI dude seems like a nice guy!"</p>
          </div>
          <div className="testimonial">
            <img src={pic2} alt="Anna" className="avatar" />
            <p className='review-name'>Anna Svensson</p>
            <p className='review-text'>"Min favoritfärg är grön! Jag gillar hästar också!"</p>
          </div>
          <div className="testimonial">
            <img src={pic3} alt="Linn" className="avatar" />
            <p className='review-name'>Linn af Gylenstjerna</p>
            <p className='review-text'>"CV? Haha, jag behöver inte jobba. Pappa betalar allt!"</p>
          </div>
        </div>
      </section>

      <section className="features">
        <div className="feature feature-left">
          <div className="feature-text">
            <h1>Inbyggd AI <br />Hjälper Dig Med <br />Vad Du Ska<br /> Skriva!</h1>
            <CreateCV_Button text="Skapa CV" onClick={() => navigate('/Build_CV')} />
          </div>
          <img src={screen} alt="Screen" className="feature-icon" />
        </div>

        <div className="feature feature-right">
          <img src={folder} alt="Folder" className="feature-icon" />
          <div className="feature-text">
            <h1>Färdiga Mallar<br /> Låter Dig Välja<br /> Din Optimala<br /> Stil!</h1>
            <CreateCV_Button text="Skapa CV" onClick={() => navigate('/Build_CV')} />
          </div>
        </div>
      </section>

      <Footer />
    </div>
  );
};

export default Home_Page;
