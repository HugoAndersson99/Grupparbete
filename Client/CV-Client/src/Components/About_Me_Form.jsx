import React from 'react';
import '../Css/About_Me_Form.css'

function About_Me_Form ({about_Me, handleInputChange}) {
    return (
        <div className = "about_me_form">

            <div className = "input-fields-5">

                <div className = "input-group-5" id = "beskrivning-om_mig-container">
                    <label htmlFor = "beskrivning-om_mig">Berätta kort om dig!</label>
                    <textarea 
                        type = "textarea" 
                        id = "beskrivning-om_mig"
                        name = "about_Me"
                        placeholder = "Låt höra!"
                        value={about_Me}
                        onChange={handleInputChange}
                    />
                </div>
                
            </div>

        </div>
    );
};

export default About_Me_Form;