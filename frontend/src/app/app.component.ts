import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap, map } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  public products: Product[];

  constructor(private http: HttpClient) {}

  public ngOnInit(): void {
    this.get();
  }

  public get() {
    this.http
      .post<{ basket: Basket }>(
        'https://localhost:5000/api/basket/get-basket',
        {
          userId: 1
        }
      )
      .pipe(
        tap(res => console.log(res)),
        map(res => res.basket),
        map(res => res.products),
        tap(res => console.log(res))
      )
      .subscribe(res => (this.products = res));

    setTimeout(() => this.get(), 5000);
  }
}

export interface Product {
  id: number;
  name: string;
}

export interface Basket {
  userId: number;
  products: Product[];
}
