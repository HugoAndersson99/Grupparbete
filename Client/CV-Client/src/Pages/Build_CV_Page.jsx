import React, { useState } from 'react';
import '../Css/Build_CV_Page.css'
import Picture_model from '../assets/Images/cat-5646889_1920.jpg'
import Work_Experience_Form from '../Components/Work_Experience_Form.jsx';
import Education_Form from '../Components/Education_Form.jsx';
import Competencies_Form from '../Components/Competencies_Form.jsx';
import About_Me_Form from '../Components/About_Me_Form.jsx';
import Contact_Information_Form from '../Components/Contact_Information_Form.jsx';
import CV_Modell from '../Components/CV_Modell.jsx';


function Make_CV_Page() {
  
  const [profilePicture, setProfilePicture] = useState(Picture_model);
  const [workExperienceCount, setWorkExperienceCount] = useState(1);
  const [educationCount, setEducationCount] = useState(1)
  const [competenciesCount, setCompetenciesCount] = useState(1);

  {/* Contact Information */}
  const [name, setName] = useState("Katt Kattson");
  const [address, setAddress] = useState("Göterborg gatan 45");
  const [zip_code, setZip_Code] = useState("690 05");
  const [phoneNumber, setPhoneNumber] = useState("0789 567385");
  const [email, setEmail] = useState("Minhotmail.com");
  const [linkedin, setLinkedIn] = useState("www.linkedIn.se");
  const [otherLink, setOtherLink] = useState("www.minadress.se");

  {/* About Me information */}
  const [about_Me, setAbout_Me] = useState("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse nec tortor sed metus fermentum tincidunt. Nam accumsan libero id malesuada dignissim.Mauris dictum convallis ipsum id tincidunt. Nulla facilisi. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Etiam auctor sapien sed justo convallis, vitae interdum sapien dapibus. Sed nec semper justo. Curabitur varius magna in ipsum malesuada, quis accumsan felis ornare. Integer quis ipsum ac justo pulvinar vulputate sed in risus. Vivamus eget lacus pharetra, volutpat est vel, pretium arcu.");

  {/* Education information*/}
  const [job_title, setJob_title] = useState("Lärare");
  const [job_employer, setJob_employer] = useState("Hageby Skolan");
  const [job_city, setJob_city] = useState("Gävle");
  const [job_start_date, setJob_start_date] = useState("12-05-2013");
  const [job_end_date, setJob_end_date] = useState("14-06-2024");
  const [job_description, setJob_description] = useState("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse nec tortor sed metus fermentum tincidunt. Nam accumsan libero id malesuada dignissim.");

  {/* Work-experience information*/}
  const [education_school, setEducation_school] = useState("Linköpings Universitet");
  const [education_program, setEducation_program] = useState("Socionom");
  const [education_start_date, setEducation_start_date] = useState("12-05-2013");
  const [education_end_date, setEducation_end_date] = useState("14-06-2024");
  const [education_description, setEducation_description] = useState("Lorem ipsum dolor sit amet, consectetur adipiscing elit.");

  {/* Competencies information*/}
  const [competence_name, setCompetence_name] = useState("C#");
  const [competence_level, setCompetence_level] = useState("");
  const [competence_description, setCompetence_description] = useState("Lorem ipsum dolor sit amet, consectetur adipiscing elit.");
  

  {/* Function to update props when input changes */}
  const handleInputChange = (inputEvent) => {
    const { name, value } = inputEvent.target;
    switch (name) {
      case 'name':
        setName(value);
        break;
      case 'address':
        setAddress(value);
        break;
      case 'zip-code':
        setZip_Code(value);
        break;
      case 'phoneNumber':
        setPhoneNumber(value);
        break;
      case 'email':
        setEmail(value);
        break;
      case 'linkedin':
        setLinkedIn(value);
        break;
      case 'otherLink':
        setOtherLink(value);
        break;
      case 'about_Me':
        setAbout_Me(value);
        break;
      case 'job_title':
        setJob_title(value);
        break;
      case 'job_employer':
        setJob_employer(value);
        break;
      case 'job_city':
        setJob_city(value);
        break;
      case 'job_start_date':
        setJob_start_date(value);
        break;
      case 'job_end_date':
        setJob_end_date(value);
        break;
      case 'job_description':
        setJob_description(value);
        break;
      case 'education_school':
        setEducation_school(value);
        break;
      case 'education_program':
        setEducation_program(value);
        break;
      case 'education_start_date':
        setEducation_start_date(value);
        break;
      case 'education_end_date':
        setEducation_end_date(value);
        break;
      case 'education_description':
        setEducation_description(value);
        break;
      case 'competence_name':
        setCompetence_name(value);
        break;
      case 'competence_level':
        setCompetence_level(value);
        break;
      case 'competence_description':
        setCompetence_description(value);
        break;
      default:
        break;
    }
  };

  const handleAddWorkExperience = () => 
  {
    setWorkExperienceCount(workExperienceCount + 1);
  };
  const handleRemoveWorkExperience = () => 
  {
    setWorkExperienceCount(workExperienceCount - 1);
  };

  const handleAddEducation = () => 
  {
    setEducationCount(educationCount + 1);
  };
  const handleRemoveEducation = () => 
    {
      setEducationCount(educationCount - 1);
    };
  
  const handleAddCompetence = () =>
  {
    setCompetenciesCount(competenciesCount + 1);
  };
  const handleRemoveCompetence = () =>
    {
      setCompetenciesCount(competenciesCount - 1);
    };

  const renderWorkExperienceForms = () => 
  {
    const forms = [];
    for (let numberOfForms = 0; numberOfForms < workExperienceCount; numberOfForms++) 
    {
      forms.push(<Work_Experience_Form 
        key={numberOfForms} 
        job_title={job_title}
        job_employer={job_employer}
        job_city={job_city}
        job_start_date={job_start_date}
        job_end_date={job_end_date}
        job_description={job_description}
        handleInputChange={handleInputChange}
        />);
    }
    return forms;
  };

  const renderEducationForms = () => 
  {
    const forms = [];
    for (let numberOfForms = 0; numberOfForms < educationCount; numberOfForms++) 
    {
      forms.push(<Education_Form 
        key={numberOfForms} 
        education_school={education_school}
        education_program={education_program}
        education_start_date={education_start_date}
        education_end_date={education_end_date}
        education_description={education_description}
        handleInputChange={handleInputChange}
        />);
    }
    return forms;
  };

  const renderCompetenciesForms = () =>
  {
    const forms = [];
    for (let numberOfForms = 0; numberOfForms < competenciesCount; numberOfForms++) 
    {
      forms.push(<Competencies_Form 
        key={numberOfForms} 
        competence_name={competence_name}
        competence_level={competence_level}
        competence_description={competence_description}
        handleInputChange={handleInputChange}
      />);
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

      {/*------------------------------Formulär------------------------------*/}
      <div className = "form-section">

        {/* Kontakt information-section */}
        <form className = "contact_information-section">

          <h1 className = "header-section-1">
            Kontaktuppgifter
          </h1>

          <Contact_Information_Form 
            profilePicture={profilePicture} 
            name = {name}
            address={address}
            zip_code={zip_code}
            phoneNumber={phoneNumber}
            email={email}
            linkedin={linkedin}
            otherLink={otherLink}
            handleImageUpload={handleImageUpload} 
            handleInputChange={handleInputChange} 
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
            <p className="add_new_input_fields-link" onClick = {handleRemoveWorkExperience}>
              - Ta bort erfarenhet
            </p>
            <button className="AI-button">
              Få hjälp av AI <i className = "fa-solid fa-square-binary"></i>
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
            <p className="add_new_input_fields-link" onClick = {handleRemoveEducation}>
              - Ta bort erfarenhet
            </p>
            <button className = "AI-button">
              Få hjälp av AI <i className = "fa-solid fa-square-binary" id = "AI-icon"></i>
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
            <p className="add_new_input_fields-link" onClick = {handleRemoveCompetence}>
              - Ta bort erfarenhet
            </p>
            <button className = "AI-button">
              Få hjälp av AI <i className = "fa-solid fa-square-binary"></i>
            </button>
          </div>

          <div className = "section-breaker" />

        </form>

        {/* Om mig-section */}
        <form className = "about_me-section">
          
          <h1 className = "header-section-4">
            Om mig
          </h1>

          <About_Me_Form
            about_Me={about_Me}
            handleInputChange={handleInputChange}
          />

          <div className = "add_new_input_fields-section" id = "add_new_input_fields-About_Me-section">
            <button className = "AI-button">
              Få hjälp av AI <i className = "fa-solid fa-square-binary"></i>
            </button>
          </div>

        </form>

        <div className = "submit-button-container">

          <button className = "submit_CV-button">
            Färdig!
          </button>

        </div>

      </div>

      {/*------------------------------CV-mall------------------------------*/}
      <div className = "CV-section">
        
        <div className = "CV-modell-section">
          <CV_Modell 
            profilePicture={profilePicture} 
            name = {name}
            address={address}
            zip_code={zip_code}
            phoneNumber={phoneNumber}
            email={email}
            linkedin={linkedin}
            otherLink={otherLink}
            about_Me={about_Me}
            job_title={job_title}
            job_employer={job_employer}
            job_city={job_city}
            job_start_date={job_start_date}
            job_end_date={job_end_date}
            job_description={job_description}
            education_school={education_school}
            education_program={education_program}
            education_start_date={education_start_date}
            education_end_date={education_end_date}
            education_description={education_description}
            competence_name={competence_name}
            competence_level={competence_level}
            competence_description={competence_description}
          />
        </div>
      </div>

    </div>
  )
};

export default Make_CV_Page;