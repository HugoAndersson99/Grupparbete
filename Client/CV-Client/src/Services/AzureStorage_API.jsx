export const uploadCvToAzure = async (file, userId) => {
  try {
    const formData = new FormData();
    formData.append("file", file);
    formData.append("userId", userId);

    const response = await fetch("https://cvapplikation-gvefeagjdzb2bqf2.swedencentral-01.azurewebsites.net/api/AzureStorage/upload-cv", {
      method: "POST",
      body: formData,
    });

    if (!response.ok) {
      throw new Error("Misslyckades att ladda upp CV.");
    }

    const data = await response.json();
    return { success: true, pdfUrl: data.Url }; 
  } catch (error) {
    console.error("Fel vid uppladdning av CV:", error);
    return { success: false, message: "Ett fel uppstod vid uppladdning av CV." };
  }
};
