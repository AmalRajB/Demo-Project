import { Routes } from '@angular/router';
import { AddCountry } from './features/country-feature/add-country/add-country';
import { ListCountry } from './features/country-feature/list-country/list-country';
import { EditCountry } from './features/country-feature/edit-country/edit-country';
import { AddState } from './features/state-feature/add-state/add-state';
import { EditState } from './features/state-feature/edit-state/edit-state';
import { ListState } from './features/state-feature/list-state/list-state';
import { AddFileComponent } from './features/file-feature/add-file-component/add-file-component';
import { ListFileComponent } from './features/file-feature/file-list-component/list-file-component';
import { EditFileComponent } from './features/file-feature/edit-file-component/edit-file-component';
import { HomeComponent } from './features/home-feature/home-component/home-component';
import { AddCategory } from './features/productcategory/add-category/add-category';
import { ListCategory } from './features/productcategory/list-category/list-category';
import { EditCategory } from './features/productcategory/edit-category/edit-category';
import { AddProduct } from './features/product-feature/add-product/add-product';
import { ListProduct } from './features/product-feature/list-product/list-product';
import { EditProduct } from './features/product-feature/edit-product/edit-product';
import { AddProductImage } from './features/product-Images/add-product.image/add-product.image';
import { ListProductImage } from './features/product-Images/list-product.image/list-product.image';
import { EditProductImage } from './features/product-Images/edit-product.image/edit-product.image';
import { ProductListComponent } from './features/inventory-feature/list-inventory/list-inventory';
import { SingleviewInventory } from './features/inventory-feature/singleview-inventory/singleview-inventory';

export const routes: Routes = [
    {
        path: 'admin/addcountry',
        component: AddCountry
    },
    {
        path: 'admin/listcountry',
        component: ListCountry
    },
    {
        path: 'admin/country/edit/:id',
        component: EditCountry

    },
    {
        path: 'admin/addstate',
        component: AddState
    },
    {
        path: 'admin/editstate/:id',
        component: EditState
    },
    {
        path: 'admin/statelist',
        component: ListState
    },
    {
        path: 'admin/addfile',
        component: AddFileComponent
    },
    {
        path: 'admin/filelist',
        component: ListFileComponent
    },
    {
        path: 'admin/file/edit/:id',
        component: EditFileComponent
    },
    {
        path: '',
        component: HomeComponent
    },
    {
        path: 'admin/addProductCategory',
        component: AddCategory
    },
    {
        path: 'admin/listProductCategory',
        component: ListCategory
    },
    {
        path: 'admin/ProductCategory/edit/:id',
        component: EditCategory
    },
    {
        path: 'admin/product/add',
        component: AddProduct
    },
    {
        path: 'admin/product/list',
        component: ListProduct
    },
    {
        path: 'admin/product/edit/:id',
        component: EditProduct
    },
    {
        path:'admin/product-image/add/:id',
        component:AddProductImage
    },
    {
        path:'admin/product-image/list/:id',
        component:ListProductImage
    },
    {
        path:'admin/product-image/edit/:id',
        component:EditProductImage
    },
    {
        path:'admin/inventory/list',
        component:ProductListComponent
    },
    {
        path:'admin/inventory/product/:id',
        component:SingleviewInventory
    }


];
