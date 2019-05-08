import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Product } from '../interfaces/product.interface';
import { Observable, throwError } from 'rxjs';
import { map, retry, catchError } from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class DataService {
  baseUrl = environment.serviceUrls.base;
  sellRoute = environment.serviceUrls.sell;
  productsRoute = environment.serviceUrls.products;

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  constructor(private http: HttpClient) {

  }

  sellProducts(selectedProducts: Product[]) {

    return this.http.post(this.baseUrl + this.sellRoute, JSON.stringify(selectedProducts),
      this.httpOptions)
      .pipe(
        retry(1),
        catchError(this.handleError)
      );

  }

  handleError(error) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // Get client-side error
      errorMessage = error.error.message;
    } else {
      // Get server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    window.alert(errorMessage);
    return throwError(errorMessage);
  }

  public getProducts(): Observable<Product[]> {

    return this.http.get<Product[]>(this.baseUrl + this.productsRoute)
      .pipe(
        map(data =>
          data.map((item: any) => {
            return {
              Id: item.id,
              Name: item.name,
              Quantity: item.quantity,
              Category: {
                Id: item.category.id,
                Name: item.category.name,
              }
            };
          }
          )


        )
      );
  }

}
