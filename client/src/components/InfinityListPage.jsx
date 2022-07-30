import React, { useEffect, useRef, useState } from 'react'
import PropTypes from 'prop-types'

import Grid from './Grid'
import ProductCardBackEnd from './ProductCardBackEnd'

const InfinityListPage = (props) => {

    const listRef = useRef(null);
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
                        <ProductCardBackEnd
                            key={index}
                            img={item.imgAndVideoProducts[0]}
                            name={item.name}
                            price={Number(item.price)}
                            id = {item.id}
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
