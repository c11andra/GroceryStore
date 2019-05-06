import { Component, OnInit } from '@angular/core';
import { Product } from '../interfaces/product.interface';
import { DataService } from '../services/data.service';

@Component({
  selector: 'app-sell-items',
  templateUrl: './sell-items.component.html',
  styleUrls: ['./sell-items.component.scss']
})
export class SellItemsComponent implements OnInit {
  products: Product[];
  selectedProducts: Product[] = []
  constructor(private dataService: DataService) { }

  ngOnInit() {

    this.dataService.getProducts().subscribe(
      p => this.products = p
    );

  }

  onSell() {

  }
  onDecrement(product: Product) {
    let index = this.selectedProducts.findIndex(p => p.Id === product.Id);
    if (index > -1) {
      if (this.selectedProducts[index].Quantity > 0) {
        this.selectedProducts[index].Quantity -= 1;
      }
      if(this.selectedProducts[index].Quantity === 0)
      {
        

        this.selectedProducts.splice(index, 1);

      }
    }
  }

  onIncrement(product: Product) {
    let index = this.selectedProducts.findIndex(p => p.Id === product.Id);

    if (index > -1) {
      if (product.Quantity > this.selectedProducts[index].Quantity) {
        this.selectedProducts[index].Quantity += 1;
      }
    }
    else {
      let newProd: Product = {
        Id: product.Id,
        Quantity: 1,
        Name: product.Name,
        Category: product.Category
      };

      this.selectedProducts.push(newProd);
    }

  }


}
