import React from 'react'
import img from '../assets/images/JB-FE-Shop_10.png'
import Helmet from '../components/Helmet'
import { Link } from 'react-router-dom'
import Login from '../components/Log/Login'

const LoginBackEnd = () => {
  return (
    <Helmet title={"Đăng nhập"}>
      <div className="Log">
        <div className='Log__header'>
          <Link to={'/'}>
            <img src={img} className="Log__header__image" />
          </Link>
          <div className="Log__header__text">Đăng nhập</div>
        </div> 
        <div className='Log__container'>
          <div className='Log__container__image'> 
            <img src={img} alt='' />
          </div>
          <Login/>
        </div>
      </div>
    </Helmet>
  )
}

export default LoginBackEnd