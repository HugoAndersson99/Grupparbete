import React from 'react';
import '../Css/Education_Form.css'

function Education_Form ({
    education_school, 
    education_program, 
    education_start_date, 
    education_end_date, 
    education_description,
    handleInputChange_For_Education_Experiences
}) {
    return (
        <div className = "Education-form">

            <div className = "input-fields-3">

                <div className = "input-group-3">
                <label htmlFor = "skola">Skola</label>
                <input 
                    type = "text" 
                    id = "skola" 
                    name = "education_school"
                    placeholder = "Skola"
                    value={education_school}
                    onChange={handleInputChange_For_Education_Experiences}
                />
                </div>

                <div className = "input-group-3">
                <label htmlFor = "utbildning">Utbildning</label>
                <input 
                    type = "text" 
                    id = "utbildning" 
                    name = "education_program"
                    placeholder = "Utbildning "
                    value={education_program}
                    onChange={handleInputChange_For_Education_Experiences}
                />
                </div>

                <div className = "input-group-3">
                <label htmlFor = "startdatum-skola">Startdatum</label>
                <input 
                    type = "text" 
                    id = "startdatum-skola" 
                    name = "education_start_date"
                    placeholder = "12-2-12"
                    value={education_start_date}
                    onChange={handleInputChange_For_Education_Experiences}
                />
                </div>

                <div className = "input-group-3">
                <label htmlFor = "sludatum-skola">Slutdatum</label>
                <input 
                    type = "text" 
                    id = "sludatum-skola" 
                    name = "education_end_date"
                    placeholder = "21-4-19"
                    value={education_end_date}
                    onChange={handleInputChange_For_Education_Experiences}
                />
                </div>

                <div className = "input-group-3" id = "beskrivning-skola-container">
                <label htmlFor = "beskrivning-skola">Beskrivning</label>
                <textarea 
                    type = "textarea" 
                    id = "beskrivning-skola" 
                    name = "education_description"
                    placeholder = "Beskrivning"
                    value={education_description}
                    onChange={handleInputChange_For_Education_Experiences}
                />
                </div>

            </div>
            
        </div>
    );
};

export default Education_Form;