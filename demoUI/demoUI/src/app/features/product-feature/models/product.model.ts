
export interface Category {
    id: string,
    categoryName: string
}


export interface AddProductDto {
    productName: string,
    productPrice: number,
    categoryId: string
}

export interface ProductDto {
    id: string,
    productName: string,
    productPrice: number,
    categoryId: string,
    category: Category

}

export interface EditProductDto {
    productName: string,
    productPrice: number,
    categoryId: string
}