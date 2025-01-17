import React from 'react';
import '../Css/Education_Form.css'

function Education_Form () {
    return (
        <form className = "Education-form">

            <div className = "input-fields-3">

                <div className = "input-group-3">
                <label for = "skola">Skola</label>
                <input type = "text" id = "skola" placeholder = "Skola"></input>
                </div>

                <div className = "input-group-3">
                <label for = "utbildning">Utbildning</label>
                <input type = "text" id = "utbildning" placeholder = "Utbildning "></input>
                </div>

                <div className = "input-group-3">
                <label for = "startdatum-skola">Startdatum</label>
                <input type = "text" id = "startdatum-skola" placeholder = "12-2-12"></input>
                </div>

                <div className = "input-group-3">
                <label for = "sludatum-skola">Slutdatum</label>
                <input type = "text" id = "sludatum-skola" placeholder = "21-4-19"></input>
                </div>

                <div className = "input-group-3" id = "beskrivning-skola-container">
                <label for = "beskrivning-skola">Beskrivning</label>
                <textarea type = "textarea" id = "beskrivning-skola" placeholder = "Beskrivning"></textarea>
                </div>

            </div>
            
        </form>
    );
};

export default Education_Form;