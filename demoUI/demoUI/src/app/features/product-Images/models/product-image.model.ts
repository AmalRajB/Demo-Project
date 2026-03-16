export interface ProductFileinput {
    id: string,
    FileName: string,
    FileExtension: string,
    FileUrl: string,
    FileSize: string,
    CreatedDate: string,
    productId: string

}
export interface ProductFileoutput {
    id: string,
    fileName: string,
    fileExtension: string,
    fileSize: string,
    createdDate: string,
    fileUrl: string,
    productId: string

}

export interface EditProductFile {

    FileName: string,
    FileExtension: string,
    FileUrl: string,
    FileSize: string,
    CreatedDate: string,
    productId: string

}
