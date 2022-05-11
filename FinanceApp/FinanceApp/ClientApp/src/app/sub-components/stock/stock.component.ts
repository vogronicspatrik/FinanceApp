import {Component, OnInit, Input} from '@angular/core';
import {Stock} from "../../models/Stock";
import {StockService} from "../../services/stock.service";
import {Currency, CurrencyType} from "../../models/Currency";

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

  constructor(private stockService: StockService) {
  }

  ngOnInit(): void {
    this.stockService.getCurrentPrice(this.stock.id).subscribe(data => {
      this.currentTotalValue = Math.round(data);
      this.currentPrice = Math.round(data / this.stock?.amount)
    });
    this.totalValueAtPurchase = this.stock?.valueAtPurchase * this.stock?.amount;
  }
}
