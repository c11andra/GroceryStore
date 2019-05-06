import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Product } from '../interfaces/product.interface';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor(private http: HttpClient) {

  }

  public getProducts(): Observable<Product[]> {

    return this.http.get<Product[]>(environment.serviceUrls.getProduct)
      .pipe(
        map(data => 

          data.map((item: any) => {
            return {
              Id: item.id,
              Name: item.name,
              Quantity: item.quantity,
              Category: {
                Id:item.category.id,
                Name:item.category.name,
              }
            };
          }
          )
      

        )
      );
  }

}
