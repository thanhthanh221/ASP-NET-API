import React, { useEffect, useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import { AiFillGithub } from "react-icons/ai";
import { FcGoogle } from "react-icons/fc";
import { BsFacebook } from "react-icons/bs";
import Helmet from '../Layout/Helmet';
import { request } from '../../utils/request';
import { useDispatch } from 'react-redux';
import { setJwt } from '../../redux/Login-Project/loginProject';

const Login = () => {

    const navigate = useNavigate();
    const dispatch = useDispatch();

    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    useEffect(() => {
        if (localStorage.getItem('Jwt')) {
            navigate('/');
        }
    }, [])

    const LoginSubmit = async (e) => {
        const formData = new FormData();
        formData.append('Email', email);
        formData.append('PassWord', password);

        e.preventDefault();
        try {
            const resp = await request.post('/Auth/Login', formData);
            if (resp.data.message === 'thành công') {
                localStorage.setItem('Jwt', resp.data.token);
                localStorage.setItem('nameUser', resp.data.obj_user.name);
                dispatch(setJwt(resp.data.token));
                navigate('/')
            }
        } catch (error) {
            if (error.response.status === 400) {
                alert('Vui lòng điền thông tin');
            }
            else if (error.response.status === 401) {
                alert('Tên tài khoản mật khẩu không chính xác')
            }

        }
    }

    return (
        <Helmet title='Đăng Nhập'>
            <div className='Log__container__form'>
                <div className='Log__container__form__header'>Đăng nhập</div>
                <form onSubmit={LoginSubmit}>
                    <input onChange={(e) => setEmail(e.target.value)} type="text" id="login" className="Log__container__form__emailMk" placeholder="Email" />
                    <input onChange={(e) => setPassword(e.target.value)} type="password" id="password" class="Log__container__form__emailMk" placeholder="Mật Khẩu" />
                    <div className='Log__container__form__button'>
                        <div id="formFooter">
                            <a>Quên mật khẩu ?</a>
                        </div>
                        <button type='submit'>Đăng Nhập</button>
                    </div>
                </form>
                <div className='Log__container__form__FbGgGit'>
                    <button className='Log__container__form__FbGgGit__value'><BsFacebook color='#33CCFF' size={20} /> Facebook</button>
                    <button className='Log__container__form__FbGgGit__value'><FcGoogle size={20} /> Google</button>
                    <button className='Log__container__form__FbGgGit__value'><AiFillGithub size={20} /> GitHub</button>
                </div>
                <div className='Log__container__form__footer'>
                    <div>Bạn chưa có tài khoản?</div>
                    <Link to='/identity/dangKy/nguoiMua'>
                        <div>đăng kí</div>
                    </Link>
                </div>
            </div>
        </Helmet>
    )
}

export default Login