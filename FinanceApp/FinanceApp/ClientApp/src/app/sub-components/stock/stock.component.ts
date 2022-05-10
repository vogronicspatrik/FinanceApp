import {Component, OnInit, Input} from '@angular/core';
import {Stock} from "../../models/Stock";
import {StockService} from "../../services/stock.service";

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
  getPrice = this.stockService.getCurrentPrice;

  constructor(private stockService: StockService) {
  }

  ngOnInit(): void {
    this.stockService.getCurrentPrice(this.stock.id).subscribe(data => {
      this.currentTotalValue = data;
      this.currentPrice = this.currentTotalValue / this.stock?.amount});
      this.totalValueAtPurchase = this.stock?.valueAtPurchase * this.stock?.amount;
  }
}
