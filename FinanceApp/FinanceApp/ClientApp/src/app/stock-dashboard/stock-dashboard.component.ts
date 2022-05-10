import { Component, OnInit } from '@angular/core';
import {Stock} from "../models/Stock";
import {CurrencyType} from "../models/Currency";
import {StockService} from "../services/stock.service";

@Component({
  selector: 'app-stock-dashboard',
  templateUrl: './stock-dashboard.component.html',
  styleUrls: ['./stock-dashboard.component.css']
})
export class StockDashboardComponent implements OnInit {

  stocks: Stock[] = []; 
  
  constructor(private stockService: StockService) { }

  ngOnInit(): void {
    this.stockService.getStocks().subscribe(stocks => {
      this.stocks = stocks;
    });
  }

  addNewStock($event: any) {
    this.stocks.push($event);
  }
}
