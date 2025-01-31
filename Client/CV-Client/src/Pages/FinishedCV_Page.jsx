import React from 'react';
import { useNavigate } from 'react-router-dom';
import Header from '../Components/Header';
import '../Css/FinishedCV_Page.css';
import CreateCV_Button from '../Components/CreateCV_Button';
import MittKonto_Button from '../Components/MittKonto_Button';
import CV_Modell from '../Components/CV_Modell.jsx';
import { useLocation } from 'react-router-dom';
import '../Css/CV_Modell.css'
import jsPDF from "jspdf";
import html2canvas from "html2canvas";


function FinishedCV_Page() {
  const location = useLocation();
  const state = location.state || {};
  const {
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
    rightSide_Competence_Level_Foreground

  } = state;

  const navigate = useNavigate();

  const printRef = React.useRef(null);

  const handleDownloadPDF = async () => {
    const element = printRef.current;
    if(!element){
      return;
    }

    const canvas = await html2canvas(element, {
      scale: 3
    });
    const data = canvas.toDataURL("image/png");

    const pdf = new jsPDF({
      orientation: "portrait",
      unit: "px",
      format: "a4"
    });

    const imgProperties = pdf.getImageProperties(data);
    const pdfWidth = pdf.internal.pageSize.getWidth();
    const pdfHeight = (imgProperties.height * pdfWidth) / imgProperties.width;

    pdf.addImage(data, "PNG", 0, 0, pdfWidth, pdfHeight);
    pdf.save("example.pdf")
  };
  

  return (
  <>
  
    <Header />
    <div className="finished-page-container">
      <div className="half-circle"></div>
      
      <div className="finished-page-section">
        <h1 id = "create_CV_Header">Grattis! Här är ditt nya CV!</h1>
        <div id = "create_CV_button">
          <CreateCV_Button text="Ladda ned pdf"  onClick={handleDownloadPDF}/>
        </div>
        <div id = "my_account_button">
          <MittKonto_Button text="Till mitt konto" id = "my_account_button" onClick={() => navigate("/Mitt_Konto")}/>
        </div>
      </div>
      
      <div className="finished-CV-container" id = "finished_CV" ref = {printRef}>
       {/*  {state.name ? (  */}
          <CV_Modell
            profilePicture={profilePicture}
            name={name}
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

            leftSide_Color = {leftSide_Color}
            leftSide_ProfileImage_Color={leftSide_ProfileImage_Color}
            leftSide_Name_Color={leftSide_Name_Color}
            leftSide_Headers_Color={leftSide_Headers_Color}
            leftSide_Border_Color = {leftSide_Border_Color}
            leftSide_ContactInformation_Text_Color={leftSide_ContactInformation_Text_Color}
            leftSide_ContactInformation_Logos_Color={leftSide_ContactInformation_Logos_Color}
            leftSide_Education_Header_Color={leftSide_Education_Header_Color}
            leftSide_EducationInformation_Color={leftSide_EducationInformation_Color}

            rightSide_Color = {rightSide_Color}
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
        {/* ) : (
          <p>No data passed to this page! Please fill out all the fields!</p>
        )} */}
      </div>
    </div>
  </>
  );
}

export default FinishedCV_Page;
