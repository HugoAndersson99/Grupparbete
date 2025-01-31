export const createCv = async (cvData) => {
  try {
    const token = sessionStorage.getItem('authToken');
    const response = await fetch('http://localhost:5166/api/Cvs/CreateNewCV', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify(cvData),
    });

    if (!response.ok) {
      const errorText = await response.text();  
      console.error("Server Error:", errorText);
      return { success: false, message: errorText };
    }

    const data = await response.json();  
    return { success: true, data };
  } catch (error) {
    console.error('Error while creating CV:', error);
    return { success: false, message: 'An error occurred. Please try again.' };
  }
};



export const deleteCv = async (id) => {
  try {
    const token = sessionStorage.getItem('authToken');
    const response = await fetch(`http://localhost:5166/api/Cvs/DeleteCv/${id}`, {
      method: 'DELETE',
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });

    if (response.ok) {
      return { success: true };
    } else {
      const data = await response.json();
      return { success: false, message: data.message };
    }
  } catch (error) {
    console.error('Error while deleting CV:', error);
    return { success: false, message: 'An error occurred. Please try again.' };
  }
};

export const getUserCvs = async (userId) => {
  try {
    const token = sessionStorage.getItem('authToken');
    const response = await fetch(`http://localhost:5166/api/Cvs/GetUsersCv/${userId}`, {
      method: 'GET',
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });

    if (!response.ok) {
      const text = await response.text();  
      console.warn(`Server response: ${text}`);

      if (response.status === 500) {
        return { success: true, data: [] };  
      }

      return { success: false, message: text };
    }

    const data = await response.json();
    return { success: true, data };

  } catch (error) {
    console.error('Error while fetching user CVs:', error);
    return { success: false, message: 'An error occurred. Please try again.' };
  }
};


export const updateCv = async (id, updatedData) => {
  try {
    const token = sessionStorage.getItem("authToken");
    const response = await fetch(`http://localhost:5166/api/Cvs/UpdateCv/${id}`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify(updatedData),
    });

    if (!response.ok) {
      const text = await response.text();
      return { success: false, message: text };
    }

    return { success: true };
  } catch (error) {
    console.error("Fel vid uppdatering av CV:", error);
    return { success: false, message: "Ett fel uppstod vid uppdatering av CV." };
  }
};
