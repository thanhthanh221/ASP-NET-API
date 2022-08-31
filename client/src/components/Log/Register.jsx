import React, { useState } from 'react'
import Button from '../Customs/Button'
import { Link } from 'react-router-dom'
import { AiFillGithub } from "react-icons/ai";
import { FcGoogle } from "react-icons/fc";
import { BsFacebook } from "react-icons/bs";
import Helmet from '../Layout/Helmet';

const Register = () => {
    const [name, setName] = useState('');
    const [email, setEmail] = useState('');
    const [passWord, setPassword] = useState('');
    return (
        <Helmet title='Đăng Kí'>
            <div className='Log__container__form'>
                <div className='Log__container__form__header'>đăng ký</div>
                <form className='Log__container__form__value'>
                    <input type="text" id="email" className="Log__container__form__emailMk" placeholder="Email" />
                    <input type="text" id="mk" className="Log__container__form__emailMk" placeholder="Tên người dùng" />
                    <input type="password" id="mk2" className="Log__container__form__emailMk" placeholder="Nhập lại mật khẩu" />

                    <div className='Log__container__form__button'>
                        <div id="formFooter">
                            <p >Quên mật khẩu ?</p>
                        </div>
                        <Button size="sm">Đăng kí</Button>
                    </div>
                </form>

                <div className='Log__container__form__FbGgGit'>
                    <button className='Log__container__form__FbGgGit__value'><BsFacebook color='#33CCFF' size={20} /> Facebook</button>
                    <button className='Log__container__form__FbGgGit__value'><FcGoogle size={20} /> Google</button>
                    <button className='Log__container__form__FbGgGit__value'><AiFillGithub size={20} /> GitHub</button>
                </div>
                <div className='Log__container__form__footer'>
                    Bạn là thành viên rồi?
                    <Link to='/identity'>
                        đăng nhâp
                    </Link>
                </div>
            </div>
        </Helmet>
    )
}

export default Register
