export const chatWithAI = async (question) => {
  try {
      const url = new URL('https://cvapplikation-gvefeagjdzb2bqf2.swedencentral-01.azurewebsites.net/api/OpenAi/ChatToChatGPT');
      url.searchParams.append('questionForChat', question);

      const response = await fetch(url, {
          method: 'POST',
          headers: {
              'Content-Type': 'application/json',
          },
      });

      if (!response.ok) {
          const errorText = await response.text(); 
          throw new Error(`Failed to connect to OpenAI API: ${response.status} - ${errorText}`);
      }

      const data = await response.json();
      console.log('Received data from API:', data); 
      return data;
  } catch (error) {
      console.error('Error while calling OpenAI API:', error);
      return null;
  }
};
