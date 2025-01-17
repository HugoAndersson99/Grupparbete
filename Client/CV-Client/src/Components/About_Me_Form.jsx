import React from 'react';
import '../Css/About_Me_Form.css'

function About_Me_Form () {
    return (
        <form className = "about_me_form">

            <div className = "input-fields-5">

                <div className = "input-group-5" id = "beskrivning-om_mig-container">
                    <label for = "beskrivning-om_mig">Berätta kort om dig!</label>
                    <textarea type = "textarea" id = "beskrivning-om_mig" placeholder = "Låt höra!"></textarea>
                </div>
                
            </div>

        </form>
    );
};

export default About_Me_Form;