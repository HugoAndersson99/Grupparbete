import './Css/App.css';
import Login_Page from './Pages/Login_Page.jsx';
import Home_Page from './Pages/Home_Page.jsx';
import { Routes, Route } from 'react-router-dom';
import Register_Page from './Pages/Register_Page.jsx';
import Build_CV_Page from './Pages/Build_CV_Page.jsx';
import MittKonto_Page from './Pages/MittKonto_Page.jsx';
import FinishedCV_Page from './Pages/FinishedCV_Page.jsx';
import { AuthProvider } from './Services/AuthContext.jsx';
import PrivateRoute from './Services/PrivateRoute.jsx';

function App() {
  return (
    <AuthProvider>
      <Routes>
        <Route path="/" element={<Home_Page />} />
        <Route path="/Login" element={<Login_Page />} />
        <Route path="/Register" element={<Register_Page />} />

        {/* Skyddade Sidor */}
        <Route 
          path="/Build_CV" 
          element={
            <PrivateRoute>
              <Build_CV_Page />
            </PrivateRoute>
          } 
        />
        <Route 
          path="/MittKonto" 
          element={
            <PrivateRoute>
              <MittKonto_Page />
            </PrivateRoute>
          } 
        />
        <Route 
          path="/Finish" 
          element={
            <PrivateRoute>
              <FinishedCV_Page />
            </PrivateRoute>
          } 
        />
      </Routes>
    </AuthProvider>
  );
}

export default App;
