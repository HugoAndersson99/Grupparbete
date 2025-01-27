import React, { createContext, useContext, useState } from 'react';


const AuthContext = createContext();


export const AuthProvider = ({ children }) => {
    const [authToken, setAuthToken] = useState(() => {
        return sessionStorage.getItem('authToken') || null; 
    });

    const login = (token) => {
        setAuthToken(token);
        sessionStorage.setItem('authToken', token); 
    };

    

    return (
        <AuthContext.Provider value={{ authToken, login }}>
            {children}
        </AuthContext.Provider>
    );
};


export const useAuth = () => {
    return useContext(AuthContext);
};
