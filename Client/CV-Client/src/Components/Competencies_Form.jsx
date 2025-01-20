import React from 'react';
import '../Css/Competencies_Form.css'

function Competencies_Form ({handleInputChange, competence_name, competence_level, competence_description}) {
    
  return (

        <div className = "competencies-form">

          <div className = "input-fields-4">
            
            <div className = "input-group-4">
              <label htmlFor = "namn-kompetens">Namn</label>
              <input 
                type = "text" 
                id = "namn-kompetens"
                name = "competence_name"
                placeholder = "Namn"
                value={competence_name}
                onChange={handleInputChange}
              />
            </div>

            <div className = "input-group-4">
              <label htmlFor = "utbildning">Nivå</label>
              <div className = "level-bar" id = "utbildning">
                <button type = "button" className = "level-button" id="number-1" name="competence_level" value={1} onClick={handleInputChange}>1</button>
                <button type = "button" className = "level-button" id="number-2" name="competence_level" value={2} onClick={handleInputChange}>2</button>
                <button type = "button" className = "level-button" id="number-3" name="competence_level" value={3} onClick={handleInputChange}>3</button>
                <button type = "button" className = "level-button" id="number-4" name="competence_level" value={4} onClick={handleInputChange}>4</button>
                <button type = "button" className = "level-button" id="number-5" name="competence_level" value={5} onClick={handleInputChange}>5</button>
              </div>
            </div>

            <div className = "input-group-4" id = "beskrivning-kompetens-container">
              <label htmlFor = "beskrivning-kompetens">Beskrivning</label>
              <textarea 
              type = "textarea" 
              id = "beskrivning-kompetens" 
              name = "competence_description"
              placeholder = "Beskrivning"
              value={competence_description}
              onChange={handleInputChange}
              />
            </div>

          </div>

        </div>
    );
};

export default Competencies_Form;