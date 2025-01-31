import React from 'react';
import '../Css/Competencies_Form.css'

function Competencies_Form ({ 
  competence_name, 
  competence_level, 
  handleInputChange_For_Competencies_Experiences
}) {
  
  return (

        <form className = "competencies-form">

          <div className = "input-fields-4">
            
            <div className = "input-group-4">
              <label htmlFor = "namn-kompetens">Namn</label>
              <input 
                type = "text" 
                id = "namn-kompetens"
                name = "competence_name"
                placeholder = "Namn"
                maxLength="10"
                value={competence_name}
                onChange={handleInputChange_For_Competencies_Experiences}
              />
            </div>

            <div className = "input-group-4">
              <label htmlFor = "utbildning">Nivå</label>
              <div className = "level-bar" id = "utbildning">
                <button type = "button" className = "level-button" id="number-1" name="competence_level" value={1} onClick={handleInputChange_For_Competencies_Experiences}>1</button>
                <button type = "button" className = "level-button" id="number-2" name="competence_level" value={2} onClick={handleInputChange_For_Competencies_Experiences}>2</button>
                <button type = "button" className = "level-button" id="number-3" name="competence_level" value={3} onClick={handleInputChange_For_Competencies_Experiences}>3</button>
                <button type = "button" className = "level-button" id="number-4" name="competence_level" value={4} onClick={handleInputChange_For_Competencies_Experiences}>4</button>
                <button type = "button" className = "level-button" id="number-5" name="competence_level" value={5} onClick={handleInputChange_For_Competencies_Experiences}>5</button>
              </div>
            </div>

          </div>

        </form>
    );
};

export default Competencies_Form;