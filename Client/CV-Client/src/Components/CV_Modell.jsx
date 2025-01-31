import React, { useEffect, useRef, useState } from "react";
import '../Css/CV_Modell.css'


function CV_Modell({
    profilePicture, 
    name, 
    address, 
    zip_code, 
    phoneNumber, 
    email, 
    linkedin, 
    otherLink, 
    about_Me,
    work_Experiences,
    education_Experiences,
    competencies_Experiences,

    leftSide_Color,
    leftSide_ProfileImage_Color,
    leftSide_Name_Color,
    leftSide_Headers_Color,
    leftSide_Border_Color,
    leftSide_ContactInformation_Text_Color,
    leftSide_ContactInformation_Logos_Color,
    leftSide_Education_Header_Color,
    leftSide_EducationInformation_Color,

    rightSide_Color,
    rightSide_Border_Color,
    rightSide_Header_Text_Color,
    rightSide_Header_Background_Color,
    rightSide_Header_Shadow_Color,
    rightSide_AboutMe_Color,
    rightSide_Work_Title_Color,
    rightSide_Work_Information_Color,
    rightSide_Work_Description_Color,
    rightSide_Competence_Skill,
    rightSide_Competence_Level_Background,
    rightSide_Competence_Level_Foreground
    
}) {

    const [contactInfoFontSize, setContactInfoFontSize] = useState(16);
    const [educationFontSize, setducationFontSize] = useState(16);
    const [nameFontSize, setNameFontSize] = useState(16);
    const educationRef = useRef(null);
    const contactInfoRef = useRef(null);
    const nameRef = useRef(null);

    const [profileFontSize, setProfileFontSize] = useState(16);
    const [workFontSize, setWorkFontSize] = useState(16);
    const [competencyFontSize, setCompetencyFontSize] = useState(16);
    const profileRef = useRef(null);
    const workRef = useRef(null);
    const competencyRef = useRef(null);


     //--------------------Change Work-CV-Text--------------------//
     useEffect(() => {
        if (!competencyRef.current) return;
    
        const checkOverflow = () => {
            const element = competencyRef.current;
            if (!element) return;
    
            const { scrollWidth, clientWidth, scrollHeight, clientHeight } = element;
            return scrollWidth > clientWidth || scrollHeight > clientHeight;
        };
    
        const adjustFontSize = () => {
            let newSize = competencyFontSize;
            let element = competencyRef.current;
            if (!element) return;
    
            while (checkOverflow() && newSize > 6) {
                newSize--;
                element.style.fontSize = `${newSize}px`;
            }
    
            while (!checkOverflow() && newSize < 16) {
                newSize++;
                element.style.fontSize = `${newSize}px`;
                if (checkOverflow()) {
                    newSize--;
                    element.style.fontSize = `${newSize}px`;
                    break;
                }
            }
    
            if (newSize !== competencyFontSize) {
                setCompetencyFontSize(newSize);
            }
        };
    
        adjustFontSize();
        }, [work_Experiences]);

    //--------------------Change Work-CV-Text--------------------//
    useEffect(() => {
        if (!workRef.current) return;
    
        const checkOverflow = () => {
            const element = workRef.current;
            if (!element) return;
    
            const { scrollWidth, clientWidth, scrollHeight, clientHeight } = element;
            return scrollWidth > clientWidth || scrollHeight > clientHeight;
        };
    
        const adjustFontSize = () => {
            let newSize = workFontSize;
            let element = workRef.current;
            if (!element) return;
    
            while (checkOverflow() && newSize > 6) {
                newSize--;
                element.style.fontSize = `${newSize}px`;
            }
    
            while (!checkOverflow() && newSize < 16) {
                newSize++;
                element.style.fontSize = `${newSize}px`;
                if (checkOverflow()) {
                    newSize--;
                    element.style.fontSize = `${newSize}px`;
                    break;
                }
            }
    
            if (newSize !== workFontSize) {
                setWorkFontSize(newSize);
            }
        };
    
        adjustFontSize();
        }, [work_Experiences]);

     //--------------------Change Profile-CV-Text--------------------//
    useEffect(() => {
    if (!profileRef.current) return;

    const checkOverflow = () => {
        const element = profileRef.current;
        if (!element) return;

        const { scrollWidth, clientWidth, scrollHeight, clientHeight } = element;
        return scrollWidth > clientWidth || scrollHeight > clientHeight;
    };

    const adjustFontSize = () => {
        let newSize = profileFontSize;
        let element = profileRef.current;
        if (!element) return;

        while (checkOverflow() && newSize > 6) {
            newSize--;
            element.style.fontSize = `${newSize}px`;
        }

        while (!checkOverflow() && newSize < 16) {
            newSize++;
            element.style.fontSize = `${newSize}px`;
            if (checkOverflow()) { 
                newSize--;
                element.style.fontSize = `${newSize}px`;
                break;
            }
        }

        if (newSize !== profileFontSize) {
            setProfileFontSize(newSize);
        }
    };

    adjustFontSize();
    }, [about_Me]);

 //--------------------Change Name-CV-Text--------------------//
 useEffect(() => {
    if (!nameRef.current) return;

    const checkOverflow = () => {
        const element = nameRef.current;
        if (!element) return;

        const { scrollWidth, clientWidth, scrollHeight, clientHeight } = element;
        return scrollWidth > clientWidth || scrollHeight > clientHeight;
    };

    const adjustFontSize = () => {
        let newSize = nameFontSize;
        let element = nameRef.current;
        if (!element) return;

        while (checkOverflow() && newSize > 6) {
            newSize--;
            element.style.fontSize = `${newSize}px`;
        }

        while (!checkOverflow() && newSize < 24) {
            newSize++;
            element.style.fontSize = `${newSize}px`;
            if (checkOverflow()) {
                newSize--;
                element.style.fontSize = `${newSize}px`;
                break;
            }
        }

        if (newSize !== nameFontSize) {
            setNameFontSize(newSize);
        }
    };

    adjustFontSize();
    }, [name]);

    //--------------------Change Contact-Information-CV-Text--------------------//
    useEffect(() => {
        if (!contactInfoRef.current) return;

        const checkOverflow = () => {
            const element = contactInfoRef.current;
            if (!element) return;

            const { scrollWidth, clientWidth, scrollHeight, clientHeight } = element;
            return scrollWidth > clientWidth || scrollHeight > clientHeight;
        };

        const adjustFontSize = () => {
            let newSize = contactInfoFontSize;
            let element = contactInfoRef.current;
            if (!element) return;

            while (checkOverflow() && newSize > 6) {
                newSize--;
                element.style.fontSize = `${newSize}px`;
            }

            while (!checkOverflow() && newSize < 16) {
                newSize++;
                element.style.fontSize = `${newSize}px`;
                if (checkOverflow()) {
                    newSize--;
                    element.style.fontSize = `${newSize}px`;
                    break;
                }
            }

            if (newSize !== contactInfoFontSize) {
                setContactInfoFontSize(newSize);
            }
        };

        adjustFontSize();
    }, [address, zip_code, phoneNumber, email, linkedin, otherLink, ]);

    //--------------------Change Education-CV-Text--------------------//
    useEffect(() => {
        if (!educationRef.current) return;

        const checkOverflow = () => {
            const element = educationRef.current;
            if (!element) return;

            const { scrollWidth, clientWidth, scrollHeight, clientHeight } = element;
            return scrollWidth > clientWidth || scrollHeight > clientHeight;
        };

        const adjustFontSize = () => {
            let newSize = educationFontSize;
            let element = educationRef.current;
            if (!element) return;

            while (checkOverflow() && newSize > 6) {
                newSize--;
                element.style.fontSize = `${newSize}px`;
            }

            while (!checkOverflow() && newSize < 16) {
                newSize++;
                element.style.fontSize = `${newSize}px`;
                if (checkOverflow()) { 
                    newSize--;
                    element.style.fontSize = `${newSize}px`;
                    break;
                }
            }

            if (newSize !== educationFontSize) {
                setducationFontSize(newSize);
            }
        };

        adjustFontSize();
    }, [education_Experiences]);


    return (
        <div className = "CV-container" id='CV-modell'>


            <div className = "left-side" style={{ backgroundColor: leftSide_Color }}>


                <div className="profile-image">
                    <div
                        className="profile-image-img"
                        style={{ backgroundImage: `url(${profilePicture})`, border: `4px solid ${leftSide_ProfileImage_Color}`}}
                    ></div>
                </div>


                <div className = "name-CV" ref={nameRef} style={{ color: leftSide_Name_Color}}>
                    <h2>
                        {name}
                    </h2>
                </div>

                <div className = "border_left-CV" style = {{borderBottom: `3px solid ${leftSide_Border_Color}`}}></div>

                <h3 className = "left_side-CV-header" style={{ color: leftSide_Headers_Color}}>
                    Kontakt
                </h3>

                <div className = "contact-information-CV" ref={contactInfoRef} style={{ color: leftSide_ContactInformation_Text_Color }}>
                    {address && (
                        <h3>
                            <i className="fa-solid fa-location-dot" style={{ color: leftSide_ContactInformation_Logos_Color}}></i><p>{address}</p>
                        </h3>
                    )}
                    {zip_code && (
                        <h3>
                        <i className = "fa-solid fa-location-dot" style={{ color: leftSide_ContactInformation_Logos_Color}}></i><p>{zip_code}</p>
                        </h3>
                    )}
                    {phoneNumber && (
                        <h3>
                        <i className = "fa-solid fa-phone" style={{ color: leftSide_ContactInformation_Logos_Color}}></i><p>{phoneNumber}</p>
                        </h3>
                    )}
                    {email && (
                        <h3>
                        <i className = "fa-solid fa-envelope" style={{ color: leftSide_ContactInformation_Logos_Color}}></i><p>{email}</p>
                        </h3>
                    )}
                    {linkedin && (
                        <h3>
                        <i className = "fa-brands fa-linkedin" style={{ color: leftSide_ContactInformation_Logos_Color}}></i><p>{linkedin}</p>
                        </h3>
                    )}
                    {otherLink && (
                        <h3>
                        <i className = "fa-solid fa-earth-americas" style={{ color: leftSide_ContactInformation_Logos_Color}}></i><p>{otherLink}</p>
                        </h3>
                    )}
                </div>

                <h3 className = "left_side-CV-header" style={{ color: leftSide_Headers_Color}}>
                    Utbildning
                </h3>

                <div className = "education-CV" ref={educationRef} style={{ color: leftSide_EducationInformation_Color,fontSize: `${educationFontSize}px`}}>

                        {education_Experiences.map((experience, index) => (
                            <div key={index} >
                                <h2  className = "education_program-CV" style={{ color: leftSide_Education_Header_Color}}>{experience.education_program}</h2>
                                <p className = "education_school-CV">{experience.education_school}</p>
                                <div className = "education_dates-CV">
                                    <p>{experience.education_start_date}<span className="dash" style={{ color: leftSide_Education_Header_Color}}>-</span></p>
                                    <p>{experience.education_end_date}</p>
                                </div>
                            </div>
                        ))}

                </div>

            </div>

            <div className = "right-side" style={{ backgroundColor: rightSide_Color }}>
                
                <h3 className = "right_side-CV-header" style={{ color: rightSide_Header_Text_Color, background: rightSide_Header_Background_Color, textShadow: `2px 2px 4px ${rightSide_Header_Shadow_Color}`}}>Profil</h3>

                <div className = "about_Me-CV" ref={profileRef} style={{ color: rightSide_AboutMe_Color}}>
                    <p>{about_Me}</p>
                </div>

                <h3 className = "right_side-CV-header" style={{ color: rightSide_Header_Text_Color, background: rightSide_Header_Background_Color, textShadow: `2px 2px 4px ${rightSide_Header_Shadow_Color}`}}>Arbetslivserfarenhet</h3>

                <div className = "work-experience-CV" ref={workRef}>
                    {work_Experiences.map((experience, index) => (
                        <div key={index}>
                            <h2 className="job_title-CV" style={{ color: rightSide_Work_Title_Color }}>
                                <p>{experience.job_title}</p>
                            </h2>
                            <h4 className="job_statistics-CV" style={{ color: rightSide_Work_Information_Color }}>
                                <p>{experience.job_employer},</p>
                                <p>{experience.job_start_date} -</p>
                                <p>{experience.job_end_date},</p>
                                <p>{experience.job_city}</p>
                            </h4>
                            <p className="job_description-CV" style={{ color: rightSide_Work_Description_Color }}>
                            <p>{experience.job_description}</p>
                            </p>
                            <div className="border_right-CV" style = {{borderBottom: `3px solid ${rightSide_Border_Color}`}}></div>
                        </div>
                    ))}
                </div>

                <h3 className = "right_side-CV-header" style={{ color: rightSide_Header_Text_Color, background: rightSide_Header_Background_Color, textShadow: `2px 2px 4px ${rightSide_Header_Shadow_Color}`}}>Kompetenser</h3>

                <div className = "competencies-CV" ref={competencyRef}>
                    {competencies_Experiences.map((experience, index) => (
                        <div key={index}>
                            <div className = "name_and_level_of_skill-CV">
                                <p id = "competence_name" style={{ color: rightSide_Competence_Skill }}>{experience.competence_name}</p>

                                {experience.competence_level === "1" && (
                                    <div className = "level_bar" style={{ backgroundColor: rightSide_Competence_Level_Background }}>
                                        <div className = "level_bar_20_percent" style={{ backgroundColor: rightSide_Competence_Level_Foreground }}></div>
                                    </div>
                                )}
                                {experience.competence_level === "2" && (
                                    <div className = "level_bar" style={{ backgroundColor: rightSide_Competence_Level_Background }}>
                                        <div className = "level_bar_40_percent" style={{ backgroundColor: rightSide_Competence_Level_Foreground }}></div>
                                    </div>
                                )}
                                {experience.competence_level === "3"&& (
                                    <div className = "level_bar" style={{ backgroundColor: rightSide_Competence_Level_Background }}>
                                        <div className = "level_bar_60_percent" style={{ backgroundColor: rightSide_Competence_Level_Foreground }}></div>
                                    </div>
                                )}
                                {experience.competence_level === "4"&& (
                                    <div className = "level_bar" style={{ backgroundColor: rightSide_Competence_Level_Background }}>
                                        <div className = "level_bar_80_percent" style={{ backgroundColor: rightSide_Competence_Level_Foreground }}></div>
                                    </div>
                                )}
                                {experience.competence_level === "5"&& (
                                    <div className = "level_bar" style={{ backgroundColor: rightSide_Competence_Level_Background }}>
                                        <div className = "level_bar_100_percent" style={{ backgroundColor: rightSide_Competence_Level_Foreground }}></div>
                                    </div>
                                )}
                            </div>  
                        </div>
                    ))}
                </div>

            </div>

        </div>
    );
};

export default CV_Modell;