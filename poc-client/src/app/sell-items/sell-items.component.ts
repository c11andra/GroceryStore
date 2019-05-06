import { Component, OnInit } from '@angular/core';
import { Product } from '../interfaces/product.interface';
import { DataService } from '../services/data.service';

@Component({
  selector: 'app-sell-items',
  templateUrl: './sell-items.component.html',
  styleUrls: ['./sell-items.component.scss']
})
export class SellItemsComponent implements OnInit {
  products:Product[];
  selectedProducts:Product[] = [];
  constructor(private dataService:DataService) { }

  ngOnInit() {


    this.dataService.getProducts().subscribe(
      (p) =>
      {
        console.log('==  %o', p);
        
       }
      );
 this.dataService.getProducts().subscribe(
   p=>this.products = p
 )

  }
  onDecrement(product:Product)
  {

  }

  onIncrement(product: Product){

    let index = this.selectedProducts.findIndex(function(p){
      return p.Id === product.Id;
      
    });
    console.log(index)
    if(index > -1)
    {
      this.selectedProducts[index].Quantity += 1;
    }
    else
    {
      let newProd = product;
      newProd.Quantity = 1;
      this.selectedProducts.push(newProd);
    }
    // let selectedContainsProduct = this.selectedProducts.find(function(element) {
    //   return element.Id === product.Id;
    // })
    // if(selectedContainsProduct)
    // {
    //   console.log(selectedContainsProduct)
    // }
    // else
    // {

    // }
  }


}
