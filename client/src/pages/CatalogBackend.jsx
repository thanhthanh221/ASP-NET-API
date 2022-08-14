import React, { useState, useEffect, useRef } from 'react'
import { request } from '../utils/request'

import Helmet from '../components/Helmet'

import Button from '../components/Button'
import InfinityListPage from '../components/InfinityListPage'
import CheckBoxBackend from '../components/CheckBoxBackend'
import Paging from '../components/Paging'
import CheckBoxStart from '../components/CheckBoxStart'
import numberStarProduct from '../assets/fake-data/product-Star'


const CatalogBackend = () => {

    const [categories, setCategory] = useState([]);

    const [productpage, setProductPage] = useState([]);

    const [page, setPage] = useState(0);
    
    const [filterByStar, setFilterByStar] = useState(0);

    const [filterByCategory, setFilterByCategory] = useState([]);

    useEffect(() => {
        request.get('/Categories')
        .then((res) => {
            setCategory(res.data);
        })
        .catch((err) => {
            console.log(err);
        })
    },[]);
    useEffect(() => {
        var params = new URLSearchParams();
        params.append("page", page);
        params.append("filerByStar",filterByStar);
        if(filterByCategory.length) {
            filterByCategory.forEach((p) => {
                params.append("filerByCategory", p);
            })
        }
        const paramsRequest = {
            params: params
        }

        request.get('/Product', paramsRequest)
        .then((res) => {
            setProductPage(res.data.data);
            console.log(res.data.data);
        })
        .catch((err) => {
            console.log(err);
        });
    },[page, filterByStar, filterByCategory]);  

    const onChangeCheckBox = (input) => {
        if(input.checked) {
            setFilterByCategory([...filterByCategory ,input.id]);
        }
        else if(!input.checked) {
            const updateFilterByCategory = filterByCategory.filter(p => p !== input.id)
            setFilterByCategory(updateFilterByCategory);
        }
    }
    const onClickCheckStar = (e) => {
        let sum = 0;
        if(e.target.classList[0] === 'checkBoxStart__Contener' && 
                        e.target.classList[1] !== 'checkBoxStart__CheckTrue' ) {
            e.target.classList.add('checkBoxStart__CheckTrue');
            e.target.childNodes.forEach((child) => {
                if(child.classList[1] === 'checkBoxStart__Contener__Value__True'){
                    sum += 1;

                }
            })
            setFilterByStar(sum);
        }
        else if(e.target.classList[1] === 'checkBoxStart__CheckTrue') {
            e.target.classList.remove('checkBoxStart__CheckTrue');
            setFilterByStar(0);
        } 
    }

    const clearFilter = () => {
        setFilterByStar(0);
        setFilterByCategory([]);
    }

    const filterRef = useRef(null)

    const showHideFilter = () => filterRef.current.classList.toggle('active')

    return (
        <Helmet title="Sản phẩm">
            <div className="catalog">
                <div className="catalog__filter" ref={filterRef}>
                    <div className="catalog__filter__close" onClick={() => showHideFilter()}>
                        <i className="bx bx-left-arrow-alt"></i>
                    </div>
                    <div className="catalog__filter__widget">
                        <div className="catalog__filter__widget__title">
                            danh mục sản phẩm
                        </div>
                        <div className="catalog__filter__widget__content">
                            {
                                categories.map((item, index) => (
                                    <div key={index} className="catalog__filter__widget__content__item">
                                        <CheckBoxBackend
                                            name= {item.name}
                                            id= {item.id}
                                            onChange= {(input) => onChangeCheckBox(input)}
                                            checked= {filterByCategory.includes(item.id)}
                                        />
                                    </div>
                                ))
                            }
                        </div>
                    </div>

                    <div className="catalog__filter__widget">
                        <div className="catalog__filter__widget__title">
                            Đánh giá
                        </div>
                        <div className="catalog__filter__widget__content">
                            {
                                numberStarProduct.map((item, index) => (
                                    <div key={index} className="catalog__filter__widget__content__item">
                                        <CheckBoxStart
                                            numberStar = {item.numberStar} 
                                            onClick= {(e) => onClickCheckStar(e)}
                                            filerByStar = {filterByStar}
                                        />
                                    </div>
                                ))
                            }
                        </div>
                    </div>
                    <div className="catalog__filter__widget">
                        <div className="catalog__filter__widget__content">
                            <Button size="sm" onClick={clearFilter}>xóa bộ lọc</Button>
                        </div>
                    </div>
                </div>
                <div className="catalog__filter__toggle">
                    <Button size="sm" onClick={() => showHideFilter()}>bộ lọc</Button>
                </div>
                <div className="catalog__content">
                    <InfinityListPage
                        data = {productpage}
                        page = {page}
                        setProductPage = {input => setProductPage(input)}
                    />
                </div>  
            </div>
            <div className="paging__container">
                <Paging
                    data = {productpage}
                    page = {page}
                    setPage = {(input) => setPage(input)}
                />
            </div>
        </Helmet>
    )
}

export default CatalogBackend

