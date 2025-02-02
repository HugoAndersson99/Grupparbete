import React from 'react';
import { useNavigate } from 'react-router-dom';
import Header from '../Components/Header';
import '../Css/FinishedCV_Page.css';
import CreateCV_Button from '../Components/CreateCV_Button';
import MittKonto_Button from '../Components/MittKonto_Button';
import CV_Modell from '../Components/CV_Modell.jsx';
import { useLocation } from 'react-router-dom';
import '../Css/CV_Modell.css';
import jsPDF from "jspdf";
import html2canvas from "html2canvas";
import { updateCv, getUserCvs } from '../Services/CV_API';
import { uploadCvToAzure } from '../Services/AzureStorage_API';
import { useAuth } from '../Services/AuthContext';

const FinishedCV_Page = () => {
  const navigate = useNavigate();
  const { authToken } = useAuth();

  const userId = authToken 
    ? JSON.parse(atob(authToken.split('.')[1])).nameid 
    : null;

  const state = useLocation().state || {};
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
    rightSide_Competence_Level_Background,
    rightSide_Competence_Level_Foreground,
  } = state;

  const printRef = React.useRef(null);
  const [message, setMessage] = React.useState("");

  const handleCreateAndUploadCV = async () => {
    if (!userId) {
      return;
    }

    const element = printRef.current;
    if (!element) {
      return;
    }

    try {
      const canvas = await html2canvas(element, { scale: 3 });
      const dataUrl = canvas.toDataURL("image/png");

      const pdf = new jsPDF({
        orientation: "portrait",
        unit: "px",
        format: "a4"
      });

      const imgProperties = pdf.getImageProperties(dataUrl);
      const pdfWidth = pdf.internal.pageSize.getWidth();
      const pdfHeight = (imgProperties.height * pdfWidth) / imgProperties.width;

      pdf.addImage(dataUrl, "PNG", 0, 0, pdfWidth, pdfHeight);
      const pdfBlob = pdf.output("blob");
      const file = new File([pdfBlob], "cv.pdf", { type: "application/pdf" });

      const uploadResult = await uploadCvToAzure(file, userId);

      if (!uploadResult.success) {
        return;
      }

      let pdfUrl = uploadResult.pdfUrl; 
      if (!pdfUrl) {
        return;
      }

      const userCvs = await getUserCvs(userId);
      if (!userCvs.success || !userCvs.data || userCvs.data.length === 0) {
        return;
      }

      const latestCv = userCvs.data[userCvs.data.length - 1]; 
      const cvId = latestCv.id;

      const updatedTitle = name ? `CV - ${name}` : "Mitt CV";

      const updatedData = {
        id: cvId,
        title: updatedTitle,
        userId: userId,
        pdfUrl: pdfUrl,
      };

      const updateResult = await updateCv(cvId, updatedData);
      if (!updateResult.success) {
        return;
      }

      setMessage("CV har sparats");
    } catch (error) {
    }
  };

  const handleDownloadPDF = async () => {
    const element = printRef.current;
    if (!element) return;

    const canvas = await html2canvas(element, { scale: 3 });
    const dataUrl = canvas.toDataURL("image/png");

    const pdf = new jsPDF({
      orientation: "portrait",
      unit: "px",
      format: "a4"
    });

    const imgProperties = pdf.getImageProperties(dataUrl);
    const pdfWidth = pdf.internal.pageSize.getWidth();
    const pdfHeight = (imgProperties.height * pdfWidth) / imgProperties.width;

    pdf.addImage(dataUrl, "PNG", 0, 0, pdfWidth, pdfHeight);
    pdf.save("cv.pdf");
  };

  return (
    <>
      <Header />
      <div className="finished-page-container">
        <div className="half-circle"></div>
        <div className="finished-page-section">
          <h1 id="create_CV_Header">Grattis! Här är ditt nya CV!</h1>
          <div id="create_CV_button">
            <CreateCV_Button text="Spara CV" onClick={handleCreateAndUploadCV} />
          </div>
          {message && <p>{message}</p>}
          <div id="download_pdf_button">
            <CreateCV_Button text="Ladda ned pdf" onClick={handleDownloadPDF} />
          </div>
          <div id="my_account_button">
            <MittKonto_Button text="Till mitt konto" onClick={() => navigate("/Mitt_Konto")} />
          </div>
        </div>
        <div className="finished-CV-container" id="finished_CV" ref={printRef}>
          <CV_Modell {...state} />
        </div>
        <div className="half-circle-2"></div>
      </div>
    </>
  );
}

export default FinishedCV_Page;
