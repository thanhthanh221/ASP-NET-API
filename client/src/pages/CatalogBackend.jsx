import React, { useCallback, useState, useEffect, useRef } from 'react'
import axios from 'axios'
import { request } from '../utils/request'

import Helmet from '../components/Helmet'
import CheckBox from '../components/CheckBox'

import productData from '../assets/fake-data/products'
import Button from '../components/Button'
import InfinityListPage from '../components/InfinityListPage'
import CheckBoxBackend from '../components/CheckBoxBackend'
import Paging from '../components/Paging'
import CheckBoxStart from '../components/CheckBoxStart'
import numberStarProduct from '../assets/fake-data/product-Star'


const CatalogBackend = () => {

    const initFilter = {
        category: [],
        color: [],
        size: []
    }
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
        request.get('/Product', 
        {
            params: {
                page : page,
                filerByStar : filterByStar,
                filerByCategory: filterByCategory
            }
        })
        .then((res) => {
            setProductPage(res.data.data);
        })
        .catch((err) => {
            console.log(err);
        });
    },[page, filterByStar, categories]);  

    const productList = productData.getAllProducts();

    const [products, setProducts] = useState(productList);

    const [filter, setFilter] = useState(initFilter);

    const filterSelect = (type, checked, item) => {
        if (checked) {
            switch(type) {
                case "CATEGORY":
                    setFilter(
                        {...filter,
                        category: [ ...filter.category,item.name]})
                    break;
                
                default:
            }
        } else {
            switch(type) {
                case "CATEGORY":
                    const newCategory = filter.category.filter(e => e !== item.name)
                    setFilter({...filter, category: newCategory})
                    break
                default:
            }
        }
    }
    const onChangeCheckBox = (input) => {
        console.log(input)
    }
    const onClickCheckStar = (e) => {
        console.log(e.target);
        let sum = 0;
        if(e.target.classList[0] === 'checkBoxStart__Contener'  ) {
            e.target.classList.toggle('checkBoxStart__CheckTrue');
            e.target.childNodes.forEach((child) => {
                if(child.classList[1] === 'checkBoxStart__Contener__Value__True'){
                    sum += 1;

                }
            })
            setFilterByStar(sum);
        }
    }

    const clearFilter = () => setFilter(initFilter)

    const updateProducts = useCallback(
        () => {
            let temp = productList

            if (filter.category.length > 0) {
                temp = temp.filter(e => filter.category.includes(e.name))
            }

            if (filter.color.length > 0) {
                temp = temp.filter(e => {
                    const check = e.colors.find(color => filter.color.includes(color))
                    return check !== undefined
                })
            }

            if (filter.size.length > 0) {
                temp = temp.filter(e => {
                    const check = e.size.find(size => filter.size.includes(size))
                    return check !== undefined
                })
            }

            setProducts(temp)
        },
        [filter, productList],
    )

    useEffect(() => {
        updateProducts()
    }, [updateProducts])

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
                                            onChange= {(input) => onChangeCheckBox(input.checked)}
                                            checked= {filter.category.includes(item.name)}
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
                                            onChange= {(input) => filterSelect("COLOR", input.checked, item)}
                                            checked= {filter.color.includes(item.color)}
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
