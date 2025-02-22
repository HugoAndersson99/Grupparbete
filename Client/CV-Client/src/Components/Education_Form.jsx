import React from 'react';
import '../Css/Education_Form.css'

function Education_Form ({
    education_school, 
    education_program, 
    education_start_date, 
    education_end_date, 
    handleInputChange_For_Education_Experiences
}) {
    return (
        <form className = "Education-form">

            <div className = "input-fields-3">

            <div className = "input-group-3">

                <label htmlFor = "utbildning">Utbildning</label>
                <input 
                    type = "text" 
                    id = "utbildning" 
                    name = "education_program"
                    placeholder = "Utbildning"
                    maxLength="30"
                    value={education_program}
                    onChange={handleInputChange_For_Education_Experiences}
                />
                </div>

                <div className = "input-group-3">
                <label htmlFor = "skola">Skola</label>
                <input 
                    type = "text" 
                    id = "skola" 
                    name = "education_school"
                    placeholder = "Skola"
                    maxLength="25"
                    value={education_school}
                    onChange={handleInputChange_For_Education_Experiences}
                />
                </div>



                <div className = "input-group-3">
                <label htmlFor = "startdatum-skola">Startdatum</label>
                <input 
                    type = "date" 
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
                    type = "date" 
                    id = "sludatum-skola" 
                    name = "education_end_date"
                    placeholder = "21-4-19"
                    value={education_end_date}
                    onChange={handleInputChange_For_Education_Experiences}
                />
                </div>

            </div>
            
        </form>
    );
};

export default Education_Form;