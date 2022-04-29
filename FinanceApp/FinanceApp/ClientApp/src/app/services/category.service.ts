import {Inject, Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Category} from "../models/Category";
import {Observable} from "rxjs";

const httpOptions = {
  headers: new HttpHeaders({'Content-type': 'application/json'})
}

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  getCategories(): Observable<Category[]> {
    console.log(this.baseUrl)
    return this.http.get<Category[]>(`${this.baseUrl}api/categories`);
  }

  addCategory(category: Category): Observable<Category> {
    const newCategory = {
      Name: category.name,
      Color: category.color
    }

    return this.http.post<Category>(`${this.baseUrl}api/categories`, newCategory, httpOptions);
  }

  deleteCategories(ids: number[]): Observable<number[]> {
    return this.http.request<number[]>('delete', `${this.baseUrl}api/categories`, {
      body: ids,
      headers: httpOptions.headers
    });
  }
}
