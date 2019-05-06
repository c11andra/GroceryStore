export interface Product {
    Id: number;
    Name: string;
    Category: Category;
    Quantity: number;
}

export interface Category {
    Id: number;
    Name: string;
}