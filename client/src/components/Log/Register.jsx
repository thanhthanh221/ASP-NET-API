import React from 'react'
import Button from '../Button'
import { Link } from 'react-router-dom'
import {  AiFillGithub } from "react-icons/ai";
import { FcGoogle } from "react-icons/fc";
import { BsFacebook } from "react-icons/bs";

const register = () => {
  return (
    <div className='Log__container__form'>
        <div className='Log__container__form__header'>Đăng Kí</div>
        <input type="text" id="login" className="Log__container__form__emailMk" placeholder="Email"/>
        <input type="password" id="login" class="Log__container__form__emailMk" placeholder="Mật Khẩu"/>
        <div className='Log__container__form__button'>
            <div id="formFooter">
                <a >Quên mật khẩu ?</a>
            </div>
            <Button size="sm">Đăng nhập</Button>
        </div>
        <div className='Log__container__form__FbGgGit'>
            <button className='Log__container__form__FbGgGit__value'><BsFacebook color='#33CCFF' size={20}/> Facebook</button>
            <button className='Log__container__form__FbGgGit__value'><FcGoogle size={20}/> Google</button>
            <button className='Log__container__form__FbGgGit__value'><AiFillGithub size={20}/> GitHub</button>
        </div>
        <div>
            Bạn chưa có tài khoản? 
            <Link to='/'>
                Đăng kí
            </Link>
        </div>
    </div>
  )
}

export default register
