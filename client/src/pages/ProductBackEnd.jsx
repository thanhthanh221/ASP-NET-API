import React, { useEffect, useMemo, useState } from 'react'

import Helmet from '../components/Layout/Helmet'
import Section, {SectionBody, SectionTitle} from '../components/Layout/Section'
import Grid from '../components/Customs/Grid'

import productData from '../assets/fake-data/products'
import { useParams } from 'react-router-dom'
import ProductViewBackEnd from '../components/Product/ProductViewBackEnd'
import { request } from '../utils/request'
import ReviewProduct from '../components/ReviewProduct/ReviewProduct'


const ProductBackEnd = () => {
    const params = useParams();
    const [product, setProduct] = useState({});
    const [commentsOfProduct, setCommentsOfProduct] = useState([]);
    const [page, SetPage] = useState(0);

    useEffect(() => {
        request.get('/Product/Id',{
            params :{
                Id: params.id,
            }
        })
        .then((res) => {
            setProduct(res.data);
        })
        .catch((err) => {
            console.log(err);
        })
    },[]);
    useEffect(() => {
        request.get('/ProductReview/ProductId',{
            params :{
                ProductId: params.id,
                page: page
            }
        })
        .then((res) => {
            setCommentsOfProduct(res.data);
        })
        .catch((err) => {
            console.log(err);
        })

    },[page])

    // const relatedProducts = productData.getProducts(8)

    React.useEffect(() => {
        window.scrollTo(0,0)
    }, [product])
    return (
        <Helmet title={"Trang Sản Phẩm"}>
            {/* Phẩn đầu của sản phẩm */}
            <Section>
                <SectionBody>
                    <ProductViewBackEnd product={product}/>
                    <ReviewProduct 
                        CommentsInPage={commentsOfProduct}
                        page = {page}
                        setPage = {(input) => SetPage(input)}
                        product = {product}
                        />
                </SectionBody>
            </Section>
            {/* Phần hiển thị thông tin sản phẩm khác */}
            <Section>
                <SectionTitle>
                    Khám phá thêm
                </SectionTitle>
                {/* <SectionBody>
                    <Grid
                        col={4}
                        mdCol={2}
                        smCol={1}
                        gap={20}
                    >
                        {
                            relatedProducts.map((item, index) => (
                                <ProductCardBackEnd
                                    key={index}
                                    img01={item.image01}
                                    img02={item.image02}
                                    name={item.title}
                                    price={Number(item.price)}
                                    slug={item.slug}
                                />   
                            ))
                        }
                    </Grid>
                </SectionBody> */}
            </Section>
        </Helmet>
    )
}

export default ProductBackEnd
