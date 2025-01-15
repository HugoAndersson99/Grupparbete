import React from 'react';
import '../Css/Competencies_Form.css'

const levelButtons = document.querySelectorAll('.level-button');

levelButtons.forEach(button => {
    button.addEventListener('click', () => 
    {
      levelButtons.forEach(btn => btn.classList.remove('active'));
      button.classList.add('active');
    });
});

function Competencies_Form () {
    return (

        <form className = "competencies-form">

          <div className = "input-fields-4">
            
            <div className = "input-group-4">
              <label for = "namn-kompetens">Namn</label>
              <input type = "text" id = "namn-kompetens" placeholder = "Namn"></input>
            </div>

            <div className = "input-group-4">
              <label for = "utbildning">Nivå</label>
              <div class = "level-bar" id = "utbildning">
                <button class = "level-button" id="number-1">1</button>
                <button class = "level-button" id="number-2">2</button>
                <button class = "level-button" id="number-3">3</button>
                <button class = "level-button" id="number-4">4</button>
                <button class = "level-button" id="number-5">5</button>
              </div>
            </div>

            <div className = "input-group-4" id = "beskrivning-kompetens-container">
              <label for = "beskrivning-kompetens">Beskrivning</label>
              <textarea type = "textarea" id = "beskrivning-kompetens" placeholder = "Beskrivning"></textarea>
            </div>

          </div>

        </form>
    );
};

export default Competencies_Form;