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
import UseeReduce from './UseeReduce';
import GetProduct from './API/ApiProduct/GetProduct';
import PostProduct from './API/ApiProduct/PostProduct';
import UseMemoTest from './UseMemoTest';


function App() {
  return (
    <BrowserRouter>
      <div className='app'>
        <header className="header">
          <NavLink className="header_navbar" to="/Pages/Home">Home</NavLink>
          <NavLink className="header_navbar" to="/API/ApiProduct/GetProduct">Get Product</NavLink>
          <NavLink className={"header_navbar" } to="/API/ApiProduct/PostProduct">PostProduct</NavLink>
          
        </header>
        <div className="cotainer">
          <Routes>
            <Route path='/Pages/Home' element = {<Home/>} />
            <Route path='/API/ApiProduct/GetProduct' element = {<GetProduct/>}></Route>
            <Route path='/API/ApiProduct/PostProduct' element= {<PostProduct/>}></Route>
          </Routes>
        </div>
        <footer className="footer">

        </footer>

      </div>
    </BrowserRouter>
  );
}

export default App;
