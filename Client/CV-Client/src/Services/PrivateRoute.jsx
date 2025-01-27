import React from 'react';
import { Navigate } from 'react-router-dom';
import { useAuth } from './AuthContext'; 

const PrivateRoute = ({ children }) => {
    const { authToken } = useAuth(); 

    
    if (!authToken) {
        return <Navigate to="/Login" replace />;
    }

    
    return children;
};

export default PrivateRoute;
