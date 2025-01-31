export const loginUser = async (loginData) => {
  try {
    const response = await fetch('https://cvapplikation-gvefeagjdzb2bqf2.swedencentral-01.azurewebsites.net/api/Users/Login', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(loginData),
    });

    const data = await response.json();

    if (response.ok) {
      
      return { success: true, token: data.token };
    } else {
      
      return { success: false, message: data.message };
    }
  } catch (error) {
    console.error('Fel vid inloggning:', error);
    return { success: false, message: 'Något gick fel. Försök igen.' };
  }
};


export const registerUser = async (newUser) => {
  try {
    const response = await fetch('https://cvapplikation-gvefeagjdzb2bqf2.swedencentral-01.azurewebsites.net/api/Users/AddNewUser', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(newUser),
    });

    const data = await response.json();

    if (response.ok) {
      return { success: true, data: data.data }; 
    } else {
      return { success: false, message: data.message }; 
    }
  } catch (error) {
    console.error('Fel vid registrering:', error);
    return { success: false, message: 'Något gick fel. Försök igen.' };
  }
};
