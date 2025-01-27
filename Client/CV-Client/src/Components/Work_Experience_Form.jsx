import React from 'react';
import '../Css/Work_Experience_Form.css'


function Work_Experience_Form ({
    job_title, 
    job_employer, 
    job_city, 
    job_start_date, 
    job_end_date, 
    job_description, 
    handleInputChange_For_Work_Experiences
}) {
    
    return (

        <div className="work-experience-form">

            <div className="input-fields-2">

                <div className="input-group-2">
                    <label htmlFor="jobbtitel">Jobbtitel</label>
                    <input 
                        type="text" 
                        id="jobbtitel" 
                        name = "job_title"
                        placeholder="Jobbtitel" 
                        value={job_title}
                        onChange={handleInputChange_For_Work_Experiences}
                    />
                </div>

                <div className="input-group-2">
                    <label htmlFor="arbetsgivare">Arbetsgivare</label>
                    <input 
                    type="text" 
                    id="arbetsgivare" 
                    name = "job_employer"
                    placeholder="Arbetsgivare" 
                    value={job_employer}
                    onChange={handleInputChange_For_Work_Experiences}
                    />
                </div>

                <div className="input-group-2" id="stad-container">
                    <label htmlFor="stad">Stad</label>
                    <input 
                    type="text" 
                    id="stad" 
                    name = "job_city"
                    placeholder="Postnummer"
                    value={job_city}
                    onChange={handleInputChange_For_Work_Experiences} 
                    />
                </div>

                <div className="break"></div>

                <div className="input-group-2">
                    <label htmlFor="startdatum-arbete">Startdatum</label>
                    <input 
                    type="text" 
                    id="startdatum-arbete" 
                    name ="job_start_date"
                    placeholder="21-4-19" 
                    value={job_start_date}
                    onChange={handleInputChange_For_Work_Experiences}
                    />
                </div>

                <div className="input-group-2">
                    <label htmlFor="slutdatum-arbete">Slutdatum</label>
                    <input 
                    type="text" 
                    id="slutdatum-arbete"
                    name ="job_end_date"
                    placeholder="13-3-15" 
                    value={job_end_date}
                    onChange={handleInputChange_For_Work_Experiences}
                    />
                </div>

                <div className="input-group-2" id="beskrivning-arbete-container">
                    <label htmlFor="beskrivning">Beskrivning</label>
                    <textarea 
                    id="beskrivning" 
                    name ="job_description"
                    placeholder="Beskrivning"
                    value={job_description}
                    onChange={handleInputChange_For_Work_Experiences}
                    />
                </div>
                
            </div> 
             
        </div>
    );
};

export default Work_Experience_Form;
