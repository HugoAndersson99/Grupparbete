import React from 'react';
import '../Css/Work_Experience_Form.css'

function Work_Experience_Form () {
    return (

        <form className="work-experience-form">

            <div className="input-fields-2">

                <div className="input-group-2">
                    <label htmlFor="jobbtitel">Jobbtitel</label>
                    <input type="text" id="jobbtitel" placeholder="Jobbtitel" />
                </div>

                <div className="input-group-2">
                    <label htmlFor="arbetsgivare">Arbetsgivare</label>
                    <input type="text" id="arbetsgivare" placeholder="Arbetsgivare" />
                </div>

                <div className="input-group-2" id="stad-container">
                    <label htmlFor="stad">Stad</label>
                    <input type="text" id="stad" placeholder="Postnummer" />
                </div>

                <div className="break"></div>

                <div className="input-group-2">
                    <label htmlFor="startdatum-arbete">Startdatum</label>
                    <input type="text" id="startdatum-arbete" placeholder="21-4-19" />
                </div>

                <div className="input-group-2">
                    <label htmlFor="slutdatum-arbete">Slutdatum</label>
                    <input type="text" id="slutdatum-arbete" placeholder="13-3-15" />
                </div>

                <div className="input-group-2" id="beskrivning-arbete-container">
                    <label htmlFor="beskrivning">Beskrivning</label>
                    <textarea id="beskrivning" placeholder="Beskrivning"></textarea>
                </div>
                
            </div> 
             
        </form>
    );
};

export default Work_Experience_Form;
