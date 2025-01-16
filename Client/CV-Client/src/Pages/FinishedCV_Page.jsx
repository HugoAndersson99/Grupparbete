import React from 'react';
import { useNavigate } from 'react-router-dom';
import Header from '../Components/Header';
import '../Css/FinishedCV_Page.css'
import cv from '../assets/Images/CV.png';
import CreateCV_Button from '../Components/CreateCV_Button';
import MittKonto_Button from '../Components/MittKonto_Button';


const FinishedCV_Page = () => {
  const navigate = useNavigate();
  
  return(
    <div className='finished-page-container'>
    <div className='half-circle'></div>
    <div className='header-container'>
     <Header />
    </div>
    
    <div className='page-text'>
      <h1>Du är klar!<br />Här är ditt nya CV!</h1>
    </div>
    
    <div className='page-content'>
      <img src={cv} alt='CV' className='cv-pic' />
    </div>

    <div className='pdf-button'>
    <CreateCV_Button text="Ladda ned pdf" />
    <MittKonto_Button text="Till mitt konto" onClick={() => navigate('/MittKonto')}/>
    </div>
    

    </div>
  );
};

export default FinishedCV_Page;