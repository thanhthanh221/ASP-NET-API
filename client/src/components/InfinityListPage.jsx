import React, { useEffect, useRef, useState } from 'react'
import PropTypes from 'prop-types'

import Grid from './Grid'
import ProductCard from './ProductCard'

const InfinityListPage = (props) => {

    const listRef = useRef(null);
    
    console.log(props.data);

    return (
        <div ref={listRef}>
            <Grid
                col={3}
                mdCol={2}
                smCol={1}
                gap={20}
            >
                {
                    props.data.map((item, index) => (
                        <ProductCard
                            key={index}
                            img01={item.image01}
                            img02={item.image02}
                            name={item.name}
                            price={Number(item.price)}
                            Id = {item.Id}
                        />
                    ))
                }
            </Grid>
        </div>
    )
}

InfinityListPage.propTypes = {
    data: PropTypes.array.isRequired
}

export default InfinityListPage
