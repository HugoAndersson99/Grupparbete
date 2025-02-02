export const uploadCvToAzure = async (file, userId) => {
  try {
    if (!userId) {
      throw new Error("UserId saknas!");
    }

    const formData = new FormData();
    formData.append("file", file);

    console.log("Uploading file with size:", file.size, "and userId:", userId);

    const response = await fetch(
      `https://cvapplikation-gvefeagjdzb2bqf2.swedencentral-01.azurewebsites.net/api/AzureStorage/upload-cv?userId=${userId}`,
      {
        method: "POST",
        body: formData,
      }
    );

    if (!response.ok) {
      const errorText = await response.text();
      console.error("Server returned error:", errorText);
      throw new Error("Misslyckades att ladda upp CV.");
    }

    const data = await response.json();
    console.log("Upload success, received data:", data);

    
    return { success: true, pdfUrl: data.url };  
  } catch (error) {
    console.error("Fel vid uppladdning av CV:", error);
    return { success: false, message: "Ett fel uppstod vid uppladdning av CV." };
  }
};
