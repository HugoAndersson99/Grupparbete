import React from 'react';
import '../Css/CV_Modell.css'

function CV_Modell({profilePicture, name, address, zip_code, phoneNumber, email, linkedin, otherLink, 
                    about_Me, 
                    job_title, job_employer, job_city, job_start_date, job_end_date, job_description, 
                    education_school, education_program, education_start_date, education_end_date, education_description, 
                    competence_name, competence_level, competence_description}) {

    return (
        <div className = "CV-container">


            <div className = "left-side">


                <div className = "profile-image">
                    <img className = "profile-image-img" src = {profilePicture}></img>
                </div>

                <div className = "name-CV">
                    <h2>
                        {name}
                    </h2>
                </div>

                <div className = "border_left-CV"></div>

                <h3 className = "left_side-CV-header">
                    Contact Info
                </h3>

                <div className = "contact-information-CV">
                    {address && (
                        <h3>
                            <i className="fa-solid fa-location-dot"></i><p>{address}</p>
                        </h3>
                    )}
                    {zip_code && (
                        <h3>
                        <i className = "fa-solid fa-location-dot"></i><p>{zip_code}</p>
                        </h3>
                    )}
                    {phoneNumber && (
                        <h3>
                        <i className = "fa-solid fa-phone"></i><p>{phoneNumber}</p>
                        </h3>
                    )}
                    {email && (
                        <h3>
                        <i className = "fa-solid fa-envelope"></i><p>{email}</p>
                        </h3>
                    )}
                    {linkedin && (
                        <h3>
                        <i className = "fa-brands fa-linkedin"></i><p>{linkedin}</p>
                        </h3>
                    )}
                    {otherLink && (
                        <h3>
                        <i className = "fa-solid fa-earth-americas"></i><p>{otherLink}</p>
                        </h3>
                    )}
                </div>

                <h3 className = "left_side-CV-header">
                    Utbildning
                </h3>
                
                <div className = "education-CV">
                    <h2 className = "education_program-CV">{education_program}</h2>
                    <p className = "education_school-CV">{education_school}</p>
                    <div className = "education_dates-CV">
                    <p>{education_start_date}<span className="dash">-</span></p>
                        <p>{education_end_date}</p>
                    </div>
                    <p>{education_description}</p>
                </div>
    
            </div>

            <div className = "right-side">
                
                <h3 className = "right_side-CV-header">Profile</h3>

                <div className = "about_Me-CV">
                    <p>{about_Me}</p>
                </div>

                <h3 className = "right_side-CV-header">Work Experince</h3>

                <div className = "work-experience-CV">
                    <h2 className = "job_title-CV">
                        <p>{job_title}</p>
                    </h2>
                    <h4 className = "job_statistics-CV">
                        <p>{job_employer},</p>
                        <p>{job_start_date} -</p>
                        <p>{job_end_date},</p>
                        <p>{job_city}</p>
                    </h4>
                    <p classname = "job_description-CV">
                        <p>{job_description}</p>
                    </p>
                    <div className = "border_right-CV"></div>
                </div>

                <h3 className = "right_side-CV-header">Competencies</h3>

                <div className = "competencies-CV">

                    <div className = "name_and_level_of_skill-CV">
                        <p id = "competence_name">{competence_name}</p>
                        {competence_level === "1" && (
                            <div className = "level_bar">
                                <div className = "level_bar_20_percent" ></div>
                            </div>
                        )}
                        {competence_level === "2" && (
                            <div className = "level_bar">
                                <div className = "level_bar_40_percent" ></div>
                            </div>
                        )}
                        {competence_level === "3"&& (
                            <div className = "level_bar">
                                <div className = "level_bar_60_percent" ></div>
                            </div>
                        )}
                        {competence_level === "4"&& (
                            <div className = "level_bar">
                                <div className = "level_bar_80_percent" ></div>
                            </div>
                        )}
                        {competence_level === "5"&& (
                            <div className = "level_bar">
                                <div className = "level_bar_100_percent" ></div>
                            </div>
                        )}
                    </div>  
                    <p>{competence_description}</p>
                </div>

            </div>

        </div>
    );
};

export default CV_Modell;