
export interface Category {
  id: string;
  categoryName: string;
}

export interface ProductImage {
  id: string;
  fileName: string;
  fileExtension: string;
  fileSize: number;
  fileUrl: string;
  createdDate: string;
  productId: string;
}



export interface ProductDetails {
  productId: string;
  productName: string;
  productPrice: number;
  category: Category;
  productFiles: ProductImage[];
}

export interface ApiResponse<T> {
  success: boolean;
  message: string;
  data: T;
}