import React, { useState } from 'react';
import '../Css/Build_CV_Page.css'
import Picture_model from '../assets/Images/Picture-model.png'
import CV_model from '../assets/Images/Mock upm CV.png'
import Work_Experience_Form from '../Components/Work_Experience_Form.jsx';
import Education_Form from '../Components/Education_Form.jsx';
import Competencies_Form from '../Components/Competencies_Form.jsx';
import About_Me_Form from '../Components/About_Me_Form.jsx';
import Contact_Information_Form from '../Components/Contact_Information_Form.jsx';


function Make_CV_Page() {
  
  const [profilePicture, setProfilePicture] = useState(Picture_model);
  const [workExperienceCount, setWorkExperienceCount] = useState(1);
  const [educationCount, setEducationCount] = useState(1)
  const [competenciesCount, setCompetenciesCount] = useState(1);

  const handleAddWorkExperience = () => 
  {
    setWorkExperienceCount(workExperienceCount + 1);
  };
  const handleAddEducation = () => 
  {
    setEducationCount(educationCount + 1);
  };
  const handleAddCompetence = () =>
  {
    setCompetenciesCount(competenciesCount + 1);
  };

  const renderWorkExperienceForms = () => 
  {
    const forms = [];
    for (let numberOfForms = 0; numberOfForms < workExperienceCount; numberOfForms++) 
    {
      forms.push(<Work_Experience_Form key={numberOfForms} />);
    }
    return forms;
  };
  const renderEducationForms = () => 
  {
    const forms = [];
    for (let numberOfForms = 0; numberOfForms < educationCount; numberOfForms++) 
    {
      forms.push(<Education_Form key={numberOfForms} />);
    }
    return forms;
  };
  const renderCompetenciesForms = () =>
  {
    const forms = [];
    for (let numberOfForms = 0; numberOfForms < competenciesCount; numberOfForms++) 
    {
      forms.push(<Competencies_Form key={numberOfForms} />);
    }
    return forms;
  };

  const handleImageUpload = (event) => {
    const file = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = () => {
        setProfilePicture(reader.result);
      };
      reader.readAsDataURL(file);
    }
  };

  return (
    <div className = "container">

      {/* Formulär */}
      <div className = "form-section">

        {/* Kontakt information-section */}
        <form className = "contact_information-section">

          <h1 className = "header-section-1">
            Kontaktuppgifter
          </h1>

          <Contact_Information_Form 
            profilePicture={profilePicture} 
            handleImageUpload={handleImageUpload} 
          />
          
          <div className="section-breaker" />  
          
        </form>

        {/* Arbetslivserfarenhet-section */}
        <form className = "work-experience-section">
  
          <h1 className="header-section-2">
            Arbetslivserfarenhet
          </h1>

          {renderWorkExperienceForms()}

          <div className="add_new_input_fields-section">
            <p className="add_new_input_fields-link" onClick = {handleAddWorkExperience}>
              + Lägg till ytterligare erfarenhet
            </p>
            <button className="AI-button">
              Få hjälp av AI <i class="fa-solid fa-square-binary"></i>
            </button>
          </div>

          <div className="section-breaker" />   

        </form>

        {/* Utbildning-section */}
        <form className = "education-experience">
          
          <h1 className = "header-section-3">
            Utbildning
          </h1>

          {renderEducationForms()}

          <div className = "add_new_input_fields-section">
            <p className = "add_new_input_fields-link" onClick={handleAddEducation}>
              + Lägg till ytterliggare erfarenhet
            </p>
            <button className = "AI-button">
              Få hjälp av AI <i class="fa-solid fa-square-binary" id = "AI-icon"></i>
            </button>
          </div>

          <div className = "section-breaker" />

        </form>

        {/* Kompetens-section */}
        <form className = "competence-section">
          
          <h1 className = "header-section-4">
            Kompetenser
          </h1>
          
          {renderCompetenciesForms()}

          <div className = "add_new_input_fields-section">
            <p className = "add_new_input_fields-link" onClick={handleAddCompetence}>
              + Lägg till ytterliggare erfarenhet
            </p>
            <button className = "AI-button">
              Få hjälp av AI <i class="fa-solid fa-square-binary"></i>
            </button>
          </div>

          <div className = "section-breaker" />

        </form>

        {/* Om mig-section */}
        <form className = "about_me-section">
          
          <h1 className = "header-section-4">
            Om mig
          </h1>

          <About_Me_Form />

          <div className = "add_new_input_fields-section" id = "add_new_input_fields-About_Me-section">
            <button className = "AI-button">
              Få hjälp av AI <i class="fa-solid fa-square-binary"></i>
            </button>
          </div>

        </form>

        <div className = "submit-button-container">

          <button className = "submit_CV-button">
            Färdig!
          </button>

        </div>

      </div>

      {/* CV-mall */}
      <div className = "CV-section">
        <img className = "CV-mall" src = {CV_model}></img>
      </div>

    </div>
  )
};

export default Make_CV_Page;