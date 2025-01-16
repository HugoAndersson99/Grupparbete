import './Css/App.css'
import Login_Page from './Pages/Login_Page.jsx';
import Home_Page from './Pages/Home_Page.jsx';
import { Routes, Route, Navigate } from 'react-router-dom';
import Register_Page from './Pages/Register_Page.jsx';
function App() {
  

  return (
    <Routes>
      <Route path="/" element={<Home_Page />} />
      <Route path="/Login" element={<Login_Page />} />
      <Route path="/Register" element={<Register_Page />} />
    </Routes>
    
  )
};

export default App;
