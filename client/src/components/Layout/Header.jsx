import React, { useRef, useEffect, useState } from 'react'
import { AiOutlineLogin, AiOutlineMenu, AiOutlineSearch, AiOutlineShoppingCart, AiOutlineUser } from 'react-icons/ai'
import { useSelector } from 'react-redux'
import { Link, useLocation } from 'react-router-dom'

const mainNav = [
    {
        display: "Trang chủ",
        path: "/"
    },
    {
        display: "Sản phẩm",
        path: "/danhMucSanPham"
    },
    {
        display: "Phụ kiện",
        path: "/accessories"
    },
    {
        display: "Liên hệ",
        path: "/contact"
    }
]

const Header = () => {

    const { pathname } = useLocation()
    const activeNav = mainNav.findIndex(e => e.path === pathname);

    const headerRef = useRef(null);
    const [jwt, setJwt] = useState(localStorage.getItem('Jwt'));

    const userName = localStorage.getItem('nameUser')
    
    const logOutClick = () => {
        localStorage.removeItem('Jwt');
        setJwt('');
        localStorage.removeItem('nameUser');
    }

    // xử lý của UseRef 
    const menuLeft = useRef(null)

    const menuToggle = () => menuLeft.current.classList.toggle('active')

    return (
        <div className="header" ref={headerRef}>
            <div className="container">
                <div className="header__menu">
                    <div className="header__menu__mobile-toggle" onClick={menuToggle}>
                        <AiOutlineMenu />
                    </div>
                    <div className="header__menu__left" ref={menuLeft}>
                        <div className="header__menu__left__close" onClick={menuToggle}>
                            <i className='bx bx-chevron-left'></i>
                        </div>
                        {
                            mainNav.map((item, index) => (
                                <div
                                    key={index}
                                    className={`header__menu__item header__menu__left__item ${index === activeNav ? 'active' : ''}`}
                                    onClick={menuToggle}
                                >
                                    {/* Chuyển hướng đến */}
                                    <Link to={item.path}>
                                        <span>{item.display}</span>
                                    </Link>
                                </div>
                            ))
                        }
                    </div>
                    {
                        !jwt ?
                            <div className="header__menu__right">
                                <div className="header__menu__item header__menu__right__item">
                                    <AiOutlineSearch />
                                </div>
                                <div className="header__menu__item header__menu__right__item">
                                    <Link to='/identity/dangNhap'>
                                        <AiOutlineUser />
                                    </Link>
                                </div>
                            </div> :
                            <div className="header__menu__right">
                                <div className="header__menu__item header__menu__right__item">
                                    <AiOutlineSearch />
                                </div>
                                <div className="header__menu__item header__menu__right__item">
                                    <Link to="/cart">
                                        <AiOutlineShoppingCart />
                                    </Link>
                                </div>
                                <div className="header__menu__item header__menu__right__text">
                                    <a className=''>{userName}</a>
                                    <ul>
                                        <li onClick={logOutClick}>Đăng xuất</li>
                                    </ul>
                                </div>
                            </div>
                    }
                </div>
            </div>
        </div>
    )
}

export default Header
