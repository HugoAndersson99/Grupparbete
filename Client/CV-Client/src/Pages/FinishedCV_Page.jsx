import React, { useState, useRef } from 'react';
import { useNavigate, useLocation } from 'react-router-dom';
import Header from '../Components/Header';
import '../Css/FinishedCV_Page.css';
import CreateCV_Button from '../Components/CreateCV_Button';
import MittKonto_Button from '../Components/MittKonto_Button';
import CV_Modell from '../Components/CV_Modell.jsx';
import jsPDF from "jspdf";
import html2canvas from "html2canvas";
import { createCv } from '../Services/CV_API.jsx';
import { uploadCvToAzure } from '../Services/AzureStorage_API.jsx';
import { useAuth } from '../Services/AuthContext';
import { jwtDecode } from "jwt-decode"; 

function FinishedCV_Page() {
  const location = useLocation();
  const state = location.state || {};
  const navigate = useNavigate();
  const printRef = useRef(null);
  const { authToken } = useAuth();

  const [pdfUrl, setPdfUrl] = useState("");

  
  const extractUserId = (token) => {
    if (!token) return null;
    try {
      const decodedToken = jwtDecode(token);
      return decodedToken.nameid || decodedToken.sub || decodedToken.userId || null; 
    } catch (error) {
      console.error(" Fel vid avkodning av JWT:", error);
      return null;
    }
  };

  const userId = extractUserId(authToken);

  
  console.log("FinishedCV_Page - authToken:", authToken);
  console.log("FinishedCV_Page - userId:", userId);

  const handleDownloadAndUploadPDF = async () => {
    if (!userId) {
      alert("Fel: Användar-ID saknas.");
      console.error("Fel: userId är null eller undefined");
      return;
    }

    const element = printRef.current;
    if (!element) {
      alert("Fel: CV-mallen hittades inte.");
      return;
    }

    try {
      console.log(" Genererar PDF...");
      const canvas = await html2canvas(element, { scale: 3 });
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

      const pdfBlob = pdf.output("blob");
      const pdfFile = new File([pdfBlob], "Mitt_CV.pdf", { type: "application/pdf" });

      
      console.log(" Laddar upp PDF till Azure...");
      const uploadResult = await uploadCvToAzure(pdfFile, userId);

      if (!uploadResult.success) {
        alert("Fel vid uppladdning: " + uploadResult.message);
        return;
      }

      console.log(" PDF uppladdad! URL:", uploadResult.pdfUrl);
      setPdfUrl(uploadResult.pdfUrl);

      
      const cvData = {
        title: `CV - ${state.name}`,
        pdfUrl: uploadResult.pdfUrl,
      };

      console.log(" Skapar CV i backend...");
      const createResult = await createCv(cvData, authToken);

      if (!createResult.success) {
        alert("Fel vid skapande av CV: " + createResult.message);
        return;
      }

      console.log(" CV skapat i backend!");
      alert("Ditt CV har skapats och PDF:n har laddats upp!");

      
      navigate("/MittKonto");

      
      pdf.save("Mitt_CV.pdf");
      
    } catch (error) {
      console.error(" Fel under hantering av PDF:", error);
      alert(" Ett fel uppstod vid generering eller uppladdning av PDF.");
    }
  };

  return (
    <>
      <Header />
      <div className="finished-page-container">
        <div className="half-circle"></div>
        
        <div className="finished-page-section">
          <h1 id="create_CV_Header">Grattis! Här är ditt nya CV!</h1>
          <div id="create_CV_button">
            <CreateCV_Button text="Ladda ned & spara CV" onClick={handleDownloadAndUploadPDF} />
          </div>
          <div id="my_account_button">
            <MittKonto_Button text="Till mitt konto" onClick={() => navigate("/MittKonto")} />
          </div>
        </div>
        
        <div className="finished-CV-container" id="finished_CV" ref={printRef}>
          {state.name ? (
            <CV_Modell {...state} />
          ) : (
            <p>No data passed to this page! Something has gone wrong!</p>
          )}
        </div>
      </div>
    </>
  );
}

export default FinishedCV_Page;
