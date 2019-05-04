import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';



export interface Category {
  
  Name: string;
}

@Component({
  selector: 'app-manage-items',
  templateUrl: './manage-items.component.html',
  styleUrls: ['./manage-items.component.scss']
})
export class ManageItemsComponent implements OnInit {
  categories: Category[] = [
    { Name: 'Beverages'},
    { Name: 'Bakery'},
    { Name: 'Cleaners'}
  ];
  quantity = 0;
  manageForm: FormGroup;
  constructor(private formBuilder: FormBuilder) {
    
  }

  ngOnInit() {
    this.manageForm = this.formBuilder.group({
      addItem: [''],
      brand: [''],
    });

  }

  onIncrement()
  {
    this.quantity ++; 
  }
  onDecrement()
  {
    if(this.quantity !== 0)
    {
      this.quantity --;
    }
    
  }

}
