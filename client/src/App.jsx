import logo from './logo.svg';
import './App.css';
import UseRefTest from './UseRefTest';
import ReadProduct from './ReadProduct';
import ClearUpFuc from './ClearUpFuc';
import SetUpAvatar from './SetUpAvatar';
import UseLayEf from './UseLayEf';
import Test from './UseeReduce';
import Home from './Pages/Home';
import {NavLink, Routes, Route, BrowserRouter} from "react-router-dom";


function App() {
  return (
    <BrowserRouter>
      <div className='app'>
        <header className="header">
          <NavLink className="header_navbar" to="/Pages/Home">Home</NavLink>
          
        </header>
        <div className="cotainer">
          <Routes>
            <Route path='/Pages/Home' element = {<Home/>} />
          </Routes>
        </div>
        <footer className="footer">

        </footer>

      </div>
    </BrowserRouter>
  );
}

export default App;
