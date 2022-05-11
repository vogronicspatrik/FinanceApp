import {Component, OnInit, Input} from '@angular/core';
import {Stock} from "../../models/Stock";
import {Currency} from "../../models/Currency";

@Component({
  selector: 'app-stock',
  templateUrl: './stock.component.html',
  styleUrls: ['./stock.component.css']
})
export class StockComponent implements OnInit {
  @Input() stock!: Stock;
  open: boolean = false;
  currentTotalValue: number = 0.0;
  currentPrice = 0.0;
  totalValueAtPurchase = 0.0;
  displayValueWithCurrency = Currency.getCurrency;

  constructor() {
  }

  ngOnInit(): void {
    this.currentTotalValue = this.stock?.currentValue * this.stock?.amount;
    this.totalValueAtPurchase = this.stock?.valueAtPurchase * this.stock?.amount;
  }
}
