import './Css/App.css'
import Login_Page from './Pages/Login_Page.jsx';
import { Routes, Route, Navigate } from 'react-router-dom';
function App() {
  

  return (
    <Routes>
      <Route path="/Login" element={<Login_Page />} />
    </Routes>
    
  )
};

export default App;
