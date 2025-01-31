import React, { useState } from 'react';
import '../Css/Build_CV_Page.css'
import Picture_model from '../assets/Images/cat-5646889_1920.jpg'
import Work_Experience_Form from '../Components/Work_Experience_Form.jsx';
import Education_Form from '../Components/Education_Form.jsx';
import Competencies_Form from '../Components/Competencies_Form.jsx';
import About_Me_Form from '../Components/About_Me_Form.jsx';
import Contact_Information_Form from '../Components/Contact_Information_Form.jsx';
import CV_Modell from '../Components/CV_Modell.jsx';
import { useNavigate } from "react-router-dom";
import { chatWithAI } from '../Services/OpenAI_API.jsx';
import { createCv } from '../Services/CV_API';
import { useAuth } from '../Services/AuthContext';
import { jwtDecode } from "jwt-decode";

function Make_CV_Page() {

  const { authToken } = useAuth();
  const navigate = useNavigate();
  const [profilePicture, setProfilePicture] = useState(Picture_model);

  {/*--------------------Contact information-Section--------------------*/}
  const [name, setName] = useState("Katt Kattson");
  const [address, setAddress] = useState("Göterborg gatan 45");
  const [zip_code, setZip_Code] = useState("690 05");
  const [phoneNumber, setPhoneNumber] = useState("0789 567385");
  const [email, setEmail] = useState("Minhotmail.com");
  const [linkedin, setLinkedIn] = useState("www.linkedIn.se");
  const [otherLink, setOtherLink] = useState("www.minadress.se");

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

  {/*--------------------About Me-Section--------------------*/}
  const [about_Me, setAbout_Me] = useState("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse nec tortor sed metus fermentum tincidunt. Nam accumsan libero id malesuada dignissim.Mauris dictum convallis ipsum id tincidunt. Nulla facilisi. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Etiam auctor sapien sed justo convallis, vitae interdum sapien dapibus. Sed nec semper justo. Curabitur varius magna in ipsum malesuada, quis accumsan felis ornare. Integer quis ipsum ac justo pulvinar vulputate sed in risus. Vivamus eget lacus pharetra, volutpat est vel, pretium arcu.");

  {/* Function to update props when input changes for Contact Information and About Me*/}
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
      default:
        break;
    }
  };

  {/*--------------------Work-experience-Section--------------------*/}
  const [work_Experiences, setWork_Experiences] = useState([
    { 
      job_title: "Lärare", 
      job_employer: "Haga Skolan", 
      job_city: "Norrköping", 
      job_start_date: "12-03-2010", 
      job_end_date: "30-11-2021", 
      job_description: "Lorem ipsum dolor sit amet, consectetur adipiscing elit.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit.Lorem ipsum dolor sit amet, consectetur adipiscing elit." 
    }
  ]);

  const handleJobDescriptionChange = (index, newDescription) => {
    const updatedWorkExperiences = work_Experiences.map((experience, i) =>
      i === index ? { ...experience, job_description: newDescription } : experience
    );
    setWork_Experiences(updatedWorkExperiences);
  };

  const handleInputChange_For_Work_Experiences = (e, index) => {
    const { name, value } = e.target;
    const new_Work_Experiences = [...work_Experiences];
    new_Work_Experiences[index][name] = value;
    setWork_Experiences(new_Work_Experiences);
  };
  const handleAdd_Work_Experiences = () => {
    setWork_Experiences([
      ...work_Experiences,
      { job_title: '', job_employer: '', job_city: '', job_start_date: '', job_end_date: '', job_description: '' }
    ]);
  };
  const handleRemove_Work_Experiences = () => {
    if (work_Experiences.length > 0) {
      setWork_Experiences(work_Experiences.slice(0, work_Experiences.length - 1));
    }
  };

  {/*--------------------Education-Section--------------------*/}
  const [education_Experiences, setEducation_Experiences] = useState([
    { 
      education_school: "Socionom", 
      education_program: "Linköpings Universitet", 
      education_start_date: "12-03-2010", 
      education_end_date: "30-11-2021", 
      education_description: "Lorem ipsum dolor sit amet, consectetur adipiscing elit." 
    }
  ]);

  const handleEducationDescriptionChange = (index, newDescription) => {
    const updatedEducationExperiences = education_Experiences.map((experience, i) =>
      i === index ? { ...experience, education_description: newDescription } : experience
    );
    setEducation_Experiences(updatedEducationExperiences);
  };
  
  const handleInputChange_For_Education_Experiences = (e, index) => {
    const { name, value } = e.target;
    const new_education_Experiences = [...education_Experiences];
    new_education_Experiences[index][name] = value;
    setEducation_Experiences(new_education_Experiences);
  };
  const handleAdd_Education_Experiences = () => {
    setEducation_Experiences([
      ...education_Experiences,
      { education_school: '', education_program: '', education_start_date: '', education_end_date: '', education_description: ''}
    ]);
  };
  const handleRemove_Education_Experiences = () => {
    if (education_Experiences.length > 0) {
      setEducation_Experiences(education_Experiences.slice(0, education_Experiences.length - 1));
    }
  };

  {/*--------------------Competencies-Section--------------------*/}
  const [competencies_Experiences, setCompetencies_Experiences] = useState([
    { 
      competence_name: "C#", 
      competence_level: "", 
      competence_description: "Lorem ipsum dolor sit amet, consectetur adipiscing elit.", 
    }
  ]);

  const handleCompetenceDescriptionChange = (index, newDescription) => {
    const updatedCompetenciesExperiences = competencies_Experiences.map((experience, i) =>
      i === index ? { ...experience, competence_description: newDescription } : experience
    );
    setCompetencies_Experiences(updatedCompetenciesExperiences);
  };
  
  const handleInputChange_For_Competencies_Experiences = (e, index) => {
    const { name, value } = e.target;
    const new_competencies_Experiences = [...competencies_Experiences];
    new_competencies_Experiences[index][name] = value;
    setCompetencies_Experiences(new_competencies_Experiences);
  };
  const handleAdd_Competencies_Experiences = () => {
    setCompetencies_Experiences([
      ...competencies_Experiences,
      { competence_name: "", competence_level: "", competence_description: ""}
    ]);
  };
  const handleRemove_Competencies_Experiences = () => {
    if (competencies_Experiences.length > 0) {
      setCompetencies_Experiences(competencies_Experiences.slice(0, competencies_Experiences.length - 1));
    }
  };

  {/*--------------------Utility-Section--------------------*/}

  const [leftSide_Color, setLeftSide_Color] = useState("#F3A2B1");
  const [leftSide_ProfileImage_Color, setLeftSide_ProfileImage_Color] = useState("#8C292C");
  const [leftSide_Name_Color, setLeftSide_Name_Color] = useState("#FFFFFF");
  const [leftSide_Headers_Color, setLeftSide_Headers_Color] = useState("#FFFFFF");
  const [leftSide_Border_Color, setLeftSide_Border_Color] = useState("#8C292C");
  const [leftSide_ContactInformation_Text_Color, setLeftSide_ContactInformation_Text_Color] = useState("#EEF2CF");
  const [leftSide_ContactInformation_Logos_Color, setLeftSide_ContactInformation_Logos_Color] = useState("#8C292C");
  const [leftSide_Education_Header_Color, setLeftSide_Education_Header_Color] = useState("#8C292C");
  const [leftSide_EducationInformation_Color, setLeftSide_EducationInformation_Color] = useState("#EEF2CF");
  
  const [rightSide_Color, setRightSide_Color] = useState("#C1EADA");
  const [rightSide_Border_Color, setRighSide_Border_Color] = useState("#778C79");
  const [rightSide_Header_Text_Color, setRightSide_Header_Text_Color] = useState("#FFFFFF");
  const [rightSide_Header_Background_Color, setRightSide_Header_Background_Color] = useState("#778C79");
  const [rightSide_Header_Shadow_Color, setrightSide_Header_Shadow_Color] = useState("#000000");
  const [rightSide_AboutMe_Color, setrightSide_AboutMe_Color] = useState("#59574A");
  const [rightSide_Work_Title_Color, setRightSide_Work_Title_Color] = useState("#59574A");
  const [rightSide_Work_Information_Color, setRightSide_Work_Information_Color] = useState("#59574A");
  const [rightSide_Work_Description_Color, setRightSide_Work_Description_Color] = useState("#59574A");
  const [rightSide_Competence_Skill, setRightSide_Competence_Skill] = useState("#59574A");
  const [rightSide_Competence_Description, setRightSide_Competence_Description] = useState("#59574A");
  const [rightSide_Competence_Level_Background, setRightSide_Competence_Level_Background] = useState("#EEF2CF");
  const [rightSide_Competence_Level_Foreground, setRightSide_Competence_Level_Foreground] = useState("#F2CFBB");
  

  const handleSubmitCV = async () => {
    if (!authToken) {
      alert("Fel: Ingen användare inloggad.");
      return;
    }
  
    
    let userId;
    try {
      const decodedToken = jwtDecode(authToken);  
      userId = decodedToken.nameid;  
    } catch (error) {
      console.error("Fel vid avkodning av JWT:", error);
      alert("Fel vid inloggning, försök igen.");
      return;
    }
  
    if (!userId) {
      alert("Fel: Kunde inte hitta användarens ID.");
      return;
    }
  
    
    const cvData = {
      title: `CV - ${name}`,
      userId: userId,  
      pdfUrl: "",
    };
  
    
    const result = await createCv(cvData);
  
    if (result.success) {
      navigate("/Finish", {
        state: {
          profilePicture,
          name,
          address,
          zip_code,
          phoneNumber,
          email,
          linkedin,
          otherLink,
          about_Me,
          work_Experiences,
          education_Experiences,
          competencies_Experiences,
          leftSide_Color,
          leftSide_ProfileImage_Color,
          leftSide_Name_Color,
          leftSide_Headers_Color,
          leftSide_Border_Color,
          leftSide_ContactInformation_Text_Color,
          leftSide_ContactInformation_Logos_Color,
          leftSide_Education_Header_Color,
          leftSide_EducationInformation_Color,
          rightSide_Color,
          rightSide_Border_Color,
          rightSide_Header_Text_Color,
          rightSide_Header_Background_Color,
          rightSide_Header_Shadow_Color,
          rightSide_AboutMe_Color,
          rightSide_Work_Title_Color,
          rightSide_Work_Information_Color,
          rightSide_Work_Description_Color,
          rightSide_Competence_Skill,
          rightSide_Competence_Description,
          rightSide_Competence_Level_Background,
          rightSide_Competence_Level_Foreground,
          cvId: result.data.id,
        },
      });
    } else {
      alert(`Fel: ${result.message}`);
    }
  };

  const handleAIButtonClick = async (section, inputText, additionalInfo, index) => {
   
    const question = `Hjälp mig med ${section} avsnittet. Här är vad jag har hittills: ${additionalInfo}. Förbättra det: ${inputText}`;
    const response = await chatWithAI(question);
    
    if (response) {
        const improvedDescription = response.choices[0].message.content;
        switch (section) {
            case 'Work Experience':
                handleJobDescriptionChange(index, improvedDescription);
                break;
            case 'Education':
                handleEducationDescriptionChange(index, improvedDescription);
                break;
            case 'Competencies':
                handleCompetenceDescriptionChange(index, improvedDescription);
                break;
            case 'About Me':
                setAbout_Me(improvedDescription);
                break;
            default:
                break;
        }
    } else {
        console.error('Error getting AI response');
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

          {work_Experiences.map((experience, index) => (
          <div key={index}>
            <Work_Experience_Form
              job_title={experience.job_title}
              job_employer={experience.job_employer}
              job_city={experience.job_city}
              job_start_date={experience.job_start_date}
              job_end_date={experience.job_end_date}
              job_description={experience.job_description}
              handleInputChange_For_Work_Experiences={(e) => handleInputChange_For_Work_Experiences(e, index)}
            />
            <button 
              type="button" 
              className="AI-button" 
              onClick={() => handleAIButtonClick('Work Experience', experience.job_description, `${experience.job_title}, ${experience.job_employer}, ${experience.job_city}, ${experience.job_start_date} till ${experience.job_end_date}`, index)}
            >
              Få hjälp av AI <i className="fa-solid fa-square-binary"></i>
            </button>
          </div>
          ))}

          <div className="add_new_input_fields-section">
            <p className="add_new_input_fields-link" onClick = {handleAdd_Work_Experiences}>
              + Lägg till ytterligare erfarenhet
            </p>
            <p className="add_new_input_fields-link" onClick = {handleRemove_Work_Experiences}>
              - Ta bort erfarenhet
            </p>

          </div>

          <div className="section-breaker" />   

        </form>

        {/* Utbildning-section */}
        <form className = "education-experience">

          <h1 className = "header-section-3">
          Utbildning
          </h1>

          {education_Experiences.map((experience, index) => (
          <div key={index}>
            <Education_Form
              education_school={experience.education_school}
              education_program={experience.education_program}
              education_start_date={experience.education_start_date}
              education_end_date={experience.education_end_date}
              education_description={experience.education_description}
              handleInputChange_For_Education_Experiences={(e) => handleInputChange_For_Education_Experiences(e, index)}
            />
            <button 
              type="button" 
              className="AI-button" 
              onClick={() => handleAIButtonClick('Education', experience.education_description, `${experience.education_school}, ${experience.education_program}, ${experience.education_start_date} till ${experience.education_end_date}`, index)}
            >
              Få hjälp av AI <i className="fa-solid fa-square-binary"></i>
            </button>
          </div>
          ))}

          <div className = "add_new_input_fields-section">
            <p className = "add_new_input_fields-link" onClick={handleAdd_Education_Experiences}>
              + Lägg till ytterliggare erfarenhet
            </p>
            <p className="add_new_input_fields-link" onClick = {handleRemove_Education_Experiences}>
              - Ta bort erfarenhet
            </p>
          </div>

          <div className = "section-breaker" />

        </form>

        {/* Kompetens-section */}
        <form className = "competence-section">

          <h1 className = "header-section-4">
            Kompetenser
          </h1>

          {competencies_Experiences.map((experience, index) => (
          <div key={index}>
            <Competencies_Form
              competence_name={experience.competence_name}
              competence_level={experience.competence_level}
              competence_description={experience.competence_description}
              handleInputChange_For_Competencies_Experiences={(e) => handleInputChange_For_Competencies_Experiences(e, index)}
            />
            <button 
              type="button" 
              className="AI-button" 
              onClick={() => handleAIButtonClick('Competencies', experience.competence_description, `${experience.competence_name}, nivå ${experience.competence_level}`, index)}
            >
              Få hjälp av AI <i className="fa-solid fa-square-binary"></i>
            </button>
          </div>
          ))}

          <div className = "add_new_input_fields-section">
            <p className = "add_new_input_fields-link" onClick={handleAdd_Competencies_Experiences}>
              + Lägg till ytterliggare erfarenhet
            </p>
            <p className="add_new_input_fields-link" onClick = {handleRemove_Competencies_Experiences}>
              - Ta bort erfarenhet
            </p>
          </div>

          <div className="section-breaker" />

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
            <button type="button" className="AI-button" onClick={() => handleAIButtonClick('About Me', about_Me, `Min bakgrund: ${about_Me}`)}>
              Få hjälp av AI <i className="fa-solid fa-square-binary"></i>
            </button>
          </div>

        </form>

        <div className = "submit-button-container">

          <button className = "submit_CV-button" onClick={handleSubmitCV}>
            Färdig!
          </button>

        </div>

      </div>

      {/*------------------------------CV-mall------------------------------*/}
      <div className = "CV-section">

        <div className = "color_picker-section">

          <div className="color-picker">
            <input
              type="color"
              id="leftSide-color"
              value={leftSide_Color}
              onChange={(e) => setLeftSide_Color(e.target.value)}
            />
          </div>

          <div className="color-picker">
            <input
              type="color"
              id="leftSide-Border-color"
              value={leftSide_Border_Color}
              onChange={(e) => setLeftSide_Border_Color(e.target.value)}
            />
          </div>

          <div className="color-picker">
            <input
              type="color"
              id="leftSide-ProfileImage"
              value={leftSide_ProfileImage_Color}
              onChange={(e) => setLeftSide_ProfileImage_Color(e.target.value)}
            />
          </div>

          <div className="color-picker">
            <input
              type="color"
              id="leftSide-Name"
              value={leftSide_Name_Color}
              onChange={(e) => setLeftSide_Name_Color(e.target.value)}
            />
          </div>

          <div className="color-picker">
            <input
              type="color"
              id="leftSide-Headers"
              value={leftSide_Headers_Color}
              onChange={(e) => setLeftSide_Headers_Color(e.target.value)}
            />
          </div>

          <div className="color-picker">
            <input
              type="color"
              id="leftSide-ContactInformation-Text"
              value={leftSide_ContactInformation_Text_Color}
              onChange={(e) => setLeftSide_ContactInformation_Text_Color(e.target.value)}
            />
          </div>

          <div className="color-picker">
            <input
              type="color"
              id="leftSide-ContactInformation-Logos"
              value={leftSide_ContactInformation_Logos_Color}
              onChange={(e) => setLeftSide_ContactInformation_Logos_Color(e.target.value)}
            />
          </div>

          <div className="color-picker">
            <input
              type="color"
              id="leftSide-Education-Header"
              value={leftSide_Education_Header_Color}
              onChange={(e) => setLeftSide_Education_Header_Color(e.target.value)}
            />
          </div>

          <div className="color-picker">
            <input
              type="color"
              id="leftSide-Education-Information"
              value={leftSide_EducationInformation_Color}
              onChange={(e) => setLeftSide_EducationInformation_Color(e.target.value)}
            />
          </div>

          <div className="color-picker">
            <input
              type="color"
              id="rightSide-color"
              value={rightSide_Color}
              onChange={(e) => setRightSide_Color(e.target.value)}
            />
          </div>

          <div className="color-picker">
            <input
              type="color"
              id="rightSide-Border-color"
              value={rightSide_Border_Color}
              onChange={(e) => setRighSide_Border_Color(e.target.value)}
            />
          </div>

          <div className="color-picker">
            <input
              type="color"
              id="rightSide-Header-Text-color"
              value={rightSide_Header_Text_Color}
              onChange={(e) => setRightSide_Header_Text_Color(e.target.value)}
            />
          </div>

          <div className="color-picker">
            <input
              type="color"
              id="rightSide-Header-Background-color"
              value={rightSide_Header_Background_Color}
              onChange={(e) => setRightSide_Header_Background_Color(e.target.value)}
            />
          </div>

          <div className="color-picker">
            <input
              type="color"
              id="rightSide-Header-Shadow-color"
              value={rightSide_Header_Shadow_Color}
              onChange={(e) => setrightSide_Header_Shadow_Color(e.target.value)}
            />
          </div>

          <div className="color-picker">
            <input
              type="color"
              id="rightSide-AboutMe-color"
              value={rightSide_AboutMe_Color}
              onChange={(e) => setrightSide_AboutMe_Color(e.target.value)}
            />
          </div>

          <div className="color-picker">
            <input
              type="color"
              id="rightSide-Work-Title-color"
              value={rightSide_Work_Title_Color}
              onChange={(e) => setRightSide_Work_Title_Color(e.target.value)}
            />
          </div>

          <div className="color-picker">
            <input
              type="color"
              id="rightSide-Work-Information-color"
              value={rightSide_Work_Information_Color}
              onChange={(e) => setRightSide_Work_Information_Color(e.target.value)}
            />
          </div>

          <div className="color-picker">
            <input
              type="color"
              id="rightSide-Work-Description-color"
              value={rightSide_Work_Description_Color}
              onChange={(e) => setRightSide_Work_Description_Color(e.target.value)}
            />
          </div>

          <div className="color-picker">
            <input
              type="color"
              id="rightSide-Competence-Skill-color"
              value={rightSide_Competence_Skill}
              onChange={(e) => setRightSide_Competence_Skill(e.target.value)}
            />
          </div>

          <div className="color-picker">
            <input
              type="color"
              id="rightSide-Competence-Descriptioncolor"
              value={rightSide_Competence_Description}
              onChange={(e) => setRightSide_Competence_Description(e.target.value)}
            />
          </div>


          <div className="color-picker">
            <input
              type="color"
              id="rightSide-Competence-Level-Background-color"
              value={rightSide_Competence_Level_Background}
              onChange={(e) => setRightSide_Competence_Level_Background(e.target.value)}
            />
          </div>

          <div className="color-picker">
            <input
              type="color"
              id="rightSide-Competence-Level-Foreground-color"
              value={rightSide_Competence_Level_Foreground}
              onChange={(e) => setRightSide_Competence_Level_Foreground(e.target.value)}
            />
          </div>

        </div>

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
            work_Experiences={work_Experiences}
            education_Experiences={education_Experiences}
            competencies_Experiences={competencies_Experiences}
            
            leftSide_Color={leftSide_Color}
            leftSide_ProfileImage_Color={leftSide_ProfileImage_Color}
            leftSide_Name_Color={leftSide_Name_Color}
            leftSide_Headers_Color={leftSide_Headers_Color}
            leftSide_Border_Color = {leftSide_Border_Color}
            leftSide_ContactInformation_Text_Color={leftSide_ContactInformation_Text_Color}
            leftSide_ContactInformation_Logos_Color={leftSide_ContactInformation_Logos_Color}
            leftSide_Education_Header_Color={leftSide_Education_Header_Color}
            leftSide_EducationInformation_Color={leftSide_EducationInformation_Color}

            rightSide_Color={rightSide_Color}
            rightSide_Border_Color = {rightSide_Border_Color}
            rightSide_Header_Text_Color={rightSide_Header_Text_Color}
            rightSide_Header_Background_Color={rightSide_Header_Background_Color}
            rightSide_Header_Shadow_Color={rightSide_Header_Shadow_Color}
            rightSide_AboutMe_Color={rightSide_AboutMe_Color}
            rightSide_Work_Title_Color={rightSide_Work_Title_Color}
            rightSide_Work_Information_Color={rightSide_Work_Information_Color}
            rightSide_Work_Description_Color={rightSide_Work_Description_Color}
            rightSide_Competence_Skill={rightSide_Competence_Skill}
            rightSide_Competence_Description={rightSide_Competence_Description}
            rightSide_Competence_Level_Background={rightSide_Competence_Level_Background}
            rightSide_Competence_Level_Foreground={rightSide_Competence_Level_Foreground}
          />
        </div>
      </div>

    </div>
  )
};

export default Make_CV_Page;